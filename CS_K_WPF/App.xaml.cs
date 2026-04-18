﻿﻿global using CS.Base;
using CommunityToolkit.Mvvm.Messaging;
using CS.Communication;
using CS.DataAnalasis.View;
using CS.Database;
using CS_K_WPF.view;
using CS_K_WPF.viewModel;
using CSK.Core;
using CSK.Core.Messaging;
using LiveChartsCore;
using LoggerProj;
using Microsoft.Extensions.DependencyInjection;
using ProtoBuf.Meta;
using System.Configuration;
using System.Data;
using System.Reflection;
using System.Windows;

namespace CS_K_WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// 这个是DI的全局容器，可以在应用程序的任何地方使用它来获取服务实例
        /// </summary>
        public static IServiceProvider GlobalServiceProvider { get; private set; }
        private IServiceCollection service;

        private string uniqueToken = "F88E8E09-7905-4F2F-8A9E-98D7B6E87C12";
        private static Mutex mutex;
        public App()
        {

        }

        

        private void DIContainerInit()
        {
            service = new ServiceCollection();
            
            // 自动发现并注册所有服务
            RegisterAllServices(service);




            //-----------服务容器构建完成，不可再添加服务，只能获取服务【重点】-------------
            GlobalServiceProvider = service.BuildServiceProvider();


            Resources["ModelLocator"] = GlobalServiceProvider.GetRequiredService<ModelLocator>();
            MessageRegister(GlobalServiceProvider); //报警窗口Message注册
            ViewManeger.Init();
        }

        /// <summary>
        /// 自动发现并注册所有服务
        /// </summary>
        private void RegisterAllServices(IServiceCollection services)
        {
            
            // 【1】获取当前程序集
            var currentAssembly = Assembly.GetExecutingAssembly();
            var assemblies = new List<Assembly>
            { 
                currentAssembly ,
                Assembly.Load("LoggerProj") //手动强制加载程序集的方式
            };
            
            // 【2】获取所有引用的程序集。要不然其他的引用项目无法注册到DI中
            foreach (var referencedAssemblyName in currentAssembly.GetReferencedAssemblies())
            {
                try
                {
                    var assembly = Assembly.Load(referencedAssemblyName);
                    assemblies.Add(assembly);
                }
                catch { }
            }
            
            // 查找所有实现了IServiceRegistration接口的类型
            var registrationTypes = new List<Type>();
            foreach (var assembly in assemblies)
            {
                try
                {
                    var types = assembly.GetTypes()
                        .Where(t => typeof(IServiceRegistration).IsAssignableFrom(t) && !t.IsAbstract); //判断t是不是IServiceRegistration派生类 && 不是抽象类。
                    registrationTypes.AddRange(types);
                }
                catch { }
            }
            
            // 实例化并执行服务注册(以前的老办法是主程序引用其他程序集并直接调用，现在其他程序集依赖于base，利用反射注册)
            foreach (var type in registrationTypes)
            {
                try
                {
                    var registration = (IServiceRegistration)Activator.CreateInstance(type);
                    registration.RegisterServices(services); //将容器传入(这个方式和以前不一样，以前是传到模块去，这里是通过反射得到的传送通道，没有耦和模块)
                }
                catch { }
            }
            
            // 注册核心服务
            services.AddSingleton<ModelLocator>();
            DatabaseServiceProvider.Register(services);
        }

        private void MessageRegister(IServiceProvider provider)
        {
            //消息总线方式：
            GlobalServiceProvider.GetRequiredService<IMessageBus>().Register<OpenExceptionWindowMes>((sender, message) =>
            {
                var wnd = provider.GetRequiredService<ExceptionWnd>();
                var vm = provider.GetRequiredService<ExceptionWndVM>();
                vm.Title = message.Title;
                vm.MesForDev = message.ExpMesForDeveloper;
                vm.MesForUser = message.ExpMesForUser;
                wnd.DataContext = vm;
                wnd.Show();
            });

            //老按本(原生消息)方式
            //WeakReferenceMessenger.Default.Register<OpenExceptionWindowMes>(this, (sender, message) =>
            //{
            //    var wnd = provider.GetRequiredService<ExceptionWnd>();
            //    var vm = provider.GetRequiredService<ExceptionWndVM>();
            //    vm.Title = message.Title;
            //    vm.MesForDev = message.ExpMesForDeveloper;
            //    vm.MesForUser = message.ExpMesForUser;
            //    wnd.DataContext = vm;
            //    wnd.Show();
            //});
        }

        private void Application_StartUp(object sender, StartupEventArgs e)
        {
            Application_IsCAnRun();

            //1.拦截UI异常
            DispatcherUnhandledException += UIExceptionHandler;

            //2.拦截异步任务异常(防止task内的异常，此异常不会抛而给到了返回值内，如果从未用过返回值，没取出来去catch或者throw出来，
            //则到该task被GC回收时，触发异常导致报错或者崩溃)
            TaskScheduler.UnobservedTaskException += TaskExceptionHandler;

            //3.全局兜底异常拦截
            AppDomain.CurrentDomain.UnhandledException += GlobalExceptionHandler;


            DIContainerInit();

            // 启动通信服务(前面已经通过反射实现了DI注册，因此这里可以直接获取服务，直接调用)
            var communicationStarter = GlobalServiceProvider.GetRequiredService<ICommunicationServiceStarter>();
            communicationStarter.Start();

            //// 直接new MainWindow并显示，和之前逻辑一致
            //MainWindow mainWindow = new MainWindow();
            //mainWindow.Show();
        }

        private void UIExceptionHandler(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            // 这里可以记录日志、显示错误信息等
            //MessageBox.Show($"[UIExcep]拦截到未处理的UI异常: {e.Exception.Message}", "kk错误", MessageBoxButton.OK, MessageBoxImage.Error);
            WeakReferenceMessenger.Default.Send(new OpenExceptionWindowMes("[UIExcep]拦截到未处理的UI异常", e.Exception.Message, e.Exception.StackTrace.ToString()));

            e.Handled = true; // 标记异常已处理，防止程序崩溃
        }

        private void GlobalExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            // 这里可以记录日志、显示错误信息等
            Exception ex = e.ExceptionObject as Exception;
            MessageBox.Show($"[GlobalExcep]拦截到未处理的全局异常: {ex?.Message}", "kk错误", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void TaskExceptionHandler(object sender, UnobservedTaskExceptionEventArgs e)
        {
            // 这里可以记录日志、显示错误信息等
            MessageBox.Show($"[TaskExcep]拦截到未处理的异步任务异常: {e.Exception.Message}", "kk错误", MessageBoxButton.OK, MessageBoxImage.Error);
            e.SetObserved(); // 标记异常已观察，防止程序崩溃
        }


        private void Application_IsCAnRun()
        {
            //uniqueToken一定要确保唯一，如果不唯一的话存在重名，系统就会判定这是同一个锁，会认为这个程序已经在运行了，导致你无法启动，或者导致其他程序无法启动，这既是误伤！
            mutex = new Mutex(true, uniqueToken, out bool iscanRun);
            if(!iscanRun)
            {
                MessageBox.Show("程序已经在运行了！", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                mutex.Dispose();
                App.Current.Shutdown(); //WPF 推荐的正常退出方式，走生命周期、优雅收尾；
                //Environment.Exit(0); //强制退出，绕过生命周期，直接杀进程；
            }
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            // 停止通信服务
            try
            {
                var communicationStarter = GlobalServiceProvider?.GetRequiredService<CS.Communication.ICommunicationServiceStarter>();
                communicationStarter?.Stop();
            }
            catch { }
            
            mutex?.Dispose();
        }

        private void Application_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {

       
        }
    }
}

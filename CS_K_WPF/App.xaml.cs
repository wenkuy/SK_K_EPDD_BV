global using CS.Base;
using CommunityToolkit.Mvvm.Messaging;
using CS_K_WPF.view;
using CS_K_WPF.viewModel;
using CSK.Core;
using CS.DataAnalasis.View;
using CS.Database;
using LoggerProj;
using Microsoft.Extensions.DependencyInjection;
using ProtoBuf.Meta;
using System.Configuration;
using System.Data;
using System.Windows;
using CS.Communication;
using System.Reflection;

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
            RegisterWndServices(service);
            RegisterPageServides(service);
            LogRegister.Register(service);//日志注册
            service.AddSingleton<ModelLocator>();
            service.AddSingleton<ReceiveMessages>(); //通信服务注册
            DatabaseServiceProvider.Register(service);
            //-----------服务容器构建完成，不可再添加服务，只能获取服务【重点】-------------
            GlobalServiceProvider = service.BuildServiceProvider();


            Resources["ModelLocator"] = GlobalServiceProvider.GetRequiredService<ModelLocator>();
            MessageRegister(GlobalServiceProvider); //报警窗口Message注册
            ViewManeger.Init();
        }


        /// <summary>
        /// DI注册窗体windows
        /// </summary>
        private void RegisterWndServices(IServiceCollection service)
        {
            service.AddTransient<IWindowOperation, WindowOperation>();
            service.AddTransient<DataAnalysisWnd>();
            service.AddTransient<ExceptionWnd>();
        }

        /// <summary>
        /// DI注册viewModelVM
        /// </summary>
        private void RegisterPageServides(IServiceCollection service)
        {
            service.AddSingleton<HomePageVM>();
            service.AddSingleton<NavigationPageVM>();
            service.AddSingleton<MainPageVM>();
            service.AddSingleton<SettingPageVm>();
            service.AddSingleton<UserPageVM>();

            service.AddTransient<ExceptionWndVM>();
        }

        private void MessageRegister(IServiceProvider provider)
        {
            WeakReferenceMessenger.Default.Register<OpenExceptionWindowMes>(typeof(ExceptionWnd), (obj, TMes) =>
            {
                var wnd = provider.GetRequiredService<ExceptionWnd>();
                var vm = provider.GetRequiredService<ExceptionWndVM>();
                vm.Title = TMes.Title;
                vm.MesForDev = TMes.ExpMesForDeveloper;
                vm.MesForUser = TMes.ExpMesForUser;
                wnd.DataContext = vm;
                wnd.Show();
            });
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
            mutex?.Dispose();
        }
    }
}

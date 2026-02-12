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

            ViewManeger.ViewRegisterManegerGeneral<OpenAnalysisWindowMes, DataAnalysisWnd>();
        }


        /// <summary>
        /// DI注册窗体windows
        /// </summary>
        private void RegisterWndServices(IServiceCollection service)
        {
            service.AddTransient<IWindowOperation, WindowOperation>();
            service.AddTransient<DataAnalysisWnd>();
        }

        /// <summary>
        /// DI注册viewModelVM
        /// </summary>
        private void RegisterPageServides(IServiceCollection service)
        {
            service.AddSingleton<HomePageVM>();
            service.AddSingleton<NavigationPageVM>();
            service.AddSingleton<MainPageVM>();
        }

        private void Application_StartUp(object sender, StartupEventArgs e)
        {

            //1.拦截UI异常
            DispatcherUnhandledException += UIExceptionHandler;

            //2.拦截异步任务异常
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
            MessageBox.Show($"[UIExcep]拦截到未处理的UI异常: {e.Exception.Message}", "kk错误", MessageBoxButton.OK, MessageBoxImage.Error);
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
    }
}

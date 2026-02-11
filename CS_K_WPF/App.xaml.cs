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
            DIContainerInit();

            //// 直接new MainWindow并显示，和之前逻辑一致
            //MainWindow mainWindow = new MainWindow();
            //mainWindow.Show();
        }
    }



}

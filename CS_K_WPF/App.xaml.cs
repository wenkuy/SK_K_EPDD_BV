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
            service  = new ServiceCollection();
            RegisterWndServices(service);
            RegisterPageServides(service);
            LogRegister.Register(service);//日志注册
            service.AddSingleton<ReceiveMessages>(); //通信服务注册
            GlobalServiceProvider = service.BuildServiceProvider(); //服务容器构建完成，不可再添加服务，只能获取服务【重点】

            //这个数数据库模块的初始化必须在应用程序启动时就进行，否则后续调用会报错
            DatabaseServiceProvider.Register(service);

            //数据分析模块需要在这里提前注册消息
            //ViewManeger.ViewRegisterManeger();
            ViewManeger.ViewRegisterManegerGeneral<OpenAnalysisWindowMes,DataAnalysisWnd>();
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

    }



}

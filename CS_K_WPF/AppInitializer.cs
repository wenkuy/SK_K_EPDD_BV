using CS.Communication;
using CS_K_WPF.view;
using CSK.Core.Messaging;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace CS_K_WPF
{
    /// <summary>
    /// 应用程序初始化类
    /// </summary>
    public static class AppInitializer
    {
        /// <summary>
        /// 初始化应用程序
        /// </summary>
        public static void Initialize()
        {
            // 异常处理
            ExceptionHandler.RegisterHandlers();

            // DI注册
            ServiceRegistrar.DIRegisterServices();

            // 报警弹窗注册
            MessageRegistrar.RegisterMessages();

            // 先获取 ReceiveMessages 实例，设置 UDPMessAction
            App.GlobalServiceProvider.GetRequiredService<ReceiveMessages>();

            // 启动通信TCP UDP服务 
            var communicationStarter = App.GlobalServiceProvider.GetRequiredService<ICommunicationServiceStarter>();
            communicationStarter.Start();

            //// 显示主窗口
            //var mainWindow = App.GlobalServiceProvider.GetRequiredService<MainWindow>();
            //mainWindow.Show();
        }

        /// <summary>
        /// 关闭应用程序
        /// </summary>
        public static void ShutdownCommunication()
        {
            // 停止通信服务
            try
            {
                var communicationStarter = App.GlobalServiceProvider?.GetRequiredService<ICommunicationServiceStarter>();
                communicationStarter?.Stop();
            }
            catch { }
        }
    }
}
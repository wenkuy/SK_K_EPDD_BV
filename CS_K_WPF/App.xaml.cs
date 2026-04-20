using CS.Base;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
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
        public static IServiceProvider GlobalServiceProvider { get; set; }

        private string uniqueToken = "F88E8E09-7905-4F2F-8A9E-98D7B6E87C12";
        private static Mutex mutex;

        public App()
        {
            //CommonDispatcherHelper.Init();
        }

        private void Application_StartUp(object sender, StartupEventArgs e)
        {
            Application_IsCAnRun();

          
            // 初始化应用程序
            AppInitializer.Initialize();
        }

        private void Application_IsCAnRun()
        {
            //uniqueToken一定要确保唯一，如果不唯一的话存在重名，系统就会判定这是同一个锁，会认为这个程序已经在运行了，导致你无法启动，或者导致其他程序无法启动，这既是误伤！
            mutex = new Mutex(true, uniqueToken, out bool iscanRun);
            if (!iscanRun)
            {
                MessageBox.Show("程序已经在运行了！", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                mutex.Dispose();
                App.Current.Shutdown(); //WPF 推荐的正常退出方式，走生命周期、优雅收尾；
                //Environment.Exit(0); //强制退出，绕过生命周期，直接杀进程；
            }
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            // 关闭应用程序
            AppInitializer.ShutdownCommunication();
            mutex?.Dispose();
        }


    }
}
using CS.Communication;
using CSK.Core;
using CSK.Core.Messaging;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace CS_K_WPF
{
    /// <summary>
    /// 异常处理类
    /// </summary>
    public static class ExceptionHandler
    {
        /// <summary>
        /// 注册异常处理程序
        /// </summary>
        public static void RegisterHandlers()
        {
            // 1. 拦截UI异常
            Application.Current.DispatcherUnhandledException += UIExceptionHandler;

            // 2. 拦截异步任务异常
            TaskScheduler.UnobservedTaskException += TaskExceptionHandler;

            // 3. 全局兜底异常拦截
            AppDomain.CurrentDomain.UnhandledException += GlobalExceptionHandler;
        }

        private static void UIExceptionHandler(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            // 这里可以记录日志、显示错误信息等
            App.GlobalServiceProvider.GetRequiredService<IMessageBus>().Send(new OpenExceptionWindowMes("[UI]异常", e.Exception.Message, e.Exception.StackTrace.ToString()));

            e.Handled = true; // 标记异常已处理，防止程序崩溃
        }

        private static void GlobalExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            // 这里可以记录日志、显示错误信息等
            Exception ex = e.ExceptionObject as Exception;
            App.GlobalServiceProvider.GetRequiredService<IMessageBus>().Send(new OpenExceptionWindowMes("[global]异常", ex.Message, ex.StackTrace.ToString()));
        }

        private static void TaskExceptionHandler(object sender, UnobservedTaskExceptionEventArgs e)
        {
            // 这里可以记录日志、显示错误信息等
            MessageBox.Show($"[TaskExcep]拦截到未处理的异步任务异常: {e.Exception.Message}", "kk错误", MessageBoxButton.OK, MessageBoxImage.Error);

            App.GlobalServiceProvider.GetRequiredService<IMessageBus>().Send(new OpenExceptionWindowMes("[Task]异常", e.Exception.Message, e.Exception.StackTrace.ToString()));
            e.SetObserved(); // 标记异常已观察，防止程序崩溃
        }
    }
}
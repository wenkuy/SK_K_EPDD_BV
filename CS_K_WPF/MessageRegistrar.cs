using CS.Communication;
using CS.DataAnalasis.View;
using CS_K_WPF.view;
using CS_K_WPF.viewModel;
using CSK.Core;
using CSK.Core.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace CS_K_WPF
{
    /// <summary>
    /// 消息注册类
    /// </summary>
    public static class MessageRegistrar
    {
        /// <summary>
        /// 注册消息
        /// </summary>
        public static void RegisterMessages()
        {
            var provider = App.GlobalServiceProvider;

            // 消息总线方式
            provider.GetRequiredService<IMessageBus>().Register<OpenExceptionWindowMes>((sender, message) =>
            {
                var wnd = provider.GetRequiredService<ExceptionWnd>();
                var vm = provider.GetRequiredService<ExceptionWndVM>();
                vm.Title = message.Title;
                vm.MesForDev = message.ExpMesForDeveloper;
                vm.MesForUser = message.ExpMesForUser;
                wnd.DataContext = vm;
                wnd.Show();
            });
        }
    }
}
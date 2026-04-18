using CommunityToolkit.Mvvm.Messaging;

namespace CSK.Core.Messaging
{
    /// <summary>
    /// 消息总线实现
    /// </summary>
    public class MessageBus : IMessageBus
    {
        /// <summary>
        /// 注册消息处理器
        /// </summary>
        public void Register<TMessage>(Action<object, TMessage> handler) where TMessage : class
        {
            WeakReferenceMessenger.Default.Register<TMessage>(this, (recipient, message) =>
            {
                handler(recipient,message); 
            });
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <typeparam name="TMessage">消息类型</typeparam>
        /// <param name="message">消息内容</param>
        public void Send<TMessage>(TMessage message) where TMessage : class
        {
            WeakReferenceMessenger.Default.Send(message);
        }
    }
}

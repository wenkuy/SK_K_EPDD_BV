namespace CSK.Core.Messaging
{
    /// <summary>
    /// 消息总线接口
    /// </summary>
    public interface IMessageBus
    {
        /// <summary>
        /// 注册消息处理器
        /// </summary>
        void Register<TMessage>(Action<object, TMessage> handler) where TMessage : class;
        
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <typeparam name="TMessage">消息类型</typeparam>
        /// <param name="message">消息内容</param>
        void Send<TMessage>(TMessage message) where TMessage : class;
    }
}

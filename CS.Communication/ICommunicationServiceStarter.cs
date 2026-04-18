namespace CS.Communication
{
    /// <summary>
    /// 通信服务启动器接口
    /// </summary>
    public interface ICommunicationServiceStarter
    {
        /// <summary>
        /// 启动所有通信服务
        /// </summary>
        void Start();
        
        /// <summary>
        /// 停止所有通信服务
        /// </summary>
        void Stop();
    }
}
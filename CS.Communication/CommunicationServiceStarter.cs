using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CS.Communication
{
    /// <summary>
    /// 通信服务启动器实现
    /// </summary>
    public class CommunicationServiceStarter : ICommunicationServiceStarter
    {
        private readonly TcpServer _tcpServer;
        private readonly UdpReceiver _udpReceiver;
        private readonly ILogger<CommunicationServiceStarter> _logger;
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tcpServer">TCP服务器实例</param>
        /// <param name="udpReceiver">UDP接收器实例</param>
        /// <param name="logger">日志记录器</param>
        public CommunicationServiceStarter(TcpServer tcpServer, UdpReceiver udpReceiver, ILogger<CommunicationServiceStarter> logger)
        {
            _tcpServer = tcpServer;
            _udpReceiver = udpReceiver;
            _logger = logger;
        }
        
        /// <summary>
        /// 启动所有通信服务
        /// </summary>
        public void Start()
        {
            _logger.LogInformation("启动通信服务...");
            
            // 启动TCP服务
            Task.Run(() => 
            {
                try
                {
                    _tcpServer.TcpServerRun();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "TCP服务启动失败");
                }
            });
            
            // 启动UDP服务
            Task.Run(() => 
            {
                try
                {
                    _udpReceiver.UdpReceiveRun();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "UDP服务启动失败");
                }
            });
            
            _logger.LogInformation("通信服务启动完成");
        }
        
        /// <summary>
        /// 停止所有通信服务
        /// </summary>
        public void Stop()
        {
            _logger.LogInformation("停止通信服务...");
            
            // 这里可以添加停止逻辑
            // 由于TcpServer和UdpReceiver使用了无限循环，可能需要添加取消令牌来支持优雅停止
            
            _logger.LogInformation("通信服务停止完成");
        }
    }
}
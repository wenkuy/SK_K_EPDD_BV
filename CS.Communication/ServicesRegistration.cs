using CS.Base;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS.Communication
{
    public class ServicesRegistration : IServiceRegistration
    {
        public void RegisterServices(IServiceCollection services)
        {
            // 注册通信服务
            services.AddSingleton<TcpServer>();
            services.AddSingleton<UdpReceiver>();
            services.AddSingleton<ICommunicationServiceStarter, CommunicationServiceStarter>();
            
            // 注册消息接收服务
            services.AddSingleton<ReceiveMessages>();

        }
    }
}
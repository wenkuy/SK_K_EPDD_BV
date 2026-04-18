using CS.Base;
using CSK.Core.Messaging;
using CSK.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CSK.Core
{
    /// <summary>
    /// 核心服务注册
    /// </summary>
    public class ServicesRegistration : IServiceRegistration
    {
        public void RegisterServices(IServiceCollection services)
        {
            // 注册业务服务
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IEquipmentService, EquipmentService>();
            services.AddTransient<IMaterialService, MaterialService>();
            
            // 注册消息总线
            services.AddSingleton<IMessageBus, MessageBus>();  //消息总部必须唯一，所以必须是单例。如果不是，那就接受者很多个，不是指定的那个接收的，那个也没法处理执行。
        }
    }
}

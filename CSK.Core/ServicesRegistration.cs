using CS.Base;
using CSK.Core.Messaging;
using CSK.Core.QueryService;
using CSK.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CSK.Core
{
    public class ServicesRegistration : IServiceRegistration
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IEquipmentService, EquipmentService>();
            services.AddTransient<IMaterialService, MaterialService>();

            services.AddTransient<IProductQueryService, ProductQueryService>();
            services.AddTransient<IEquipmentQueryService, EquipmentQueryService>();
            services.AddTransient<IMaterialQueryService, MaterialQueryService>();

            services.AddSingleton<IMessageBus, MessageBus>();
        }
    }
}
using CS.Base;
using CS.DataAnalasis.View;
using CS_K_WPF.view;
using Microsoft.Extensions.DependencyInjection;

namespace CS_K_WPF.Services
{
    /// <summary>
    /// UI服务注册
    /// </summary>
    public class UIServicesRegistration : IServiceRegistration
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IWindowOperation, WindowOperation>();
            services.AddTransient<DataAnalysisWnd>();
            services.AddTransient<ExceptionWnd>();
        }
    }
}

using CS.Base;
using CS_K_WPF.viewModel;
using Microsoft.Extensions.DependencyInjection;

namespace CS_K_WPF.Services
{
    /// <summary>
    /// ViewModel服务注册
    /// </summary>
    public class ViewModelServicesRegistration : IServiceRegistration
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<HomePageVM>();
            services.AddSingleton<NavigationPageVM>();
            services.AddSingleton<MainPageVM>();
            services.AddSingleton<SettingPageVm>();

            services.AddTransient<UserPageVM>();
            services.AddTransient<ExceptionWndVM>();
        }
    }
}

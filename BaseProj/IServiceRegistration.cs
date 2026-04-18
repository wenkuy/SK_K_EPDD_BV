using Microsoft.Extensions.DependencyInjection;

namespace CS.Base
{
    /// <summary>
    /// 服务注册接口，用于模块化服务注册
    /// </summary>
    public interface IServiceRegistration
    {
        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="services">服务集合</param>
        void RegisterServices(IServiceCollection services);
    }
}

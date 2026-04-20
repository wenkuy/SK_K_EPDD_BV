using CS.Base;
using CS.Database;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CS_K_WPF
{
    /// <summary>
    /// 服务注册类
    /// </summary>
    public static class ServiceRegistrar
    {
        /// <summary>
        /// 注册所有服务
        /// </summary>
        public static void DIRegisterServices()
        {
            var services = new ServiceCollection();

            // 自动发现并注册所有服务
            RegisterAllServices(services);

            // 构建服务提供器
            App.GlobalServiceProvider = services.BuildServiceProvider();

            // 设置模型定位器
            App.Current.Resources["ModelLocator"] = App.GlobalServiceProvider.GetRequiredService<ModelLocator>();
        }

        /// <summary>
        /// 自动发现并注册所有服务
        /// </summary>
        private static void RegisterAllServices(IServiceCollection services)
        {
            // 【1】获取当前程序集
            var currentAssembly = Assembly.GetExecutingAssembly();
            var assemblies = new System.Collections.Generic.List<Assembly>
            {
                currentAssembly,
                Assembly.Load("LoggerProj") // 手动强制加载程序集的方式
            };

            // 【2】获取所有引用的程序集
            foreach (var referencedAssemblyName in currentAssembly.GetReferencedAssemblies())
            {
                try
                {
                    var assembly = Assembly.Load(referencedAssemblyName);
                    assemblies.Add(assembly);
                }
                catch { }
            }

            // 查找所有实现了IServiceRegistration接口的类型
            var registrationTypes = new System.Collections.Generic.List<System.Type>();
            foreach (var assembly in assemblies)
            {
                try
                {
                    var types = assembly.GetTypes()
                        .Where(t => typeof(IServiceRegistration).IsAssignableFrom(t) && !t.IsAbstract);
                    registrationTypes.AddRange(types);
                }
                catch { }
            }

            // 实例化并执行服务注册
            foreach (var type in registrationTypes)
            {
                try
                {
                    var registration = (IServiceRegistration)System.Activator.CreateInstance(type);
                    registration.RegisterServices(services);
                }
                catch { }
            }

            // 注册核心服务
            services.AddSingleton<ModelLocator>();
            DatabaseServiceProvider.Register(services);
        }
    }
}
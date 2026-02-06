using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerProj
{
    public class LogRegister
    {
        public static void Register(IServiceCollection services)
        {
            services.AddLogging(configure =>  //注册日志组件
            {
                Log.Logger = new LoggerConfiguration() //配置日志参数
                    .MinimumLevel.Debug()
                    .Enrich.FromLogContext()
                    .WriteTo.Console()
                    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
                    .CreateLogger();
                configure.AddSerilog();
            });
        }
    }
}

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
#if DEBUG
            //配置日志参数
            Log.Logger = new LoggerConfiguration() 
                .MinimumLevel.Verbose()
                .Enrich.FromLogContext()
                .WriteTo.Console()//这个是输出到cmd控制台，不是调试的输出窗口
                //.WriteTo.File("logs/log-.txt",Serilog.Events.LogEventLevel.Information, fileSizeLimitBytes: 500,  rollingInterval: RollingInterval.Day) // bin/Debug/net8.0-windows/logs/ 下的按天滚动 txt 文件,eg:log-20260206.txt
                .WriteTo.File("logs/log-.txt",Serilog.Events.LogEventLevel.Information, fileSizeLimitBytes: 1024, rollOnFileSizeLimit: true, rollingInterval: RollingInterval.Infinite) // bin/Debug/net8.0-windows/logs/ 下的按天滚动 txt 文件,eg:log-20260206.txt
                .CreateLogger();
#else
              //配置日志参数
            Log.Logger = new LoggerConfiguration() 
                .MinimumLevel.Verbose()
                .Enrich.FromLogContext()
                .WriteTo.Console()//这个是输出到cmd控制台，不是调试的输出窗口
                //.WriteTo.File("logs/log-.txt",Serilog.Events.LogEventLevel.Debug, fileSizeLimitBytes: 500,  rollingInterval: RollingInterval.Day) // bin/Debug/net8.0-windows/logs/ 下的按天滚动 txt 文件,eg:log-20260206.txt
                .WriteTo.File("logs/log-.txt",Serilog.Events.LogEventLevel.Debug, fileSizeLimitBytes: 1024, rollOnFileSizeLimit: true, rollingInterval: RollingInterval.Infinite) // bin/Debug/net8.0-windows/logs/ 下的按天滚动 txt 文件,eg:log-20260206.txt
                .CreateLogger();
#endif

            // 然后再配置 DI 的日志服务，接入已初始化的 Serilog
            services.AddLogging(configure =>
            {
                configure.AddSerilog(); // 直接传入已初始化的 Log.Logger，无需再传参数
            });
        }
    }
}

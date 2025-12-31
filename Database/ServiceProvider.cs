using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public static class ServiceProvider
    {
        // 全局容器实例（只初始化一次）
        private static IServiceProvider _serviceProvider;
        public static void Initialize()
        {
                 _serviceProvider = new ServiceCollection()
                 .AddTransient<CSK_DBContext>()
                 .AddTransient<IDBRecordPOperation, OperationDate>()
                 .BuildServiceProvider();
        }

        public static OperationDate DatabaseServicesProvider()
        {
            //获取服务
            return (OperationDate)_serviceProvider.GetRequiredService<IDBRecordPOperation>(); 
        }

    
    }
}

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS.Database
{
    public class DatabaseServiceProvider
    {
        public static void Register(IServiceCollection service)
        {
            service.AddScoped<CSK_DBContext>();
            service.AddScoped<IDBRecordPOperation, OperationDate>();
        }
    }
}
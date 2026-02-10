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
        // 全局容器：

        public static void Register(IServiceCollection service)
        {
            service.AddScoped<CSK_DBContext>();
            service.AddScoped<IDBRecordPOperation, OperationDate>();
            /* 1.这里不搞瞬态，因为每次要重连数据里，太频繁一出问题
               2. 不搞单例。DbContext 本身不 “持有” 物理数据库连接，执行查询 / 新增 / 修改操作才会从连接池借一个物理连接，执行完 SQL 后立即把连接归还到连接池。
                  连接池的连接数量，取决于 “并发执行的数据库操作数。
                  a.单例 DbContext 会永久缓存所有查询过的实体，内存越用越大，查询 / 操作越来越慢；慢查询会让 “借出去的连接” 迟迟无法归还到连接池，连接池被占满。
                  b. DbContext 本身不是线程安全的！单例模式下，多线程（比如多个请求）同时调用这个 DbContext，会导致：
                     ① 操作冲突，SQL 执行卡住；
                     ② 连接释放异常，连接池里的连接被 “挂起”，无法复用最终连接池连接数飙升至数据库的 “最大连接数”。
            3.换成scoped「一个业务操作（比如一次接口请求 / 一次批量数据处理）对应一个 CSK_DBContext 实例」，同一业务内多次调用 OperationDate 会复用这个实例，避免重复建连接。
             */


        }



        //【问题】本来已经解耦了，但是这种该方式又导致直接依赖了OperationDate类，所以这里返回类型改成接口：IDBRecordPOperation
        //public static OperationDate DatabaseServicesProvider()

        //【问题2】不可能这里获取服务，这里去引用主容器获取服务，引用就颠覆了：下层引用上层。
        //public static IDBRecordPOperation DatabaseServicesProvider()
        //{
        //    
        //    return App.GlobalServiceProvider.GetRequiredService<IDBRecordPOperation>(); 
        //}


    }
}

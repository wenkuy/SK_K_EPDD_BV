using CS.Database.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CS.Database.Repository
{
    /// <summary>
    /// 产品仓储接口
    /// </summary>
    public interface IProductRepository : IRepository<ProducttProductionRecord>
    {
        /// <summary>
        /// 获取产品生产记录
        /// </summary>
        /// <param name="productNumber">产品编号</param>
        /// <returns></returns>
        ProducttProductionRecord[] GetProductRecords(string productNumber);
    }
}
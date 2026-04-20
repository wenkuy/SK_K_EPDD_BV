using CS.Database.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CS.Database.Repository
{
    /// <summary>
    /// 产品仓储实现
    /// </summary>
    public class ProductRepository : Repository<ProducttProductionRecord>, IProductRepository
    {
        private readonly ILogger<ProductRepository> _logger;

        public ProductRepository(CSK_DBContext dbContext, ILogger<ProductRepository> logger) : base(dbContext, logger)
        {
            _logger = logger;
        }

        public ProducttProductionRecord[] GetProductRecords(string productNumber)
        {
            try
            {
                var records = GetAll(e => e.ProductNumber == productNumber);
                _logger.LogInformation($"获取产品记录完成: 产品编号={productNumber}, 记录数={records?.Length ?? 0}");
                return records;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"获取产品记录失败: 产品编号={productNumber}");
                return null;
            }
        }
    }
}
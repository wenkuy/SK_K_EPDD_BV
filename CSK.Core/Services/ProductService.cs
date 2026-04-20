using CS.Base;
using CS.Database;
using CS.Database.Entity;
using CS.Database.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSK.Core.Services
{
    /// <summary>
    /// 产品服务实现
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductService> _logger;
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="productRepository">产品仓储接口</param>
        /// <param name="logger">日志记录器</param>
        public ProductService(IProductRepository productRepository, ILogger<ProductService> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }
        
        /// <summary>
        /// 处理产品动态数据
        /// </summary>
        /// <param name="product">产品数据</param>
        public void HandleProductDynamicData(ProducttProductionRecord product)
        {
            _logger.LogInformation($"处理产品数据: ProductNumber={product.ProductNumber}, TimeConsumed={product.TimeConsumed}, QualityInspectionResult={product.QualityInspectionResult}");
        }
        
        /// <summary>
        /// 统计指定时间范围内的数据
        /// </summary>
        /// <param name="hours">小时数</param>
        /// <returns>生产线生产记录</returns>
        public async Task<LineProductionRecord> StatisticalData(float hours = 12)
        {
            return await Task.Run(() =>
            {
                // 通过仓储层获取原始数据
                var productRecords = _productRepository.GetProductRecords("A-XH-GHTY0215");

                if (productRecords == null || productRecords.Length == 0)
                {
                    _logger.LogInformation("没有找到产品生产记录");
                    return new LineProductionRecord();
                }

                // 在业务服务层进行统计分析
                int totalProducts = productRecords.Length;
                int goodProducts = productRecords.Count(e => e.QualityInspectionResult);
                float passRate = totalProducts > 0 ? (float)goodProducts / totalProducts : 0;

                var statistics = new LineProductionRecord
                {
                    ProductsRate = new CS.Database.Structs.ProductQualifiedRate
                    {
                        Product1Rate = passRate
                    }
                };

                _logger.LogInformation($"产品统计完成: 总数={totalProducts}, 合格数={goodProducts}, 合格率={passRate:P2}");

                return statistics;
            });
        }
    }
}
using CS.Database.Entity;
using CSK.Core.QueryService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSK.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductQueryService _productQueryService;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IProductQueryService productQueryService, ILogger<ProductService> logger)
        {
            _productQueryService = productQueryService;
            _logger = logger;
        }

        public void HandleProductDynamicData(ProducttProductionRecord product)
        {
            _logger.LogInformation($"处理产品数据: ProductNumber={product.ProductNumber}, TimeConsumed={product.TimeConsumed}, QualityInspectionResult={product.QualityInspectionResult}");
        }

        public async Task<LineProductionRecord> StatisticalData(float hours = 12)
        {
            return await Task.Run(() =>
            {
                var productRecords = _productQueryService.GetProductRecords("A-XH-GHTY0215");

                if (productRecords == null || productRecords.Length == 0)
                {
                    _logger.LogInformation("没有找到产品生产记录");
                    return new LineProductionRecord();
                }

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
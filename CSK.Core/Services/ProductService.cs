using CS.Base;
using CS.Database;
using CS.Database.Entity;
using Microsoft.Extensions.Logging;

namespace CSK.Core.Services
{
    /// <summary>
    /// 产品服务实现
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly IDBRecordPOperation _dbOperation;
        private readonly ILogger<ProductService> _logger;
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbOperation">数据库操作接口</param>
        /// <param name="logger">日志记录器</param>
        public ProductService(IDBRecordPOperation dbOperation, ILogger<ProductService> logger)
        {
            _dbOperation = dbOperation;
            _logger = logger;
        }
        
        /// <summary>
        /// 处理产品动态数据
        /// </summary>
        /// <param name="product">产品生产记录</param>
        public void HandleProductDynamicData(ProducttProductionRecord product)
        {
            // 这里可以添加业务逻辑处理
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
                // 1. 按小时 -天 -周 -月 -年统计
                string strtime = _dbOperation.DBGetSingleObj<ProducttProductionRecord>(e => true).ProductTime;
                DateTime time = StrTimeToDateTime.StrToDateTime(strtime);

                // 获取指定产品的数据
                ProducttProductionRecord[] productRocords = _dbOperation.DBGetObjs<ProducttProductionRecord>(
                  e => e.ProductNumber == "A-XH-GHTY0215");

                // 2.统计分析
                // 2.1统计time内产量 合格率  故障率
                int goods = 0;
                for (int i = 0; i < productRocords.Length; i++)
                {
                    if (true == productRocords[i].QualityInspectionResult)
                    {
                        goods++;
                    }
                }

                // 删除测试
                _dbOperation.DBDeleteObj<ProducttProductionRecord>(e => e.Param.Temperature > 20.4f);

                // 修改测试
                _dbOperation.EditObj<ProducttProductionRecord>(e => e.Param.Temperature == 20.1f, e => e.SetProperty(p => p.Param.Humidity, 99.5f));

                return new LineProductionRecord() { ProductsRate = new CS.Database.Structs.ProductQualifiedRate() { Product1Rate = 0.5f } };
            });
        }
    }
}

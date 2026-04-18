using CS.Database.Entity;

namespace CSK.Core.Services
{
    /// <summary>
    /// 产品服务接口
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// 处理产品动态数据
        /// </summary>
        /// <param name="product">产品生产记录</param>
        void HandleProductDynamicData(ProducttProductionRecord product);
        
        /// <summary>
        /// 统计指定时间范围内的数据
        /// </summary>
        /// <param name="hours">小时数</param>
        /// <returns>生产线生产记录</returns>
        Task<LineProductionRecord> StatisticalData(float hours = 12);
    }
}

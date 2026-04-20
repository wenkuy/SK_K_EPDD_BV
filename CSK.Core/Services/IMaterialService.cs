using CS.Database.Entity;

namespace CSK.Core.Services
{
    /// <summary>
    /// 物料服务接口
    /// </summary>
    public interface IMaterialService
    {
        /// <summary>
        /// 处理物料动态数据
        /// </summary>
        /// <param name="material">物料记录</param>
        void HandleMaterialDynamicData(MaterialRecord material);
        
        /// <summary>
        /// 获取物料使用统计数据
        /// </summary>
        /// <param name="hours">统计时间范围（小时）</param>
        /// <returns>物料使用统计</returns>
        System.Collections.Generic.Dictionary<string, int> GetMaterialUsageStatistics(float hours = 12);
    }
}

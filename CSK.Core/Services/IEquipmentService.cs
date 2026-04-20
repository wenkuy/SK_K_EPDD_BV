using CS.Database.Entity;

namespace CSK.Core.Services
{
    /// <summary>
    /// 设备服务接口
    /// </summary>
    public interface IEquipmentService
    {
        /// <summary>
        /// 处理设备状态动态数据
        /// </summary>
        /// <param name="equipment">设备状态记录</param>
        void HandleEquipmentStateDynamicData(EquipmentStateRecord equipment);
        
        /// <summary>
        /// 获取设备状态统计数据
        /// </summary>
        /// <param name="hours">统计时间范围（小时）</param>
        /// <returns>设备状态统计</returns>
        System.Collections.Generic.Dictionary<string, int> GetEquipmentStateStatistics(float hours = 12);
    }
}

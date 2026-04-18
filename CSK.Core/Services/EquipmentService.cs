using CS.Database.Entity;
using Microsoft.Extensions.Logging;

namespace CSK.Core.Services
{
    /// <summary>
    /// 设备服务实现
    /// </summary>
    public class EquipmentService : IEquipmentService
    {
        private readonly ILogger<EquipmentService> _logger;
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志记录器</param>
        public EquipmentService(ILogger<EquipmentService> logger)
        {
            _logger = logger;
        }
        
        /// <summary>
        /// 处理设备状态动态数据
        /// </summary>
        /// <param name="equipment">设备状态记录</param>
        public void HandleEquipmentStateDynamicData(EquipmentStateRecord equipment)
        {
            // 这里可以添加业务逻辑处理
            _logger.LogInformation($"处理设备状态数据: EquipmentNumber={equipment.EquipmentNumber}, State={equipment.EquipmentState}");
        }
    }
}

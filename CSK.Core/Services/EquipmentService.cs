using CS.Database.Entity;
using CS.Database.Enum;
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
    /// 设备服务实现
    /// </summary>
    public class EquipmentService : IEquipmentService
    {
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly ILogger<EquipmentService> _logger;
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="equipmentRepository">设备仓储接口</param>
        /// <param name="logger">日志记录器</param>
        public EquipmentService(IEquipmentRepository equipmentRepository, ILogger<EquipmentService> logger)
        {
            _equipmentRepository = equipmentRepository;
            _logger = logger;
        }
        
        /// <summary>
        /// 处理设备状态动态数据
        /// </summary>
        /// <param name="equipment">设备状态记录</param>
        public void HandleEquipmentStateDynamicData(EquipmentStateRecord equipment)
        {
            _logger.LogInformation($"处理设备状态数据: EquipmentNumber={equipment.EquipmentNumber}, State={equipment.EquipmentState}");
        }
        
        /// <summary>
        /// 获取设备状态统计数据
        /// </summary>
        /// <param name="hours">统计时间范围（小时）</param>
        /// <returns>设备状态统计</returns>
        public System.Collections.Generic.Dictionary<string, int> GetEquipmentStateStatistics(float hours = 12)
        {
            // 通过仓储层获取原始数据
            var records = _equipmentRepository.GetEquipmentStateRecords(string.Empty);

            if (records == null || records.Length == 0)
            {
                _logger.LogInformation("没有找到设备状态记录");
                return new Dictionary<string, int>();
            }

            // 在业务服务层进行统计分析
            var statistics = records
                .GroupBy(e => e.EquipmentState)
                .ToDictionary(
                    g => g.Key.ToString(),
                    g => g.Count()
                );

            _logger.LogInformation($"设备状态统计完成: 状态数={statistics.Count}");
            return statistics;
        }
    }
}
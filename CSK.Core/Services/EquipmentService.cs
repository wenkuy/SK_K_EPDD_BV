using CS.Database.Entity;
using CSK.Core.QueryService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CSK.Core.Services
{
    public class EquipmentService : IEquipmentService
    {
        private readonly IEquipmentQueryService _equipmentQueryService;
        private readonly ILogger<EquipmentService> _logger;

        public EquipmentService(IEquipmentQueryService equipmentQueryService, ILogger<EquipmentService> logger)
        {
            _equipmentQueryService = equipmentQueryService;
            _logger = logger;
        }

        public void HandleEquipmentStateDynamicData(EquipmentStateRecord equipment)
        {
            _logger.LogInformation($"处理设备状态数据: EquipmentNumber={equipment.EquipmentNumber}, State={equipment.EquipmentState}");
        }

        public Dictionary<string, int> GetEquipmentStateStatistics(float hours = 12)
        {
            return _equipmentQueryService.GetEquipmentStateStatistics(hours);
        }
    }
}
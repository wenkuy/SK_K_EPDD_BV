using CS.Database.Entity;
using CS.Database.Enum;
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
    /// 设备仓储实现
    /// </summary>
    public class EquipmentRepository : Repository<EquipmentStateRecord>, IEquipmentRepository
    {
        private readonly ILogger<EquipmentRepository> _logger;

        public EquipmentRepository(CSK_DBContext dbContext, ILogger<EquipmentRepository> logger) : base(dbContext, logger)
        {
            _logger = logger;
        }

        public EquipmentStateRecord[] GetEquipmentStateRecords(string equipmentNumber)
        {
            try
            {
                var records = GetAll(e => e.EquipmentNumber == equipmentNumber);
                _logger.LogInformation($"获取设备状态记录完成: 设备编号={equipmentNumber}, 记录数={records?.Length ?? 0}");
                return records;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"获取设备状态记录失败: 设备编号={equipmentNumber}");
                return null;
            }
        }
    }
}
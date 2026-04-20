using CS.Database.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CS.Database.Repository
{
    /// <summary>
    /// 设备仓储接口
    /// </summary>
    public interface IEquipmentRepository : IRepository<EquipmentStateRecord>
    {
        /// <summary>
        /// 获取设备状态记录
        /// </summary>
        /// <param name="equipmentNumber">设备编号</param>
        /// <returns></returns>
        EquipmentStateRecord[] GetEquipmentStateRecords(string equipmentNumber);
    }
}
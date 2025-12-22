using CS_K_WPF.Base.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_K_WPF.model
{
    public class EquipmentStateRecord
    {
        public int Id { get; set; }
        /// <summary>
        /// 记录时间
        /// </summary>
        public string RecordTime { get; set; } = string.Empty;
        /// <summary>
        /// 设备编号
        /// </summary>
        public string EquipmentNumber { get; set; } = string.Empty; 

        /// <summary>
        /// 设备状态
        /// </summary>
        public EEquipmentState EquipmentState { get; set; }

        /// <summary>
        /// 故障代码(0；无错误)
        /// </summary>
        public EEquipmentErrorCode ErrorCode { get; set; }
    }
}

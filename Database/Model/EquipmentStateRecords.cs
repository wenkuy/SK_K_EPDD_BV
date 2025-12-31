
using Database.Enum;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Model
{
    [ProtoContract]
    public class EquipmentStateRecord
    {
        [ProtoMember(1)]
        public int Id { get; set; }
        /// <summary>
        /// 记录时间
        /// </summary>
        [ProtoMember(2)]
        public string RecordTime { get; set; } = string.Empty;
        /// <summary>
        /// 设备编号
        /// </summary>
        [ProtoMember(3)]
        public string EquipmentNumber { get; set; } = string.Empty;

        /// <summary>
        /// 设备状态
        /// </summary>
        [ProtoMember(4)]
        public EEquipmentState EquipmentState { get; set; }

        /// <summary>
        /// 故障代码(0；无错误)
        /// </summary>
        [ProtoMember(5)]
        public EEquipmentErrorCode ErrorCode { get; set; }
    }
}

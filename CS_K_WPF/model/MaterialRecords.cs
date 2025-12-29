using CS_K_WPF.Structs;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_K_WPF.model
{

    [ProtoContract]
    public class MaterialRecord
    {
        /// <summary>
        /// 原材料ID
        /// </summary>
        [ProtoMember(1)]
        public int Id { get; set; }
        /// <summary>
        /// 记录时间
        /// </summary>
        [ProtoMember(2)]
        public string RecordTime { get; set; } = string.Empty;
        /// <summary>
        /// 原材料编号
        /// </summary>
        [ProtoMember(3)]
        public string MaterialNumber { get; set; } = string.Empty;
        /// <summary>
        /// 质检参数
        /// </summary>
        [ProtoMember(4)]
        public QualityControl QualityPArams { get; set; }
        /// <summary>
        /// 原材料批次（备用）
        /// </summary>
        [ProtoMember(5)]
        public string Batch { get; set; } = string.Empty;
    }
}

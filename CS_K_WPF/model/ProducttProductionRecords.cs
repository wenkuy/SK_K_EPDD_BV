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
    public class ProducttProductionRecord
    {
        /// <summary>
        /// 员工id号
        /// </summary>
        [ProtoMember(1)]
        public int EmployeeId { get; set; }

        /// <summary>
        /// 商品编号
        /// </summary>
        [ProtoMember(2)]
        public string ProductNumber { get; set; } = "";

        /// <summary>
        /// 该商品的生产耗时
        /// </summary>
        [ProtoMember(3)]
        public float TimeConsumed { get; set; }
        /// <summary>
        /// 商品质检结果
        /// </summary>
        [ProtoMember(4)]
        public bool QualityInspectionResult { get; set; }

        /// <summary>
        /// 生产环境参数
        /// </summary>
        [ProtoMember(5)]
        public ProductEnvironmentPraram Praram { get; set; }

    }
}

using CS.Database.Structs;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS.Database.Entity
{

    [ProtoContract]
    public class ProducttProductionRecord 
    {
        [ProtoMember(1)]
        public int Id { get; set; } 
        /// <summary>
        /// 员工id号
        /// </summary>
        [ProtoMember(2)]
        public int EmployeeId { get; set; }

        /// <summary>
        /// 商品编号
        /// </summary>
        [ProtoMember(3)]
        public string ProductNumber { get; set; } = "";

        /// <summary>
        /// 该商品的生产耗时
        /// </summary>
        [ProtoMember(4)]
        public float TimeConsumed { get; set; }
        /// <summary>
        /// 商品质检结果
        /// </summary>
        [ProtoMember(5)]
        public bool QualityInspectionResult { get; set; }

        /// <summary>
        /// 生产时间
        /// </summary>
        [ProtoMember(6)]
        public string ProductTime { get; set; }

        /// <summary>
        /// 生产环境参数
        /// </summary>
        [ProtoMember(7)]
        public ProductEnvironmentPraram Param { get; set; }

    }
}

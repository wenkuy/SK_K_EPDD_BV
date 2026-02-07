using CS.Database.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS.Database.Entity
{
    /// <summary>
    /// 产线生产记录
    /// </summary>
    public class LineProductionRecord
    {
        public int ID { get; set; }

        public int EmployeeID { get; set; }

        /// <summary>
        /// 该商品的生产耗时
        /// </summary>
        public float ProductConsumed { get; set; }

        /// <summary>
        /// 各商品的产量
        /// </summary>
        public ProductOutput ProductsOutput { get; set; }

        /// <summary>
        /// 各商品的合格率
        /// </summary>
        public ProductQualifiedRate ProductsRate { get; set; }
    }
}

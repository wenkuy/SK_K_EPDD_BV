using CS_K_WPF.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_K_WPF.model
{
    public class ProducttProductionRecord
    {
        /// <summary>
        /// 员工id号
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// 商品编号
        /// </summary>
        public string ProductNumber { get; set; } = "";


        /// <summary>
        /// 商品产出时间
        /// </summary>
        public string ProductOutputTime { get; set; } = "";

        /// <summary>
        /// 商品质检结果
        /// </summary>
        public bool QualityInspectionResult { get; set; }

        /// <summary>
        /// 生产环境参数
        /// </summary>
        public ProductEnvironmentPraram Praram { get; set; }

    }
}

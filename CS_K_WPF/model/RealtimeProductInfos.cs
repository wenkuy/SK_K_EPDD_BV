using CS_K_WPF.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_K_WPF.model
{
    public class RealtimeProductInfo
    {

        public int ID { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public string Time { get; set; } = "";
        /// <summary>
        /// 设备编号
        /// </summary>
        public string EquipmentNumber { get; set; } = "";
        /// <summary>
        /// 原材料编号
        /// </summary>
        public string MaterialNumber { get; set; } = "";

        /// <summary>
        /// 商品编号
        /// </summary>
        public string ProductNumber{ get; set; } = "";
        
        /// <summary>
        /// 生产环境参数
        /// </summary>
        public ProductEnvironmentPraram Param { get; set; }
        
    }
}

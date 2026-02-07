using Database.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entity
{
  public  class Equipments
    {
        /// <summary>
        /// 设备类型
        /// </summary>
        public EEquipmentType EquipmentType { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        public string EquipmentNumber { get; set; } = "";   
        /// <summary>
        /// 设备购买日期
        /// </summary>
        public string PurchaseTime { get; set; } = "";     
        /// <summary>
        /// 服务年限
        /// </summary>
        public byte ServerLife { get; set; }
        /// <summary>
        /// 设备的生产产地
        /// </summary>
        public string PlaceOfOrigin { get; set; } = string.Empty;

    }
}

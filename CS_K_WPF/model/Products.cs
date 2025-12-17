using CS_K_WPF.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_K_WPF.model
{
    public class Product
    {
        /// <summary>
        /// 商品类型
        /// </summary>
       public int PType { get; set; }
        /// <summary>
        /// 商品编号
        /// </summary>
        public string PTypeNumber { get; set; } = "";
        /// <summary>
        /// 商品名称
        /// </summary>
        public string Name { get; set; } = "";
        /// <summary>
        /// 商品参数
        /// </summary>
        public ProductParam Param { get; set; }
    }
}

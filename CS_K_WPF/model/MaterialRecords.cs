using CS_K_WPF.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_K_WPF.model
{
    public class MaterialRecord
    {
        /// <summary>
        /// 原材料ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 记录时间
        /// </summary>
        public string RecordTime { get; set; } = string.Empty;
        /// <summary>
        /// 原材料编号
        /// </summary>
        public string MaterialNumber { get; set; } = string.Empty;
        /// <summary>
        /// 质检参数
        /// </summary>
        public QualityControl QualityPArams { get; set; }
        /// <summary>
        /// 原材料批次（备用）
        /// </summary>
        public string Batch { get; set; } = string.Empty;
    }
}

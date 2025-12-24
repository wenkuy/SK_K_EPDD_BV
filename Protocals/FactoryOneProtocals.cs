using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Protocals
{
    /// <summary>
    /// 厂房1 设备协议类型头部内容
    /// </summary>
    public class FactoryOneProtocalHeaher
    {
        public ushort Start; //开始
        public byte ProtocalType; // 协议类型
        public byte ProtocalVersion; // 工厂协议版本
        public string EquipmentType; // 设备类型
        public int EquipmentNumber;  //设备编号
    }
}

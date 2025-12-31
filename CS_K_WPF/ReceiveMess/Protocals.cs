using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_K_WPF
{
    public class Protocals
    {
        /// <summary>
        /// 厂房1 设备协议类型头部内容
        /// </summary>
        [ProtoContract]
        public class FactoryOneProtocalHeaher
        {
            [ProtoMember(1)]
            public ushort Start; //开始
            [ProtoMember(2)]
            public byte ProtocalType; // 协议类型
            [ProtoMember(3)]
            public byte ProtocalVersion; // 工厂协议版本
            [ProtoMember(4)]
            public string EquipmentType; // 设备类型
            [ProtoMember(5)]
            public int EquipmentNumber;  //设备编号
            [ProtoMember(6)]
            public byte MessType;  //消息类型
        }
    }
}

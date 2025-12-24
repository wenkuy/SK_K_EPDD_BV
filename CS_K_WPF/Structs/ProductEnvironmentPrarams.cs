using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_K_WPF.Structs
{

    /// <summary>
    /// 生产的环境参数
    /// </summary>
    [ProtoContract]
    public struct ProductEnvironmentPraram
    {
        [ProtoMember(1)]
        public float Temperature { get; set; }

        [ProtoMember(2)]
        public float Humidity { get; set; }
    }
}

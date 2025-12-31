using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Structs
{

    /// <summary>
    /// 生产的环境参数
    /// </summary>
    [ProtoContract]
    public class ProductEnvironmentPraram
    {
        [ProtoMember(1)]
        public float Temperature { get; set; }

        [ProtoMember(2)]
        public float Humidity { get; set; }
    }
}

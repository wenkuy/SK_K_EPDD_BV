using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS.Database.Enum
{
    [ProtoContract]
    public enum EEquipmentState
    {
        [ProtoMember(1)]
        Idle = 0,
        [ProtoMember(2)]
        Running =1, // 运行
        [ProtoMember(3)]
        Faulty =2,   // 故障
    }
}

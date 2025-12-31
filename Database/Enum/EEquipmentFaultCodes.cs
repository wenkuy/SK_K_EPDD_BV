using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Enum
{
    [ProtoContract]
    public enum EEquipmentErrorCode
    {
        [ProtoMember(1)]
        NoErrt =0,
        [ProtoMember(2)]
        VisionInspectionErr =1,
        [ProtoMember(3)]
        MotorErr =2,
        [ProtoMember(4)]
        ConveyorErr =3,
        [ProtoMember(5)]
        SafetyProtectionTriggered =4,
    }
}

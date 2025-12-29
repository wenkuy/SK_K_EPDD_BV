using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_K_WPF.Structs
{
    [ProtoContract]
    public struct QualityControl
    {
        [ProtoMember(1)]
        public bool IsQualified { get; set; }
       
        /// <summary>
        /// 是否变形
        /// </summary>
        [ProtoMember(2)]
        public bool IsDeformed { get; set; }
        /// <summary>
        /// 是否毛刺
        /// </summary>
        [ProtoMember(3)]
        public bool IsBurred { get; set; }
        [ProtoMember(4)]
        public float Weight { get; set; }
        [ProtoMember(5)]
        public float Height { get; set; }
        [ProtoMember(6)]
        public float Width { get; set; }
    }
}

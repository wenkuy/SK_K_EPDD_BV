using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSK.Core
{
    public class OpenAnalysisWindowMes { }
    public class UdpDataForProducttProductionRecordMes
    {
       public byte[] Content;
        public UdpDataForProducttProductionRecordMes(byte[] content)
        {
            Content = content;
        }
    }

    public class UdpDataForEquipmentStateRecordMes
    {
        public byte[] Content;
        public UdpDataForEquipmentStateRecordMes(byte[] content)
        {
            Content = content;
        }
    }
    public class UdpMaterialRecordMes
    {
        public byte[] Content;
        public UdpMaterialRecordMes(byte[] content)
        {
            Content = content;
        }
    }
}

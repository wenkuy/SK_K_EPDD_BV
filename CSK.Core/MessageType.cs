using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSK.Core
{
    public class OpenAnalysisWindowMes { }
    public class OpenExceptionWindowMes 
    {
        public OpenExceptionWindowMes(string Title, string ExpMesForUser, string ExpMesForDeveloper)
        {
            this.Title = Title;
            this.ExpMesForUser = ExpMesForUser;
            this.ExpMesForDeveloper = ExpMesForDeveloper;
        }
        public string Title;
        public string ExpMesForUser;
        public string ExpMesForDeveloper;

    }
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

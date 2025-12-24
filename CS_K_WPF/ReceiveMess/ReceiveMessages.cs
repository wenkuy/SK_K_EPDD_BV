using CS_K_WPF.model;
using Protocals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdpSenderProj;

namespace CS_K_WPF.ReceiveMess
{
    public class ReceiveMessages
    {
        public ReceiveMessages()
        {
            UdpReceive.UDPMessAction = DelMessage;
        }



        public void DelMessage(byte[] bytes)
        {
            ProducttProductionRecord kkk =  ProtocolParsing.Deserialize<ProducttProductionRecord>(bytes);
        }

    }
}

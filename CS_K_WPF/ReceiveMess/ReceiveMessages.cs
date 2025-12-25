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
            //1.去掉UDPMes的头部
            //2.解析：将字节数组转类对象
            ProducttProductionRecord kkk =  ProtocolParsing.Deserialize<ProducttProductionRecord>(bytes);
            ModelLocator.Instance.LocHomePageVM.GetMesData(kkk);
        }

    }
}

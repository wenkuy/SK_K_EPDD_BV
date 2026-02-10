using CommunityToolkit.Mvvm.Messaging;
using CS.Base;
using CS.Communication;
using CSK.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static CS.Communication.Protocals;


namespace CS.Communication
{
    public class ReceiveMessages
    {
        private ILogger<ReceiveMessages> _logger;
        public ReceiveMessages(ILogger<ReceiveMessages> logger)
        {
            UdpReceiver.UDPMessAction = DelMessage;
            _logger = logger;
        }

        public void DelMessage(byte[] bytes)
        {
            // 1.提取头部信息
            byte headerNum = 17;
            FactoryOneProtocalHeaher header = new FactoryOneProtocalHeaher();
            byte[] headerBytes = bytes.Skip(0).Take(headerNum).ToArray();
            header = ProtocolParsing.Deserialize<FactoryOneProtocalHeaher>(headerBytes);
            //2.提取Mes内容 去掉UDPMes的头部
            byte[] content = bytes.Skip(headerNum).ToArray();

            //通过message发送消息到对应ViewModel，解耦合度高，方便维护和扩展
            switch (header.MessType)
            {
                case 1:
                    WeakReferenceMessenger.Default.Send(new UdpDataForProducttProductionRecordMes(content));
                    break;

                case 2:
                    WeakReferenceMessenger.Default.Send(new UdpDataForEquipmentStateRecordMes(content));
                    break;

                case 3:
                    WeakReferenceMessenger.Default.Send(new UdpMaterialRecordMes(content));
                    break;

                default:
                    break;
            }

        }

    }
}

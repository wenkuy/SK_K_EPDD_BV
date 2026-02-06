using Database;
using Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdpSenderProj;
using static CS_K_WPF.Protocals;

namespace CS_K_WPF
{
    public class ReceiveMessages
    {
        public ReceiveMessages()
        {
            UdpReceive.UDPMessAction = DelMessage;
        }


        public void DelMessage(byte[] bytes)
        {
            // 1.提取头部信息
            byte headerNum = 17;
            FactoryOneProtocalHeaher header = new FactoryOneProtocalHeaher();
            byte[] headerBytes = bytes.Skip(0).Take(headerNum).ToArray();
            header = ProtocolParsing.Deserialize<FactoryOneProtocalHeaher>(headerBytes);

            //2.提取Mes内容
            //去掉UDPMes的头部
            byte[] content = bytes.Skip(headerNum).ToArray();

            ProducttProductionRecord productInfo;
            MaterialRecord MaterialInfo;
            EquipmentStateRecord equipmentStateInfo;
            switch (header.MessType)
            {
                case 1: //商品信息
                    productInfo = ProtocolParsing.Deserialize<ProducttProductionRecord>(content);
                    ModelLocator.Instance.LocHomePageVM.Udp_ProductDates(productInfo);
                    DatebaseServiceProvider.DatabaseServicesProvider().DBSaveObj(productInfo); //保存到数据库

                    break;
                case 2: //设备状态信息
                    equipmentStateInfo = ProtocolParsing.Deserialize<EquipmentStateRecord>(content);
                    ModelLocator.Instance.LocHomePageVM.Udp_EquipmentRecodeDates(equipmentStateInfo);
                    break;
                case 3: //原料信息
                    MaterialInfo = ProtocolParsing.Deserialize<MaterialRecord>(content);
                    ModelLocator.Instance.LocHomePageVM.Udp_MaterialDates(MaterialInfo);
                    DatebaseServiceProvider.DatabaseServicesProvider().DBSaveObj(MaterialInfo);//保存到数据库
                    break;

                default:
                    break;
            }

        }

    }
}

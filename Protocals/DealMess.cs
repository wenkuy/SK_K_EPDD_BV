using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdpSenderProj;

namespace Protocals
{
    public class DealMess
    {

        public DealMess()
        {
            UdpReceive.UDPMessAction = DealwithUDPDates;
        }


        public void DealwithUDPDates(byte[] bytes)
        {

        }


    }
}

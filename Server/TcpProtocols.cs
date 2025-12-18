using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPServer
{
    /// <summary>
    /// TCP通讯的报文协议
    /// </summary>
    public class TcpProtocol
    {
        public string Header;
        public ETCPCommand TcpCmd;
        public string EquipmentNumber;
        public string value;
        public string End;

    }
}

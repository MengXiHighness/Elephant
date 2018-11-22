using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.AFP.Communication.SocketNameSpace
{
    public class UdpClient : SocketClientBase
    {
        public UdpClient(string serverIp, int serverPort)
            : base(serverIp, serverPort, ProtocolTypes.UDP)
        {
        }
    }
}

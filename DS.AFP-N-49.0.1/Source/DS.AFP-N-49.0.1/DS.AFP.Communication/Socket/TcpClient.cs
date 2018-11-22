using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.AFP.Communication.SocketNameSpace
{
    public class TcpClient : SocketClient
    {
        public TcpClient(string serverIp, int serverPort)
            : this(serverIp, serverPort, ProtocolTypes.TCP, Encoding.UTF8, new PackageProtocol())
        {
        }

        public TcpClient(string serverIp, int serverPort, ProtocolTypes protocolType, Encoding encoding, IPackageProtocol<ScoketPackage> protocol) 
            : base(serverIp, serverPort, protocolType, encoding, protocol)
        {
        }
    }
}

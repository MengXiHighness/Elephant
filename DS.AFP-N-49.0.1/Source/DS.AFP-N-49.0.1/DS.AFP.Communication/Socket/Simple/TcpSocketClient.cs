using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DS.AFP.Common.Core;
using DS.AFP.Communication.Chain;




namespace DS.AFP.Communication.SocketNameSpace
{
    /// <summary>
    /// TCP客户端类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TcpSocketClient<T> : SocketClient<T>, IDisposable where T : SocketPackage, new()
    {
        public TcpSocketClient(string serverIP, int serverPort, IPackageProtocol<T> protocol, int packageSize = 1024*1024)
            : base(serverIP, serverPort,protocol,ProtocolType.Tcp,packageSize)
        {
        }
    }
}

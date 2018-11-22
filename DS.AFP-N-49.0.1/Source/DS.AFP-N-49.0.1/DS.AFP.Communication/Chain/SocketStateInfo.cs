using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using DS.AFP.Communication.Chain;

namespace DS.AFP.Communication.SocketNameSpace
{
    public class SocketStateInfo:EventArgs
    {
        public SocketStateInfo( Socket socket)
        {
            this.Socket = socket;
            Package = new byte[1024 * 1024];
        }

        public byte[] Package { get; set; }

        public Socket Socket { get; private set; }

        public ChainPackage ToChainPackage()
        {
            ChainProtocol cpcl = new ChainProtocol();
            return cpcl.ResolveProtocol(Package);
        }
    }

  
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.AFP.Communication.SocketNameSpace
{
    public class SimpleSocketPackageParser : ISocketPackageParser<SocketPackage>
    {
        public SocketPackage ParseRequestInfo(byte[] source)
        {
            SimpleSocketPackageProtocol spp = new SimpleSocketPackageProtocol();
            return spp.ResolveProtocol(source);

        }
    }
}

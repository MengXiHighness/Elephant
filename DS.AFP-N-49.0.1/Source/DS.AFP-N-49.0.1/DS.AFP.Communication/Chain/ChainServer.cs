using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.AFP.Communication.SocketNameSpace;

namespace DS.AFP.Communication.Chain
{
    /// <summary>
    /// Chain服务
    /// </summary>
    public class ChainServer : AppServer<ChainSession,ChainPackage>
    {
        public ChainServer()
            : base( new ChainReceiveFilterFactory(Encoding.UTF8, new ChainRequestInfoParser()))
        {

        }
       
    }
}

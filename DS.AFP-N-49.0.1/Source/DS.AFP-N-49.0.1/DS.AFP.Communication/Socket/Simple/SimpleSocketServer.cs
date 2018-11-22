using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.AFP.Communication.SocketNameSpace;

namespace DS.AFP.Communication.SocketNameSpace
{
    /// <summary>
    /// Socket服务
    /// </summary>
    public class SimpleSocketServer : AppServer<SimpleSocketSession,SocketPackage>
    {
		public SimpleSocketServer()
			: base(new SimpleSocketReceiveFilterFactory(Encoding.UTF8, new SimpleSocketRequestInfoParser()))
        {

        }
       
    }
}

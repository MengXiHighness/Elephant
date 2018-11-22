using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using DS.AFP.Communication.Common;
using DS.AFP.Communication.SocketBase;
using DS.AFP.Communication.SocketBase.Logging;
using DS.AFP.Communication.SocketEngine.AsyncSocket;

namespace DS.AFP.Communication.SocketEngine
{
    interface IAsyncSocketSessionBase : ILoggerProvider
    {
        SocketAsyncEventArgsProxy SocketAsyncProxy { get; }
        
        Socket Client { get; }
    }

    interface IAsyncSocketSession : IAsyncSocketSessionBase
    {
        void ProcessReceive(SocketAsyncEventArgs e);
    }
}

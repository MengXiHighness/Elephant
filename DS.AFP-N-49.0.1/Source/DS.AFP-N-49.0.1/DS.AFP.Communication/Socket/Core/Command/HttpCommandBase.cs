using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.AFP.Communication.SocketNameSpace;
using DS.AFP.Communication.Http;
using DS.AFP.Communication.SocketBase.Command;
using DS.AFP.Communication.SocketBase;

namespace DS.AFP.Communication
{
    public abstract class HttpCommandBase<TAppSession> : CommandBase<HttpSession, HttpRequestInfo>
        where TAppSession : IAppSession, IAppSession<HttpSession, HttpRequestInfo>, new()
    {
        //StringRequestInfo
    }
}

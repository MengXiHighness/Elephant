using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.AFP.Communication.SocketNameSpace;
using DS.AFP.Communication.SocketBase;
using DS.AFP.Communication.SocketBase.Protocol;
using DS.AFP.Communication.Http;

namespace DS.AFP.Communication
{
    /// <summary>
    /// 通用服务
    /// </summary>
    public class HttpServer : AppServer<HttpSession, HttpRequestInfo>
    {
        public HttpServer()
            : base(new DefaultReceiveFilterFactory<HttpReceiveFilter,HttpRequestInfo>())
        {
        }
       
    }
}

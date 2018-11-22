using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.AFP.Communication.SocketNameSpace;
using DS.AFP.Communication.SocketBase.Protocol;
using DS.AFP.Communication.SocketBase;
using DS.AFP.Communication.Http;

namespace DS.AFP.Communication
{
    /// <summary>
    /// 常用会话类
    /// </summary>
    public class HttpSession : AppSession<HttpSession, HttpRequestInfo>//AppSession<CommonSession>
    {

        protected override void OnSessionStarted()
        {
        }


        protected override void HandleUnknownRequest(HttpRequestInfo requestInfo)
        {
            this.Send("Unknow request");
        }

        protected override void HandleException(Exception e)
        {
            this.Send("Application error: {0}", e.Message);
        }

        /// <summary>
        /// 会话结束时回调
        /// </summary>
        /// <param name="reason">会话结束原因</param>
        internal protected override void OnSessionClosed(CloseReason reason)
        {
            base.OnSessionClosed(reason);
        }
    }
}

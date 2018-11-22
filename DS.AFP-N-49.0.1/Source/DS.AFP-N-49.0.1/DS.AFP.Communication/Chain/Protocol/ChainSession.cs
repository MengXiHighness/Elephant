using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.AFP.Communication.SocketNameSpace;
using DS.AFP.Communication.SocketBase;

namespace DS.AFP.Communication.Chain
{
    /// <summary>
    /// Chain会话类
    /// </summary>
    public class ChainSession : AppSession<ChainSession, ChainPackage>
    {

        protected override void OnSessionStarted()
        {
        }

        //protected override void HandleUnknownRequest(HttpRequest requestInfo)
        //{
        //    this.Send("Unknow request");
        //}

        protected override void HandleException(Exception e)
        {
            this.Send("Application error: {0}", e.Message);
        }

        public override void OnSessionClosed(CloseReason reason)
        {
            base.OnSessionClosed(reason);
        }
    }


}

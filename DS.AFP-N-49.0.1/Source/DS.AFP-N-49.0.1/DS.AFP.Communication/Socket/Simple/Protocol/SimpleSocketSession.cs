using DS.AFP.Communication.SocketBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.AFP.Communication.SocketNameSpace
{
	
    /// <summary>
    /// Socket会话类
    /// </summary>
	public class SimpleSocketSession : AppSession<SimpleSocketSession, SocketPackage>
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

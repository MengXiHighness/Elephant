using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.AFP.Communication.SocketNameSpace;
using DS.AFP.Communication.Http;
using DS.AFP.Communication.Chain;
using DS.AFP.Communication.SocketBase;
using DS.AFP.Communication.SocketBase.Command;


namespace DS.AFP.Communication.SocketNameSpace.Command
{
    /// <summary>
    /// Socket指令基类
    /// </summary>
    /// <typeparam name="TAppSession"></typeparam>
	public abstract class SimpleSocketCommandBase<TAppSession> : CommandBase<TAppSession, SocketPackage>
		where TAppSession : IAppSession, IAppSession<TAppSession, SocketPackage>, new()
    {
        
    }
}

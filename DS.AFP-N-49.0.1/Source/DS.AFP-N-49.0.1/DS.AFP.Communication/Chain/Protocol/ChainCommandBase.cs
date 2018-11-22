using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.AFP.Communication.SocketNameSpace;
using DS.AFP.Communication.Http;
using DS.AFP.Communication.Chain;
using DS.AFP.Communication.SocketBase;

namespace DS.AFP.Communication.SocketBase.Command
{
    /// <summary>
    /// 命令基类
    /// </summary>
    /// <typeparam name="TAppSession"></typeparam>
    public abstract class ChainCommandBase<TAppSession> : CommandBase<TAppSession, ChainPackage>
        where TAppSession : IAppSession, IAppSession<TAppSession, ChainPackage>, new()
    {
        
    }
}

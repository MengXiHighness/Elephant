using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using DS.AFP.Communication.SocketBase.Command;
using DS.AFP.Communication.SocketBase.Config;
using DS.AFP.Communication.SocketBase.Protocol;

namespace DS.AFP.Communication.SocketBase
{
    /// <summary>
    /// The interface for socket server factory
    /// </summary>
    public interface ISocketServerFactory
    {
        /// <summary>
        /// Creates the socket server instance.
        /// </summary>
        /// <typeparam name="TRequestInfo">The type of the request info.</typeparam>
        /// <param name="appServer">The app server.</param>
        /// <param name="listeners">The listeners.</param>
        /// <param name="config">The config.</param>
        /// <returns></returns>
        ISocketServer CreateSocketServer<TRequestInfo>(IAppServer appServer, ListenerInfo[] listeners, IServerConfig config)
            where TRequestInfo : IRequestInfo;
    }
}

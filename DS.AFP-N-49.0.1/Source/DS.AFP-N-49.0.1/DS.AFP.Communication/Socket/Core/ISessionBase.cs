﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace DS.AFP.Communication.SocketBase
{
    /// <summary>
    /// The basic session interface
    /// </summary>
    public interface ISessionBase
    {
        /// <summary>
        /// Gets the session ID.
        /// </summary>
        string SessionID { get; }

        /// <summary>
        /// Gets the remote endpoint.
        /// </summary>
        IPEndPoint RemoteEndPoint { get; }
    }
}

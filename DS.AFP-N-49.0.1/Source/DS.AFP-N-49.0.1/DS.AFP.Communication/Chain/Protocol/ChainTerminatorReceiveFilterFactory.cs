﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using DS.AFP.Communication.SocketNameSpace;
using DS.AFP.Communication.SocketBase.Protocol;

namespace DS.AFP.Communication.Chain
{
    /// <summary>
    /// Chain包后缀过滤的工厂
    /// </summary>
    public class ChainTerminatorReceiveFilterFactory : IReceiveFilterFactory<ChainPackage>
    {
        private readonly Encoding m_Encoding;
        private readonly byte[] m_Terminator;
        private readonly IRequestInfoParser<ChainPackage> m_RequestInfoParser;

        /// <summary>
        /// Initializes a new instance of the <see cref="TerminatorReceiveFilterFactory"/> class.
        /// </summary>
        /// <param name="terminator">The terminator.</param>
        public ChainTerminatorReceiveFilterFactory(string terminator)
            : this(terminator, Encoding.UTF8, new ChainRequestInfoParser())
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TerminatorReceiveFilterFactory"/> class.
        /// </summary>
        /// <param name="terminator">The terminator.</param>
        /// <param name="encoding">The encoding.</param>
        public ChainTerminatorReceiveFilterFactory(string terminator, Encoding encoding)
            : this(terminator, encoding, new ChainRequestInfoParser())
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TerminatorReceiveFilterFactory"/> class.
        /// </summary>
        /// <param name="terminator">The terminator.</param>
        /// <param name="encoding">The encoding.</param>
        /// <param name="requestInfoParser">The line parser.</param>
        public ChainTerminatorReceiveFilterFactory(string terminator, Encoding encoding, IRequestInfoParser<ChainPackage> requestInfoParser)
        {
            m_Encoding = encoding;
            m_Terminator = encoding.GetBytes(terminator);
            m_RequestInfoParser = requestInfoParser;
        }

        /// <summary>
        /// Creates the Receive filter.
        /// </summary>
        /// <param name="appServer">The app server.</param>
        /// <param name="appSession">The app session.</param>
        /// <param name="remoteEndPoint">The remote end point.</param>
        /// <returns>
        /// the new created request filer assosiated with this socketSession
        /// </returns>
        public virtual IReceiveFilter<ChainPackage> CreateFilter(IAppServer appServer, IAppSession appSession, IPEndPoint remoteEndPoint)
        {
            return new ChainTerminatorReceiveFilter(m_Terminator, m_Encoding, m_RequestInfoParser);
        }
    }
}

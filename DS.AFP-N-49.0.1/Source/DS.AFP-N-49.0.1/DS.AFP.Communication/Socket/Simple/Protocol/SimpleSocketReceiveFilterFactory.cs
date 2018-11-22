using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.AFP.Communication.SocketNameSpace;
using DS.AFP.Communication.SocketBase.Protocol;

namespace DS.AFP.Communication.SocketNameSpace
{
    /// <summary>
    /// 接收消息包过滤器的工厂
    /// </summary>
    public class SimpleSocketReceiveFilterFactory : SimpleSocketTerminatorReceiveFilterFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandLineReceiveFilterFactory"/> class.
        /// </summary>
        public SimpleSocketReceiveFilterFactory()
            : this(Encoding.UTF8)
        {
            
        }

        ///// <summary>
        ///// Initializes a new instance of the <see cref="CommandLineReceiveFilterFactory"/> class.
        ///// </summary>
        ///// <param name="encoding">The encoding.</param>
        public SimpleSocketReceiveFilterFactory(Encoding encoding)
            : this(encoding, new SimpleSocketRequestInfoParser())
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandLineReceiveFilterFactory"/> class.
        /// </summary>
        /// <param name="encoding">The encoding.</param>
        /// <param name="requestInfoParser">The request info parser.</param>
		public SimpleSocketReceiveFilterFactory(Encoding encoding, IRequestInfoParser<SocketPackage> requestInfoParser)
            : base(SocketParams.Terminator, encoding, requestInfoParser)
        {

        }
    }
}

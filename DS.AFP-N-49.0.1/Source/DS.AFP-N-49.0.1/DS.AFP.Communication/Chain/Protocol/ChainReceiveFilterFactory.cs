using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.AFP.Communication.SocketNameSpace;
using DS.AFP.Communication.SocketBase.Protocol;

namespace DS.AFP.Communication.Chain
{
    /// <summary>
    /// 接收消息包过滤器的工厂
    /// </summary>
    public class ChainReceiveFilterFactory : ChainTerminatorReceiveFilterFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandLineReceiveFilterFactory"/> class.
        /// </summary>
        public ChainReceiveFilterFactory()
            : this(Encoding.UTF8)
        {
            
        }

        ///// <summary>
        ///// Initializes a new instance of the <see cref="CommandLineReceiveFilterFactory"/> class.
        ///// </summary>
        ///// <param name="encoding">The encoding.</param>
        public ChainReceiveFilterFactory(Encoding encoding)
            : this(encoding, new ChainRequestInfoParser())
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandLineReceiveFilterFactory"/> class.
        /// </summary>
        /// <param name="encoding">The encoding.</param>
        /// <param name="requestInfoParser">The request info parser.</param>
        public ChainReceiveFilterFactory(Encoding encoding, IRequestInfoParser<ChainPackage> requestInfoParser)
            : base(SocketParams.Terminator, encoding, requestInfoParser)
        {

        }
    }
}

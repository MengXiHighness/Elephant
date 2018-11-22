using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.AFP.Common.Core;
using DS.AFP.Communication.SocketNameSpace;
using DS.AFP.Communication.SocketBase.Protocol;

namespace DS.AFP.Communication.Chain
{
    /// <summary>
    /// Chain包解析器
    /// </summary>
    public class ChainRequestInfoParser : IRequestInfoParser<ChainPackage>
    {

        #region ICommandParser Members

        /// <summary>
        /// 把byte[] source解析成Chain包
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public ChainPackage ParseRequestInfo(byte[] source)
        {
            IPackageProtocol<ChainPackage> packageHeadProtocol = new ChainProtocol();
            return packageHeadProtocol.ResolveProtocol(source);
        }

        #endregion
    }
}

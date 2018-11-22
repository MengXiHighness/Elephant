using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.AFP.Common.Core;
using DS.AFP.Communication.SocketNameSpace;
using DS.AFP.Communication.SocketBase.Protocol;

namespace DS.AFP.Communication
{
    /// <summary>
    /// Socket包解析器
    /// </summary>
	public class SimpleSocketRequestInfoParser : IRequestInfoParser<SocketPackage>
    {

        #region ICommandParser Members

        /// <summary>
        /// 把byte[] source解析成SocketPackage
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
		public SocketPackage ParseRequestInfo(byte[] source)
        {
			IPackageProtocol<SocketPackage> packageHeadProtocol = new SimpleSocketPackageProtocol();
            return packageHeadProtocol.ResolveProtocol(source);
        }

        #endregion
    }
}

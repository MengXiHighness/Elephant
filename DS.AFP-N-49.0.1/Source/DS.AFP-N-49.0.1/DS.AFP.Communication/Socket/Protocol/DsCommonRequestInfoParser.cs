using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.AFP.Common.Core;
using DS.AFP.Communication.SocketNameSpace.Protocol;

namespace DS.AFP.Communication.SocketNameSpace
{
    /// <summary>
    /// 在已经按SocketParams.Terminator截包后的字符串，中收到有包头的信息
    /// 包头总长度为14字节
    /// 长度6位，版本号2位，标志位1位，消息号4位， 
    /// </summary>
    public class DsCommonRequestInfoParser : IRequestInfoParser<StringRequestInfo>
    {
        private readonly string defaultCommand = SocketParams.DefaultCommand;
       
        #region ICommandParser Members

        public StringRequestInfo ParseRequestInfo(string source)
        {
            PackageProtocol packageHeadProtocol = new PackageProtocol();
            ScoketPackage ph;
            /////解压
            ph = packageHeadProtocol.ResolveProtocol(source);

            
            return new StringRequestInfo(ph.Command, source,new string[0]);
        }

        #endregion
    }
}

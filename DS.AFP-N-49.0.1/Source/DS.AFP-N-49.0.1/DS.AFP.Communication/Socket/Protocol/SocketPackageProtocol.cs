using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using DS.AFP.Common.Core;
using DS.AFP.Communication.SocketNameSpace.Protocol;


namespace DS.AFP.Communication.SocketNameSpace
{
    /// <summary>
    //包头总长度为14字节
    //长度6位，版本号2位，标志位1位，消息号4位， 
    /// </summary>
    public class SocketPackageProtocol : IPackageProtocol<ScoketPackage>
    {

        public ScoketPackage ResolveProtocol(string package)
        {
            ScoketPackage ph = new ScoketPackage();
            ph.PackageBodyLength = (int)package.Substring(0, 6).Form64To10();
            ph.Version = (int)package.Substring(6, 2).Form64To10();
            ph.IsZip = (package.Substring(8, 1) == "1");
            ph.MessageNo = (int)package.Substring(9, 4).Form64To10();
            ph.CommandLength = (int)package.Substring(13, 1).Form64To10();
            return ph;
        }


        public string ResolveProtocol(ScoketPackage ph)
        {
            //包头总长度为17字节
            //长度10位，版本号2位，标志位1位，消息号4位， 
            StringBuilder packHead = new StringBuilder("{0}{1}{2}{3}{4}{5}{6}");
            string t = packHead.ToString().FormatString(ph.PackageBodyLength.To64().PadLeft(6, '0'), ph.Version.To64().PadLeft(2, '0'), Convert.ToInt32(ph.IsZip), ph.MessageNo.To64().PadLeft(4, '0'), (ph.CommandLength <= 64 ? ph.CommandLength.To64() : "Z").PadLeft(1, '0'), ph.Message, SocketParams.Terminator);
            return t;
        }
       
    }

    public class ScoketPackage:IRequestInfo<NameValueCollection, byte[]>
    {
        public ScoketPackage(string key, NameValueCollection header, byte[] body)
        {
            this.Key = key;
            this.Header = header;
            this.Body = body;
        }

        /// <summary>
        /// 消息头的键值信息
        /// </summary>
        public NameValueCollection Header
        {
            get;
            private set;
        }

        /// <summary>
        /// 消息体
        /// </summary>
        public byte[] Body
        {
            get;
            private set;
        }

        /// <summary>
        /// 消息key
        /// </summary>
        public string Key
        {
            get;
            private set;
        }
    }
}

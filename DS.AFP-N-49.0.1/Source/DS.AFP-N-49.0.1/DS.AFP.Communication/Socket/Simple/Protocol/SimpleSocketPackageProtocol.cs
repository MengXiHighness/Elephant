///by 姜宁
///2014-2-11
///

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.AFP.Common.Core;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using DS.AFP.Communication.SocketNameSpace;

namespace DS.AFP.Communication.SocketNameSpace
{
    /// <summary>
    /// Socket包协议类
    /// </summary>
    public class SimpleSocketPackageProtocol :  IPackageProtocol<SocketPackage>
    {
       
        /// <summary>
        /// 将byte[]数据转成SocketPackage
        /// <code>
        /// packageHeadProtocol.ResolveProtocol(source);//byte[] source
        /// </code>
        /// </summary>
        /// <param name="package">转换前的byte[]数据</param>
        /// <returns>转换后的SocketPackage</returns>
        public SocketPackage ResolveProtocol(byte[] package)    
        {
            SocketPackage sp = new SocketPackage();
            sp.Body = package;
            return sp;
        }

        /// <summary>
        /// 将SocketPackage数据转成byte[]
        /// <code>
        /// ChainPackage cp = new ChainPackage(Encoding.UTF8.GetBytes("1234567890"), null);
        /// byte[] readBuffer = cpcl.ResolveProtocol(cp);
        /// </code>
        /// </summary>
        /// <param name="package">转换前的SocketPackage数据</param>
        /// <returns>转换后的byte[]</returns>
        public byte[] ResolveProtocol(SocketPackage package)
        {
            return package.Body;
        }

        /// <summary>
        /// 将byte型ILIst数据转成SocketPackage型ILIst
        /// </summary>
        /// <code>
        /// byte[] t = new byte[readBuffer.Length * 2];
        /// var cps = cpcl.ResolveProtocol(t.ToList());
        /// </code>
        /// <param name="package">转换前的byte型ILIst数据</param>
        /// <returns>转换后的SocketPackage型ILIst</returns>
        public IList<SocketPackage> ResolveProtocol(IList<byte> package)
        {
            IList<SocketPackage> splist = new List<SocketPackage>();
            byte[] data = package.ToArray<byte>();
            SocketPackage sp = new SocketPackage();
            sp.Body = data;
            splist.Add(sp);
            return splist;
        }
    }
}

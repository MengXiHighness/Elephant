using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DS.AFP.Common.Core;
using DS.AFP.Communication.Chain;
using System.IO;
using System.Net;

namespace DS.AFP.Communication.SocketNameSpace
{
    /// <summary>
    /// UDP客户端类
    /// </summary>
    /// <typeparam name="T"></typeparam>
	public class UdpSocketClient<T> : SocketClient<T>, IDisposable where T : SocketPackage, new()
	{
		public UdpSocketClient(IPackageProtocol<T> protocol ,int packageSize = 1024*1024):base("127.0.0.1",0,protocol,ProtocolType.Udp,packageSize)
		{
		}

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="data">要发送的数据</param>
        /// <param name="remoteEP">远程节点</param>
        /// <returns></returns>
		public  int SendTo(byte[] data,EndPoint remoteEP)
		{
			return base.Client.SendTo(data, remoteEP);
		}

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="data">要发送的数据</param>
        /// <param name="remoteEP">远程节点</param>
        /// <returns></returns>
		public  int SendTo(T data, EndPoint remoteEP)
		{
			return base.Client.SendTo(base.protocol.ResolveProtocol(data), remoteEP);
		}

	}
}

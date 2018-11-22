using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using DS.AFP.Common.Core.Utility;
using DS.AFP.Common.Core;


namespace DS.AFP.Communication.SocketNameSpace
{
    /// <summary>
    /// Socket工厂类
    /// </summary>
    public class SocketFactory
    {
        private event EventHandler<SocketPackage> m_SetupReceiveEvent;
        private readonly Object m_eventLock = new Object();
        public event EventHandler<SocketPackage> SetupReceiveEvent
        {
            add
            {
                lock (m_eventLock)
                {
                    m_SetupReceiveEvent += value;
                }
            }
            remove
            {
                lock (m_eventLock)
                {
                    m_SetupReceiveEvent -= value;
                }
            }
        
        }

        /// <summary>
        /// 获取所需的Socket对象
        /// </summary>
        /// <param name="hostIp">远程IP</param>
        /// <param name="hostPort">远程端口</param>
        /// <param name="protocolType">协议类型</param>
        /// <returns>创建的Socket对象</returns>
        public Socket Get(string hostIp, int hostPort, ProtocolType protocolType)
        {
            //IPEndPoint localIpEndPoint = new IPEndPoint(IPAddress.Any,2013);
            IPEndPoint hostIpEndPoint = new IPEndPoint(IPAddress.Parse(hostIp), hostPort);
            return Get( hostIpEndPoint, protocolType);
        }

        //public  Socket Get(int localPort,string hostIp, int hostPort,ProtocolType protocolType)
        //{
        //    IList<IPAddress> localAdressList = NetHelper.GetLoaclAddressList().Where(o=>o.AddressFamily == AddressFamily.InterNetwork).ToList<IPAddress>();
        //    IPEndPoint hostIpEndPoint = new IPEndPoint(IPAddress.Parse(hostIp), hostPort);
        //    foreach (IPAddress address in localAdressList)
        //    {
        //       Socket client = Get(new IPEndPoint(address, localPort), hostIpEndPoint, protocolType);
        //       if (client.Connected)
        //           return client;
        //    }
        //    throw new Exception("本地所有IP和端口都不能连接服务地址{0}:{1}".FormatString(hostIp,hostPort.ToString()));
            
        //}

        /// <summary>
        /// 异步获取所需的Socket对象
        /// </summary>
        /// <param name="localIPEndPoint">本地节点</param>
        /// <param name="hostIPEndPoint">远程节点</param>
        /// <param name="protocolType">协议类型</param>
        /// <param name="packageSize">包大小</param>
        /// <returns></returns>
        public Socket AsyncGet(IPEndPoint localIPEndPoint, IPEndPoint hostIPEndPoint, ProtocolType protocolType, int packageSize = 1024*1024)
        {

            Socket client = new Socket(localIPEndPoint.Address.AddressFamily, SocketType.Stream, protocolType);
            //client.Blocking = true;
            client.Bind(localIPEndPoint);

            switch (protocolType)
            {
                case ProtocolType.Tcp:
                    {
                        client.BeginConnect(hostIPEndPoint, (ar) =>
                        {

                            Socket sk = (Socket)ar.AsyncState;
                            Socket s2 = sk.EndAccept(ar);
                            SocketPackage ssi = new SocketPackage() {  Body=new byte[packageSize], Client=s2};
                            try
                            {
                                if (sk.Connected)
                                    m_SetupReceiveEvent(this, ssi);
                            }
                            catch
                            {
                            }

                        }, client);
                        break;
                    }
                case ProtocolType.Udp:
                    {
                        break;
                    }
            }
            return client;

        }

        /// <summary>
        /// 获取所需的Socket对象
        /// </summary>
        /// <param name="hostIPEndPoint">远程节点</param>
        /// <param name="protocolType">协议类型</param>
        /// <param name="packageSize">包大小</param>
        /// <returns>创建的Socket对象</returns>
        public Socket Get( IPEndPoint hostIPEndPoint, ProtocolType protocolType,int packageSize = 1024*1024)
        {

            Socket client = null;

            //try
            //{
            //    client.Bind(localIPEndPoint);
            //}
            //catch (Exception er)
            //{

            //}
            switch (protocolType)
            {
                case ProtocolType.Tcp:
                    {
                        client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        client.Blocking = true;
                        //client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveBuffer, packageSize);
                        //client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendBuffer, packageSize);
                        try
                        {
                            client.Connect(hostIPEndPoint);
                        }
                        catch (Exception e)
                        {
                            return client;
                        }
                        if (client.Connected)
                        {
                            SocketPackage ssi = new SocketPackage() { Body = new byte[packageSize], Client = client };
                            if (m_SetupReceiveEvent!=null)
                                m_SetupReceiveEvent(this, ssi);
                        }
                        break;
                    }
                case ProtocolType.Udp:
                    {
                        client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                                   
                       // client.Bind(localIPEndPoint);

                        break;
                    }
            }
            return client;
               
        }

       
    }
}

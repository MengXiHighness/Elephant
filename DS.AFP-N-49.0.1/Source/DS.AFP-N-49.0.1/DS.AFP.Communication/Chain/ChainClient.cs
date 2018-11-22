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
using System.Net;
using DS.AFP.Communication.SocketNameSpace.Protocol;
using System.IO;

namespace DS.AFP.Communication.SocketNameSpace
{
    /// <summary>
    /// Chain客户端
    /// </summary>
    public class ChainClient:SocketClient<ChainPackage> , IDisposable
    {
       
        private const int packageSize = 1024*1024;

        private readonly byte[] m_SearchState = Encoding.UTF8.GetBytes(SocketParams.SendTerminator);

        
        public ChainClient(Socket socket)
            : base(socket, new ChainProtocol(), packageSize)
        {
          
        }

        public ChainClient(string hostIp, int hostPort)
            : base(hostIp, hostPort, new ChainProtocol(), ProtocolType.Tcp, packageSize)
        {

        }

        //public ChainClient(int localPort, string hostIp, int hostPort):base( localPort,  hostIp,  hostPort)
        //{
        //    if (Client != null && Client.Connected)
        //    {
        //        Client.Shutdown(SocketShutdown.Both);
        //        System.Threading.Thread.Sleep(10);
        //        Client.Close();
        //    }
        //    SocketFactory sf = new SocketFactory();
           
        //    sf.SetupReceiveEvent += OnSetupReceiveEvent;
        //    Client = sf.Get( hostIp, hostPort, ProtocolType.Tcp);
        //}
             

        private void HandleException(Exception e)
        {

            Close();
        }

        #region 发送

       

        /// <summary>
        /// 发送扩展消息
        /// </summary>
        /// <param name="cp"></param>
        /// <param name="isSendTerminator">是否发送结尾符，结尾符是$$</param>
        public int Send(ChainPackage cp)
        {
            try
            {
                if (!Client.Connected)
                    return -1;
                //Socket.Select(null, new List<Socket>() { Client }, null, 10);
                int r = -1;
                if (Client.Poll(-1, SelectMode.SelectWrite))
                {
                    cp.Header.Data.Add(HeadKeys.SrcIP, (Client.LocalEndPoint as IPEndPoint).Address.ToString());
                    cp.Header.Data.Add(HeadKeys.SendTime, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fffffff"));
                    r = InterSend(cp.ToByte());
                }
                return r;
            }
            catch (Exception e)
            {
                //throw e;
                return -1;
            }
        }

        int InterSend(byte[] data)
        {
            //if (isSendTerminator)
            //{
            //    byte[] t_data = m_SearchState;//添加包尾分隔符$$
            //    byte[] t1_data = new byte[data.Length + t_data.Length];
            //    data.CopyTo(t1_data, 0);
            //    t_data.CopyTo(t1_data, data.Length);

            //    return  Client.Send(t1_data, SocketFlags.None);
            //}
            //else
            //{
            return Client.Send(data, SocketFlags.None);
            //}
        }

        SocketError InterAsycSend(byte[] data)
        {
            byte[] t_data = m_SearchState;//添加包尾分隔符##
            byte[] t1_data = new byte[data.Length + t_data.Length];
            data.CopyTo(t1_data, 0);
            t_data.CopyTo(t1_data, data.Length);
            SocketError error;
            Client.BeginSend(t1_data, 0, t1_data.Length, SocketFlags.None, out error, (ar) =>
            {
                int i = Client.EndSend(ar, out error);
            }, Client);
            return error;
        }
        #endregion

     
       
    }
}

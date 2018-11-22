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
using DS.AFP.Common.Core.Utility;

namespace DS.AFP.Communication.SocketNameSpace
{
    /// <summary>
    /// Scoket客户端
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SocketClient<T> : IDisposable where T : SocketPackage, new()
    {
        public Socket Client
        {
            get;
            set;
        }

        private readonly byte[] m_SearchState = Encoding.UTF8.GetBytes(SocketParams.SendTerminator);

        private int m_packageSize = 1024 * 1024;

        /// <summary>
        /// 接收数据事件
        /// </summary>
        public event EventHandler<T> OnReceived;

        /// <summary>
        /// 链接状态变更事件
        /// </summary>
        public event EventHandler<EventArgs> OnConnectionStateChanged;

        private Action<T> DataHandler = null;

        private T package = null;

        private static object package_lock = new object();

        protected IPackageProtocol<T> protocol = null;

        private object lock_data = new object();
        //接收完成信号
        private bool isCompleted = false;

        //接收超时时间
        private TimeSpan receive_timeout = new TimeSpan(0, 0, 10);

        //  public SocketClient<T>(Socket socket) :this<T>(socket,new SocketPackageProtocol()) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="socket">要绑定的Socket对象</param>
        /// <param name="protocol">包协议类型</param>
        /// <param name="packageSize">包大小</param>
        public SocketClient(Socket socket, IPackageProtocol<T> protocol, int packageSize = 1024*1024)
        {
            if (Client != null && Client.Connected)
            {
                Client.Shutdown(SocketShutdown.Both);
                System.Threading.Thread.Sleep(10);
                Client.Close();
            }
            m_packageSize = packageSize;
            this.protocol = protocol;
            Client = socket;
            T ssi = new T();
            ssi.Body = new byte[packageSize];
            OnSetupReceiveEvent(this, ssi);
        }

        //public SocketClient(string hostIp, int hostPort) : this(hostIp, hostPort, new SocketPackageProtocol()) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hostIp">远程IP</param>
        /// <param name="hostPort">远程端口号</param>
        /// <param name="protocol">包协议类型</param>
        /// <param name="protocolType">Socket支持的类型</param>
        /// <param name="packageSize">包大小</param>
        public SocketClient(string hostIp, int hostPort, IPackageProtocol<T> protocol, ProtocolType protocolType = ProtocolType.Tcp, int packageSize = 1024*1024)
        {
            if (Client != null && Client.Connected)
            {
                Client.Shutdown(SocketShutdown.Both);
                System.Threading.Thread.Sleep(10);
                Client.Close();
            }
            m_packageSize = packageSize;
            this.protocol = protocol;

            SocketFactory sf = new SocketFactory();

            //sf.SetupReceiveEvent += OnSetupReceiveEvent;
            Client = sf.Get(hostIp, hostPort, protocolType);

        }



        /// <summary>
        ///  内部接收
        /// </summary>
        /// <param name="package"></param>
        private void ReceiveDataHandler(T package)
        {
            this.package = package;
            isCompleted = true;
        }




        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns>数据包</returns>
        public T GetData()
        {
            try
            {
                DataHandler = ReceiveDataHandler;
                DateTime startTime = System.DateTime.Now;
                while (!isCompleted)
                {
                    if (System.DateTime.Now.Subtract(startTime).TotalSeconds > receive_timeout.TotalSeconds)
                    {
                        using (Lock lk = new Lock(lock_data, 1000))
                        {
                            isCompleted = false;
                            DataHandler = null;
                        }
                        return null;
                    }
                }

                using (Lock lk = new Lock(lock_data, 1000))
                {
                    isCompleted = false;
                    DataHandler = null;
                }
            }
            catch (Exception e)
            {
                //log
            }
            return package;
        }

        private void OnSetupReceiveEvent(object sender, SocketPackage e)
        {
            e.Client.BeginReceive(e.Body, 0, e.Body.Length, SocketFlags.None, new AsyncCallback((ar) =>
            {

                SocketPackage ssi = ar.AsyncState as SocketPackage;

                try
                {
                    if (!ssi.Client.Connected)
                        return;
                    int readlength = ssi.Client.EndReceive(ar);
                    if (readlength > 0)
                    {

                        byte[] data = new byte[readlength];
                        Buffer.BlockCopy(ssi.Body, 0, data, 0, readlength);
                        //ssi.Package = data;
                        //if (OnReceived != null)
                        //    OnReceived(this, ssi);
                        //if (DataHandler!=null)
                        //     DataHandler(data);
                        PackageHandle(data.ToList());
                        ssi.Body = new byte[m_packageSize];
                        ssi.Client.BeginReceive(ssi.Body, 0, ssi.Body.Length, SocketFlags.None, ReceiveCallback, ssi);

                    }
                    //else
                    //{
                    //    ssi.Socket.Close();
                    //    if (OnConnectionStateChanged != null)
                    //        OnConnectionStateChanged(this, new EventArgs() { });
                    //}
                }
                catch
                {
                    ssi.Client.Close();
                    if (OnConnectionStateChanged != null)
                        OnConnectionStateChanged(this, new EventArgs() { });
                }

            }), e);

        }

        private void ReceiveCallback(IAsyncResult asyResult)
        {
            SocketPackage ssi = asyResult.AsyncState as SocketPackage;

            try
            {
                if (!ssi.Client.Connected)
                    return;
                int readlength = ssi.Client.EndReceive(asyResult);
                if (readlength > 0)
                {

                    byte[] data = new byte[readlength];
                    Buffer.BlockCopy(ssi.Body, 0, data, 0, readlength);
                    //Task.Factory.StartNew(new Action<object>((o) => {
                    //IList<byte> t_data = o as IList<byte>;
                    PackageHandle(data.ToList());
                    //}), data.ToList());
                    // ssi.Package = data;
                    //if (OnReceived != null)
                    //    OnReceived(this, ssi);
                    //if (DataHandler != null)
                    //    DataHandler(data);

                    ssi.Body = new byte[m_packageSize];
                    ssi.Client.BeginReceive(ssi.Body, 0, ssi.Body.Length, SocketFlags.None, ReceiveCallback, ssi);

                }
                //else
                //{

                //    if (OnReceived != null)
                //        OnReceived(this, ssi);
                //    ssi.Socket.Close();
                //    if (OnConnectionStateChanged != null)
                //        OnConnectionStateChanged(this, new EventArgs() { });
                //}
            }
            catch
            {
                ssi.Client.Close();
                if (OnConnectionStateChanged != null)
                    OnConnectionStateChanged(this, new EventArgs() { });
            }
        }

        /// <summary>
        /// 情况1：刚好收完，并且收到数据长度小接收缓冲
        /// 情况2：粘包情况，
        /// </summary>
        /// <param name="receiveCache"></param>
        /// <param name="receiveLen"></param>
        private void PackageHandle(IList<byte> readBuffer)
        {
            try
            {
                IList<T> cps = protocol.ResolveProtocol(readBuffer);
                if (cps == null)
                    return;
                foreach (T cp in cps)
                {
                    if (OnReceived != null)
                        OnReceived(this, cp);
                    using (Lock lk = new Lock(lock_data, 1000))
                    {
                        if (DataHandler != null)
                            DataHandler(cp);
                    }
                }
            }
            catch (Exception e)
            {
                //log
            }
        }





        private void HandleException(Exception e)
        {

            Close();
        }


        #region 发送
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="data">要发送的数据</param>
        /// <param name="isSendTerminator">是否要添加添加包尾分隔符</param>
        /// <returns></returns>
        public int Send(T data, bool isSendTerminator = false)
        {
            return InterSend(protocol.ResolveProtocol(data), isSendTerminator);
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="data">要发送的数据</param>
        /// <param name="isSendTerminator">是否要添加添加包尾分隔符</param>
        /// <returns></returns>
        public int Send(byte[] data, bool isSendTerminator = false)
        {
            return InterSend(data, isSendTerminator);
        }

        int InterSend(byte[] data, bool isSendTerminator)
        {
            if (isSendTerminator)
            {
                byte[] t_data = m_SearchState;//添加包尾分隔符\r\n\r\n
                byte[] t1_data = new byte[data.Length + t_data.Length];
                data.CopyTo(t1_data, 0);
                t_data.CopyTo(t1_data, data.Length);
                //Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout,2000);
                //Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 1);

                return Client.Send(t1_data, SocketFlags.None);
            }
            else
            {
                return Client.Send(data, SocketFlags.None);
            }
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

        public void Dispose()
        {
            Close();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 关闭ScoketClient
        /// </summary>
        public void Close()
        {
            if (Client != null && Client.Connected)
                Client.Shutdown(SocketShutdown.Both);
            if (Client.Connected)
                Client.Close();
            else
                Client.Dispose();
            if (OnConnectionStateChanged != null)
                OnConnectionStateChanged(this, new EventArgs() { });
        }
    }
}

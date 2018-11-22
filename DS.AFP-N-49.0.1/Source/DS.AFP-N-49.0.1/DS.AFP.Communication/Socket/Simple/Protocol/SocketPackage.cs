using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using DS.AFP.Communication.Chain;
using DS.AFP.Communication.SocketBase.Protocol;

namespace DS.AFP.Communication.SocketNameSpace
{
    public class SocketPackage : EventArgs, IRequestInfo<byte[]>
    {
        /// <summary>
        /// 基础Socket包
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="packageSize">1024*1024</param>
        public SocketPackage()
        {
          
        }

        public Socket Client
        {
            get;
            set;
        }
       

        public byte[] Body
        {
            get;
            set;
        }

        public string Key
        {
            get;
            set;
        }
    }

  
}

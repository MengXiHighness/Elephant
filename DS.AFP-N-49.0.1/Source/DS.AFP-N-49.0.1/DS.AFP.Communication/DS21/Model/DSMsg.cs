using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.AFP.Communication.DS21
{
    /// <summary>
    /// DS21消息包
    /// </summary>
    public class DSMsg : EventArgs
    {
        public DSMsg() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="srcID">源节点ID</param>
        /// <param name="srcType">源节点类型</param>
        /// <param name="destID">目标节点ID</param>
        /// <param name="destType">目标节点类型</param>
        /// <param name="msgID">消息ID</param>
        /// <param name="sendmode">发送发送</param>
        /// <param name="msgCode">消息编码</param>
        public DSMsg(ushort srcID, NodeType srcType, ushort destID, NodeType destType, InfoSysMessageID msgID, SendMode sendmode, string msgCode)
        {
            this.sendmode = (short)sendmode;
            this.srcID = srcID;
            this.srcType = (ushort)srcType;
            this.destID = destID;
            this.destType = (ushort)destType;
            this.msgID = (ushort)msgID;
            this.msgCode = msgCode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scrID">源节点ID</param>
        /// <param name="scrType">源节点类型</param>
        /// <param name="destID">目标节点ID</param>
        /// <param name="destType">目标节点类型</param>
        /// <param name="msgID">消息ID</param>
        /// <param name="sendmode">发送发送</param>
        /// <param name="msgCode">消息编码</param>
        /// <param name="scrRouteID">源路由ID</param>
        /// <param name="scrRouteType">源路由类型</param>
        /// <param name="transferScrRouteID">源路由转换ID</param>
        /// <param name="transferDestRouteID">目标路由转换ID</param>
        /// <param name="srcNetWorkID">源网络编码</param>
        /// <param name="destNetWorkID">目标网络编码</param>
        public DSMsg(ushort scrID, NodeType scrType, ushort destID, NodeType destType, InfoSysMessageID msgID, SendMode sendmode, ushort scrRouteID, NodeType scrRouteType, ushort transferScrRouteID, ushort transferDestRouteID, string srcNetWorkID, string destNetWorkID, string msgCode)
            : this(scrID, scrType, destID, destType, msgID, sendmode, msgCode)
        {
            //ushort scrRouteID, ushort transferScrRouteID, ushort transferDestRouteID, string srcNetWorkID, string destNetWorkID)
            this.scrRouteID = scrRouteID;
            this.scrRouteType = (ushort)scrRouteType;
            this.transferScrRouteID = transferScrRouteID;
            this.transferDestRouteID = transferDestRouteID;
            this.srcNetWorkID = srcNetWorkID;
            this.destNetWorkID = destNetWorkID;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scrID">源节点ID</param>
        /// <param name="scrType">源节点类型</param>
        /// <param name="destID">目标节点ID</param>
        /// <param name="destType">目标节点类型</param>
        /// <param name="msgID">消息ID</param>
        /// <param name="sendmode">发送发送</param>
        /// <param name="msgCode">消息编码</param>
        /// <param name="scrRouteID">源路由ID</param>
        /// <param name="scrRouteType">源路由类型</param>
        /// <param name="transferScrRouteID">源路由转换ID</param>
        /// <param name="transferDestRouteID">目标路由转换ID</param>
        /// <param name="srcNetWorkID">源网络编码</param>
        /// <param name="destNetWorkID">目标网络编码</param>
        /// <param name="transferDestRouteType">目标的路由类型</param>
        /// /// <param name="transferScrRouteType">源的路由类型</param>
        public DSMsg(ushort scrID, NodeType scrType, ushort destID, NodeType destType, InfoSysMessageID msgID, SendMode sendmode, ushort scrRouteID, NodeType scrRouteType, ushort transferScrRouteID, ushort transferDestRouteID, NodeType transferDestRouteType, NodeType transferScrRouteType, string srcNetWorkID, string destNetWorkID, string msgCode)
            : this(scrID, scrType, destID, destType, msgID, sendmode, scrRouteID, scrRouteType, transferScrRouteID, transferDestRouteID, srcNetWorkID, destNetWorkID, msgCode)
        {
            this.transferDestRouteType = (ushort)transferDestRouteType;
            this.transferScrRouteType = (ushort)transferScrRouteType;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scrID">源节点ID</param>
        /// <param name="scrType">源节点类型</param>
        /// <param name="destID">目标节点ID</param>
        /// <param name="destType">目标节点类型</param>
        /// <param name="msgID">消息ID</param>
        /// <param name="sendmode">发送发送</param>
        /// <param name="msgCode">消息编码</param>
        /// <param name="scrRouteID">源路由ID</param>
        /// <param name="scrRouteType">源路由类型</param>
        /// <param name="transferScrRouteID">源路由转换ID</param>
        /// <param name="transferDestRouteID">目标路由转换ID</param>
        /// <param name="srcNetWorkID">源网络编码</param>
        /// <param name="destNetWorkID">目标网络编码</param>
        /// <param name="transferDestRouteType">目标的路由类型</param>
        /// /// <param name="transferScrRouteType">源的路由类型</param>
        public DSMsg(ushort scrID, NodeType scrType, ushort destID, NodeType destType, InfoSysMessageID msgID, ushort scrRouteID, NodeType scrRouteType, SendMode sendmode, ushort transferScrRouteID, ushort transferDestRouteID, NodeType transferDestRouteType, NodeType transferScrRouteType, ushort serialNo, ushort reserved, string msgCode, string srcNetWorkID, string destNetWorkID)
            : this(scrID, (ushort)scrType, destID, (ushort)destType, (ushort)msgID, scrRouteID, (ushort)scrRouteType, (short)sendmode, transferScrRouteID, transferDestRouteID, (ushort)transferDestRouteType, (ushort)transferScrRouteType, serialNo, reserved, msgCode, srcNetWorkID, destNetWorkID)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scrID">源节点ID</param>
        /// <param name="scrType">源节点类型</param>
        /// <param name="destID">目标节点ID</param>
        /// <param name="destType">目标节点类型</param>
        /// <param name="msgID">消息ID</param>
        /// <param name="sendmode">发送发送</param>
        /// <param name="msgCode">消息编码</param>
        /// <param name="scrRouteID">源路由ID</param>
        /// <param name="scrRouteType">源路由类型</param>
        /// <param name="transferScrRouteID">源路由转换ID</param>
        /// <param name="transferDestRouteID">目标路由转换ID</param>
        /// <param name="srcNetWorkID">源网络编码</param>
        /// <param name="destNetWorkID">目标网络编码</param>
        /// <param name="transferDestRouteType">目标的路由类型</param>
        /// /// <param name="transferScrRouteType">源的路由类型</param>
        public DSMsg(ushort scrID, ushort scrType, ushort destID, ushort destType, ushort msgID, ushort scrRouteID, ushort scrRouteType, short sendmode, ushort transferScrRouteID, ushort transferDestRouteID, ushort transferDestRouteType, ushort transferScrRouteType, ushort serialNo, ushort reserved, string msgCode, string srcNetWorkID, string destNetWorkID)
        {
            this.sendmode = sendmode;
            this.srcID = scrID;
            this.srcType = scrType;
            this.destID = destID;
            this.destType = destType;
            this.msgID = msgID;
            this.scrRouteID = scrRouteID;
            this.scrRouteType = scrRouteType;
            this.transferScrRouteID = transferScrRouteID;
            this.transferDestRouteID = transferDestRouteID;
            this.transferDestRouteType = transferDestRouteType;
            this.transferScrRouteType = transferScrRouteType;
            this.serialNo = serialNo;
            this.reserved = reserved;
            this.msgCode = msgCode;
            this.srcNetWorkID = srcNetWorkID;
            this.destNetWorkID = destNetWorkID;
        }

        private string msgCode = string.Empty;
        /// <summary>
        /// 消息号（如：A406，3701等）
        /// </summary>
        public string MessageCode
        {
            get
            {
                return this.msgCode;
            }
            set
            {
                this.msgCode = value;
            }
        }

        private string srcNetWorkID = string.Empty;
        /// <summary>
        /// 源网络标识ID（仅Remote使用，十位数如0000000000）
        /// </summary>
        public string SrcNetWorkID
        {
            get
            {
                return this.srcNetWorkID;
            }
            set
            {
                this.srcNetWorkID = value;
            }
        }

        private string destNetWorkID = string.Empty;
        /// <summary>
        ///  目标网络标识ID（仅Remote使用，十位数如0000000000）
        /// </summary>
        public string DestNetWorkID
        {
            get
            {
                return this.destNetWorkID;
            }
            set
            {
                this.destNetWorkID = value;
            }
        }

        private bool isTransferRoute = false;
        /// <summary>
        /// 是否Remote（默认不是Remote）
        /// </summary>
        public bool IsTransferRoute
        {
            get
            {
                return this.isTransferRoute;
            }
            set
            {
                this.isTransferRoute = value;
            }
        }

        private ushort srcType = 0;
        /// <summary>
        /// 源类型（默认0）
        /// </summary>
        public ushort SrcType
        {
            get
            {
                return this.srcType;
            }
            set
            {
                this.srcType = value;
            }
        }

        private short sendmode = 0;
        /// <summary>
        /// 发送类型（默认为0）
        ///广播(EXCLUDE) = 1,
        ///广播(INCLUDE) = 2,
        ///UNKNOWN = 3,
        /// </summary>
        public short Sendmode
        {
            get
            {
                return this.sendmode;
            }
            set
            {
                this.sendmode = value;
            }
        }

        private ushort srcID = 0;
        /// <summary>
        /// 源标识ID（默认为0）
        /// </summary>
        public ushort SrcID
        {
            get
            {
                return this.srcID;
            }
            set
            {
                this.srcID = value;
            }
        }
        private ushort destType = 0;
        /// <summary>
        /// 目标类型（默认为0）
        /// </summary>
        public ushort DestType
        {
            get
            {
                return this.destType;
            }
            set
            {
                this.destType = value;
            }
        }

        private ushort destID = 0;
        /// <summary>
        /// 目标类型（默认为0）
        /// </summary>
        public ushort DestID
        {
            get
            {
                return this.destID;
            }
            set
            {
                this.destID = value;
            }
        }

        private ushort msgType = 0;
        /// <summary>
        /// 消息类型（默认为0）
        /// </summary>
        public ushort MsgType
        {
            get
            {
                return this.msgType;
            }
            set
            {
                this.msgType = value;
            }
        }
        private ushort msgID = 0;
        /// <summary>
        /// 消息ID（默认为0，一般不赋值）
        /// </summary>
        public ushort MsgID
        {
            get
            {
                return this.msgID;
            }
            set
            {
                this.msgID = value;
            }
        }

        private uint serialNo = 0;
        /// <summary>
        /// 消息序列号（默认为0，一般不赋值）
        /// </summary>
        public uint SerialNo
        {
            get
            {
                return this.serialNo;
            }
            set
            {
                this.serialNo = value;
            }
        }

        private uint reserved = 0;
        /// <summary>
        /// 解析号（默认为0）
        /// </summary>
        public uint Reserved
        {
            get
            {
                return this.reserved;
            }
            set
            {
                this.reserved = value;
            }
        }

        private string strData = string.Empty;
        /// <summary>
        /// 传输的数据
        /// </summary>
        public string StrData
        {
            get
            {
                return this.strData;
            }
            set
            {
                this.strData = value;
            }
        }

        /// <summary>
        /// 消息号（如8210）
        /// </summary>
        public DSMessageCode DSMessageCode
        {
            get
            {
                return (DSMessageCode)MsgID;
            }
        }


        private ushort transferScrRouteID = 1;
        /// <summary>
        /// 转换源Remote的ID（仅在Remote下使用）
        /// </summary>
        public ushort TransferScrRouteID
        {
            get
            {
                return this.transferScrRouteID;
            }
            set
            {
                this.transferScrRouteID = value;
            }
        }

        private ushort transferDestRouteID = 1;
        /// <summary>
        /// 转换目标Remote的ID（仅在Remote下使用）
        /// </summary>
        public ushort TransferDestRouteID
        {
            get
            {
                return this.transferDestRouteID;
            }
            set
            {
                this.transferDestRouteID = value;
            }
        }

        private ushort scrRouteID = 1;
        /// <summary>
        /// 源目标Remote的ID（仅在Remote下使用）
        /// </summary>
        public ushort ScrRouteID
        {
            get
            {
                return this.scrRouteID;
            }
            set
            {
                this.scrRouteID = value;
            }
        }

        private ushort scrRouteType = (ushort)0x9b00;
        /// <summary>
        /// 源目标Remote的类型（仅在Remote下使用）
        /// </summary>
        public ushort ScrRouteType
        {
            get
            {
                return this.scrRouteType;
            }
            set
            {
                this.scrRouteType = value;
            }
        }

        private ushort transferScrRouteType = (ushort)0x9c20;
        /// <summary>
        /// 转换源目标Remote的类型（仅在Remote下使用，一般不做改动，默认 (ushort)0x9c20）
        /// </summary>
        public ushort TransferScrRouteType
        {
            get
            {
                return this.transferScrRouteType;
            }
            set
            {
                this.transferScrRouteType = value;
            }
        }

        private ushort transferDestRouteType = (ushort)0x9c20;
        /// <summary>
        /// 转换目标Remote的类型（仅在Remote下使用，一般不做改动，默认 (ushort)0x9c20）
        /// </summary>
        public ushort TransferDestRouteType
        {
            get
            {
                return this.transferDestRouteType;
            }
            set
            {
                this.transferDestRouteType = value;
            }
        }
    }
}

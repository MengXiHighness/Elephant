using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.AFP.Communication.DS21;

namespace DS.AFP.Framework.Message
{
    /// <summary>
    /// 消息发送者对象
    /// </summary>
    public class MessageSender
    {
        private ushort scrType = 0;
        private ushort scrID = 0;
        private ushort destType = 0;
        private ushort destID = 0;
        private ushort msgType = 0;
        private ushort msgID = 0;
        private uint serialNo = 0;
        private uint reserved = 0;
        private bool isTransferRoute = false;
        public string MessageCode { get; set; }

        public MessageSender(DSMsg dsmsg)
        {
            scrType = dsmsg.SrcType;
            scrID = dsmsg.SrcID;
            destType = dsmsg.DestType;
            destID = dsmsg.DestID;
            msgType = dsmsg.MsgType;
            msgID = dsmsg.MsgID;
            serialNo = dsmsg.SerialNo;
            reserved = dsmsg.Reserved;
            isTransferRoute = dsmsg.IsTransferRoute;
        }

        public ushort ScrType
        {
            get
            {
                return this.scrType;
            }
            set
            {
                this.scrType = value;
            }
        }

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
        public ushort ScrID
        {
            get
            {
                return this.scrID;
            }
            set
            {
                this.scrID = value;
            }
        }

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
    }
}

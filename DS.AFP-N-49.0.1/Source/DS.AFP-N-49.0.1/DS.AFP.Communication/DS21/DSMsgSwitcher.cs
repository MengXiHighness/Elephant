using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.AFP.Communication.DS21
{
    public class DSMsgSwitcher
    {
        public NodeType SrcNodeType { get; set; }
        public ushort SrcAgentID { get; set; }
        public NodeType DestNodeType { get; set; }
        public ushort DestAgentID { get; set; }
        public InfoSysMessageID MsgID { get; set; }
        public SendMode SendMode { get; set; }
        public string MsgBody { get; set; }
        public string MessageCode { get; set; }
        public int ReserveID { get; set; }
        public string StrData { get; set; }

        public DSMsgSwitcher()
        {
            this.ReserveID = 0;
        }


        public DSMsgSwitcher(NodeType srcType, ushort srcID,
          NodeType destType, ushort destID,
          InfoSysMessageID msgID, string strData, SendMode sendMode = SendMode.P2P)
        {
            this.ReserveID = 0;
            this.SrcNodeType = srcType;
            this.SrcAgentID = srcID;
            this.DestNodeType = destType;
            this.DestAgentID = destID;
            this.MsgID = msgID;
            this.SendMode = sendMode;
            this.StrData = strData;
        }

        public DSMsg ToDSMsg()
        {
            DSMsg msg = new DSMsg()
            {
                DestID = DestAgentID,
                DestType = (ushort)DestNodeType,
                MsgID = (ushort)this.MsgID,
                ScrID = SrcAgentID,
                ScrType = (ushort)SrcNodeType,
                MsgType = 0,//////暂时不知道做什么用的
                SerialNo = 0,
                StrData = this.StrData,
                Reserved = (uint)this.ReserveID
            };
            return msg;
        }
    }
}

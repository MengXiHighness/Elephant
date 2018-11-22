using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.AFP.Communication.DS21
{
    /// <summary>
    /// DS21消息头
    /// </summary>
    public class MsgHead
    {
        public string ExtendRoute { get; set; }
        public SrcNode srcNode { get; set; }
        public DstNode dstNode { get; set; }
        public MsgNode msgNode { get; set; }

        public MsgHead()
        {

        }

        ~MsgHead()
        {

        }

        public virtual void Dispose()
        {

        }

    }
}

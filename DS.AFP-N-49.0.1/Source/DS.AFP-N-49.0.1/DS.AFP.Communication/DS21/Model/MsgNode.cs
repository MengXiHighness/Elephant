using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.AFP.Communication.DS21
{
    /// <summary>
    /// DS21消息节点
    /// </summary>
    public class MsgNode
    {

        public string MessageCode { get; set; }
        public string SendTime { get; set; }
        public string TransType { get; set; }
        public string Ver { get; set; }

        public MsgNode()
        {

        }

        ~MsgNode()
        {

        }

        public virtual void Dispose()
        {

        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace DS.AFP.Communication.DS21
{
    /// <summary>
    /// DS21消息事件数据
    /// </summary>
    public class MsgEventArgs
    {

        public MsgHead Head
        {
            get;
            set;
        }
        public MsgBody Body { get; set; }
        public string Version { get; set; }

        public MsgEventArgs()
        {

        }

        ~MsgEventArgs()
        {

        }

        public virtual void Dispose()
        {

        }

    }
}

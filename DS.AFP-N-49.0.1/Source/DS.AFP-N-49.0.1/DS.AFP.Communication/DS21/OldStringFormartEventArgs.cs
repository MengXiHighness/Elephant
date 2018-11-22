using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.AFP.Communication.DS21
{
    /// <summary>
    /// 8000消息订阅
    /// </summary>
    public class OldStringFormartEventArgs
    {
        public string MessageHeader { get; set; }
        public string MessageCode { get; set; }
        public string MessageBody { get; set; }
    }
}

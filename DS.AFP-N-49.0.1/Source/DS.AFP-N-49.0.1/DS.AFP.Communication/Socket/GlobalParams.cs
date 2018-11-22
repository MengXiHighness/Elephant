using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.AFP.Communication.SocketNameSpace
{
    /// <summary>
    /// Socket配置节点位置
    /// </summary>
    public class SocketSection
    {
        public const string SectionName = "ds/socket";
    }

    /// <summary>
    /// Socket参数
    /// </summary>
    public class SocketParams
    {
        public const string Spliters = "@@";
        public const string DefaultCommand = "DefaultCommand";
        //消息截断标记
        public const string Terminator = "\r\n\r\n";
        //send 
        public const string SendTerminator = "$$";

    }
}

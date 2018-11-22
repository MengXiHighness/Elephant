using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.AFP.Communication.Chain
{
    /// <summary>
    /// 消息头常量
    /// </summary>
    public class HeadKeys
    {
        /// <summary>
        /// 消息内容长度
        /// </summary>
        public const string ContentLen = "Content-Length";

        /// <summary>
        /// 执行路由命令
        /// </summary>
        public const string CmdName = "CmdName";

        /// <summary>
        /// 消息标识
        /// </summary>
        public const string MsgNo = "MsgNo";

        /// <summary>
        /// 发送源IP地址
        /// </summary>
        public const string SrcIP = "SrcIP";

        /// <summary>
        /// 客户端发送时间
        /// </summary>
        public const string SendTime = "SendTime";

        /// <summary>
        /// 发送行为
        /// </summary>
        public const string Action = "Action";
        
        /// <summary>
        /// URL
        /// </summary>
        public const string URI = "URI";

        /// <summary>
        /// 发送协议
        /// </summary>
        public const string Protocol = "Protocol";


        /// <summary>
        /// 错误信息
        /// </summary>
        public const string ErrMsg = "ErrMsg";

        /// <summary>
        /// 返回结果状态
        /// </summary>
        public const string Ret = "Ret";

        /// <summary>
        /// 总文件大小，不是包的大小
        /// </summary>
        public const string FileLength = "FileLength";


    }
}

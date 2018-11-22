using DS.AFP.Communication.SocketNameSpace;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using DS.AFP.Common.Core;
using DS.AFP.Communication.Chain.Protocol;
using System.Diagnostics;
using DS.AFP.Communication.SocketBase.Protocol;

namespace DS.AFP.Communication.Chain
{

    /// <summary>
    /// Chain的消息包
    /// </summary>
    public class ChainPackage : SocketPackage, IRequestInfo<ChainHeader, byte[]>
    {
        public ChainPackage(string body) : this(Encoding.UTF8.GetBytes(body), null) { }

        public ChainPackage() : this(new byte[0], null) { }

        public ChainPackage(byte[] body, ChainHeader header) :
            this(body, "DefaultCommand", "", header) { }

        public ChainPackage(byte[] body, string key, ChainHeader header) :
            this(body, key, "", header) { }

        /// <summary>
        /// 发送消息及数据结构
        /// </summary>
        /// <param name="body">消息内容</param>
        /// <param name="key">执行路由命令</param>
        /// <param name="msgNo">消息标识</param>
        /// <param name="header">消息头</param>
        public ChainPackage(byte[] body, string key = "DefaultCommand", string msgNo = "", ChainHeader header = null)
        {
            Header = new ChainHeader();
            if (header != null)
            {
                Header.Method = header.Method;
                Header.Url = header.Url;
                Header.Protocol = header.Protocol;
                Header.QueryString.Add(header.QueryString);
                if (header.Data != null)
                {
                    Header.Data.Add(header.Data);
                }
            }



            if (this.Header.Data.AllKeys.Where(k => k == HeadKeys.ContentLen).Count() == 0)
                this.Header.Data.Add(HeadKeys.ContentLen, body.Length.ToString());

            if (this.Header.Data.AllKeys.Where(k => k == HeadKeys.CmdName).Count() == 0)
                this.Key = key;

            if (this.Header.Data.AllKeys.Where(k => k == HeadKeys.MsgNo).Count() == 0)
                this.MsgNo = msgNo;

            this.Body = body;
        }

        internal byte[] ToByte()
        {
            ChainProtocol cpcl = new ChainProtocol();
            return cpcl.ResolveProtocol(this);
        }

        /// <summary>
        /// 消息头的键值信息
        /// </summary>
        public ChainHeader Header
        {
            get;
            private set;
        }

        /// <summary>
        /// 消息体
        /// </summary>
        public byte[] Body
        {
            get;
            private set;
        }

        string _Command_Name = "DefaultCommand";
        /// <summary>
        /// 消息key
        /// </summary>
        public string Key
        {
            get
            {
                if (this.Header.Data.AllKeys.Where(k => k == HeadKeys.CmdName).Count() != 0)
                    return this.Header.Data[HeadKeys.CmdName];
                return _Command_Name;
            }
            set
            {
                _Command_Name = value;
                if (this.Header.Data.AllKeys.Where(k => k == HeadKeys.CmdName).Count() != 0)
                    this.Header.Data.Remove(HeadKeys.CmdName);
                this.Header.Data.Add(HeadKeys.CmdName, _Command_Name);
            }
        }

        string _msgNo = "";
        /// <summary>
        /// 消息key
        /// </summary>
        public string MsgNo
        {
            get
            {
                if (this.Header.Data.AllKeys.Where(k => k == HeadKeys.MsgNo).Count() != 0)
                    return this.Header.Data[HeadKeys.MsgNo];
                return _msgNo;
            }
            set
            {
                _msgNo = value;
                if (this.Header.Data.AllKeys.Where(k => k == HeadKeys.MsgNo).Count() != 0)
                    this.Header.Data.Remove(HeadKeys.MsgNo);
                this.Header.Data.Add(HeadKeys.MsgNo, _msgNo);
            }
        }

    }



    /// <summary>
    /// ChainHeader Http协议头
    /// </summary>
    public class ChainHeader
    {
        public ChainHeader()
        {
            Method = "Chain";
            Url = "/";
            Protocol = "Chain/1.0";
            Data = new HeaderNameValueCollection();
            QueryString = new HeaderNameValueCollection();
        }

        public string Method
        {
            get;
            set;
        }

        public string Protocol
        {
            get;
            set;
        }

        public string Url
        {
            get;
            set;
        }

        public IHeaderNameValueCollection QueryString
        {
            get;
            set;
        }

        public IHeaderNameValueCollection Data
        {
            get;
            set;
        }

    }

    public class HeaderNameValueCollection : NameValueCollection, DS.AFP.Communication.Chain.Protocol.IHeaderNameValueCollection
    {

        // 摘要: 
        //     将指定 System.Collections.Specialized.NameValueCollection 中的项复制到当前 System.Collections.Specialized.NameValueCollection。
        //
        // 参数: 
        //   c:
        //     要复制到当前 System.Collections.Specialized.NameValueCollection 中的 System.Collections.Specialized.NameValueCollection。
        //
        // 异常: 
        //   System.NotSupportedException:
        //     集合为只读。
        //
        //   System.ArgumentNullException:
        //     c 为 null。
        public void Add(IHeaderNameValueCollection c)
        {
            foreach (string key in c.AllKeys)
            {
                this[key] = c[key];
            }
        }

        public void Add(NameValueCollection c)
        {
            foreach (string key in c.AllKeys)
            {
                this[key] = c[key];
            }
        }

        //
        // 摘要: 
        //     将具有指定名称和值的项添加到 System.Collections.Specialized.NameValueCollection。
        //
        // 参数: 
        //   name:
        //     要添加的项的 System.String 键。键可以是 null。
        //
        //   value:
        //     要添加的项的 System.String 值。该值可以为 null。
        //
        // 异常: 
        //   System.NotSupportedException:
        //     集合为只读。
        public override void Add(string name, string value)
        {
            this[name] = value;
        }

        // 摘要: 
        //     获取 System.Collections.Specialized.NameValueCollection 中指定索引处的项。
        //
        // 参数: 
        //   index:
        //     要在集合中定位的项的从零开始的索引。
        //
        // 返回结果: 
        //     一个 System.String，包含集合中指定索引处的值的列表（此列表以逗号分隔）。
        //
        // 异常: 
        //   System.ArgumentOutOfRangeException:
        //     index 在集合的有效索引范围外。
        public string this[int index]
        {
            get
            {
                return base[index].UrlDecode();
            }
        }


        //
        // 摘要: 
        //     获取或设置 System.Collections.Specialized.NameValueCollection 中具有指定键的项。
        //
        // 参数: 
        //   name:
        //     要定位的项的 System.String 键。键可以是 null。
        //
        // 返回结果: 
        //     如果找到了，则为一个 System.String，它包含与指定键关联的值的列表（用逗号分隔）；否则为 null。
        //
        // 异常: 
        //   System.NotSupportedException:
        //     该集合是只读的，但此操作尝试修改该集合。
        public string this[string name]
        {
            get
            {
                return base[name].UrlDecode();
            }
            set
            {
                base[name] = value.UrlEncode();
            }
        }
    }
}

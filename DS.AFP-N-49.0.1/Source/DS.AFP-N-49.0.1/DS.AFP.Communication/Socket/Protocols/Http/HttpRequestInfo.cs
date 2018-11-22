using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using DS.AFP.Communication.Facility.Protocol;

namespace DS.AFP.Communication.Http
{
    /// <summary>
    /// HttpRequestInfo
    /// </summary>
    public class HttpRequestInfo : HttpRequestInfoBase<byte[]>
    {
        /// <summary>
        /// 获取Get、Post、Delete、Put.
        /// </summary>
        /// <value>
        /// 
        /// </value>
        public string Method { get;  set; }

        /// <summary>
        /// 获取一个Cookie.
        /// </summary>
        /// <value>
        /// 
        /// </value>
        public NameValueCollection Cookies { get; private set; }

        /// <summary>
        /// 获取一个Form
        /// </summary>
        /// <value>
        /// 返回一个Form
        /// </value>
        public NameValueCollection Form { get; private set; }


        /// <summary>
        /// 初始化一个HttpRequestInfo
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="header">http头</param>
        public HttpRequestInfo(string key, NameValueCollection header)
            : base(key, header ,new byte[0])
        {
            
        }

        /// <summary>
        /// 初始化一个HttpRequestInfo
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="header">http头</param>
        public HttpRequestInfo(string key, NameValueCollection header,byte[] body)
            : base(key, header, body)
        {

        }
    }
}

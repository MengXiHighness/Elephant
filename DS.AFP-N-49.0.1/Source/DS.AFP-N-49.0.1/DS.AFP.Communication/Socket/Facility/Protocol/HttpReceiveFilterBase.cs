using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using DS.AFP.Communication.Common;
using DS.AFP.Communication.SocketBase;
using DS.AFP.Communication.SocketBase.Protocol;

namespace DS.AFP.Communication.Facility.Protocol
{
    /// <summary>
    /// HttpReceiveFilterBase
    /// </summary>
    /// <typeparam name="TRequestInfo">The type of the request info.</typeparam>
    public abstract class HttpReceiveFilterBase<TRequestInfo> : TerminatorReceiveFilter<TRequestInfo>
        where TRequestInfo : IRequestInfo
    {
        /// <summary>
        /// HTTP头的分割符
        /// </summary>
        private static readonly byte[] NewLine = Encoding.ASCII.GetBytes("\r\n\r\n");

        /// <summary>
        /// 标示HTTP头是否被解析
        /// </summary>
        private bool m_HeaderParsed = false;

        /// <summary>
        /// 得到header项.
        /// </summary>
        protected NameValueCollection HeaderItems { get; private set; }


        /// <summary>
        /// 初始化
        /// </summary>
        protected HttpReceiveFilterBase()
            : base(NewLine)
        {
            
        }

        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="readBuffer">read buffer.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="length">The length.</param>
        /// <param name="toBeCopied">if set to <c>true</c> [to be copied].</param>
        /// <param name="rest">The rest.</param>
        /// <returns></returns>
        public override TRequestInfo Filter(byte[] readBuffer, int offset, int length, bool toBeCopied, out int rest)
        {
            if (!m_HeaderParsed)
            {
                return base.Filter(readBuffer, offset, length, toBeCopied, out rest);
            }
            else
            {
                var requestInfo = FilterRequestBody(readBuffer, offset, length, toBeCopied, out rest);

                if (!ReferenceEquals(requestInfo, NullRequestInfo))
                {
                    //Reset the filter if one request info has been parsed
                    Reset();
                }

                return requestInfo;
            }
        }

        /// <summary>
        /// 处理Body
        /// </summary>
        /// <param name="readBuffer">The read buffer.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="length">The length.</param>
        /// <param name="toBeCopied">if set to <c>true</c> [to be copied].</param>
        /// <param name="rest">The rest data size.</param>
        /// <returns></returns>
        protected abstract TRequestInfo FilterRequestBody(byte[] readBuffer, int offset, int length, bool toBeCopied, out int rest);

        /// <summary>
        /// 解析数据.
        /// </summary>
        /// <param name="data">数据大小</param>
        /// <param name="offset">偏移长度</param>
        /// <param name="length">接收的数据长度.</param>
        /// <returns></returns>
        protected override TRequestInfo ProcessMatchedRequest(byte[] data, int offset, int length)
        {
            string header = Encoding.UTF8.GetString(data, offset, length);

            var headerItems = new NameValueCollection();
            MimeHeaderHelper.ParseHttpHeader(header, headerItems);
            HeaderItems = headerItems;

            var requestInfo = FilterRequestHeader(headerItems);

            if (ReferenceEquals(requestInfo, NullRequestInfo))
            {
                m_HeaderParsed = true;
                return requestInfo;
            }

            //Reset the filter if one request info has been parsed
            Reset();
            return requestInfo;
        }


        /// <summary>
        /// 获取请求头.
        /// </summary>
        /// <param name="header">头字典.</param>
        /// <returns>
        /// 返回转换的头
        /// </returns>
        protected virtual TRequestInfo FilterRequestHeader(NameValueCollection header)
        {
            return NullRequestInfo;
        }

        /// <summary>
        /// 重置初始状态
        /// </summary>
        public override void Reset()
        {
            m_HeaderParsed = false;
            HeaderItems = null;
            base.Reset();
        }
    }
}

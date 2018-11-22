using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using DS.AFP.Communication.Facility.Protocol;

namespace DS.AFP.Communication.Http
{
    public class HttpReceiveFilter : HttpReceiveFilterBase<HttpRequestInfo>
    {
        private long m_ContentLength;

        private NameValueCollection m_Header;

        private byte[] m_Body;

        private int m_ReceivedLength = 0;

        protected override HttpRequestInfo FilterRequestBody(byte[] readBuffer, int offset, int length, bool toBeCopied, out int rest)
        {
            rest = 0;
            if (length > 0)
            {
                if(m_Body==null)
                    m_Body = new byte[m_ContentLength];
                if (m_ContentLength <= (m_ReceivedLength+length))
                {

                    Buffer.BlockCopy(readBuffer, offset, m_Body, m_ReceivedLength, Convert.ToInt32(m_ContentLength - m_ReceivedLength));
                }
                else
                {
                    Buffer.BlockCopy(readBuffer, offset, m_Body, m_ReceivedLength, length);
                    m_ReceivedLength += length;
                    return NullRequestInfo;
                }
                HttpRequestInfo httpPage = new HttpRequestInfo(m_Header.Get(HttpHeaderKey.Method), m_Header, m_Body);
                httpPage.Method = m_Header.Get(HttpHeaderKey.Method);
                return httpPage;
            }
            return NullRequestInfo;
        }

        protected override HttpRequestInfo FilterRequestHeader(NameValueCollection header)
        {
            var contentLength = header.Get(HttpHeaderKey.ContentLength);

            if (!string.IsNullOrEmpty(contentLength))
                long.TryParse(contentLength, out m_ContentLength);

            if (m_ContentLength > 0)
            {
                m_Header = header;
                return NullRequestInfo;
            }
            return new HttpRequestInfo(header.Get(HttpHeaderKey.Method), header);
        }

        public override void Reset()
        {
            m_ContentLength = 0;
            m_Header = null;
            base.Reset();
        }
    }
}

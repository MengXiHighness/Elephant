using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.AFP.Communication.SocketNameSpace;
using DS.AFP.Common.Core;
using System.Text.RegularExpressions;
using System.Diagnostics;
using DS.AFP.Communication.SocketBase.Protocol;
using DS.AFP.Communication.Common;
using DS.AFP.Communication.SocketBase;

namespace DS.AFP.Communication.Chain
{
    /// <summary>
    /// 接收过滤器的具体实例
    /// </summary>
    /// <typeparam name="TRequestInfo">The type of the request info.</typeparam>
    public abstract class ChainTerminatorReceiveFilter<TRequestInfo> : ReceiveFilterBase<TRequestInfo>, IOffsetAdapter, IReceiveFilterInitializer
        where TRequestInfo : IRequestInfo
    {
        private readonly SearchMarkState<byte> m_SearchState;

        private IAppSession m_Session;

        /// <summary>
        /// Gets the session assosiated with the Receive filter.
        /// </summary>
        protected IAppSession Session
        {
            get { return m_Session; }
        }

        private ChainHeader chainHeader { get; set; }

        //已接收数据
        //private int receivedLength { get; set; }

        //包的总长度
        private int packLength = 0;

        /// <summary>
        /// Null RequestInfo
        /// </summary>
        protected static readonly TRequestInfo NullRequestInfo = default(TRequestInfo);

        private int m_ParsedLengthInBuffer = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="TerminatorReceiveFilter&lt;TRequestInfo&gt;"/> class.
        /// </summary>
        /// <param name="terminator">The terminator.</param>
        protected ChainTerminatorReceiveFilter(byte[] terminator)
        {
            m_SearchState = new SearchMarkState<byte>(terminator);
        }

        void IReceiveFilterInitializer.Initialize(IAppServer appServer, IAppSession session)
        {
            m_Session = session;
        }


        /// <summary>
        /// Filters received data of the specific session into request info.
        /// </summary>
        /// <param name="readBuffer">The read buffer.</param>
        /// <param name="offset">The offset of the current received data in this read buffer.</param>
        /// <param name="length">The length of the current received data.</param>
        /// <param name="toBeCopied">if set to <c>true</c> [to be copied].</param>
        /// <param name="rest">The rest, the length of the data which hasn't been parsed.</param>
        /// <returns>return the parsed TRequestInfo</returns>
        public override TRequestInfo Filter(byte[] readBuffer, int offset, int length, bool toBeCopied, out int rest)
        {
            rest = 0;

            TRequestInfo requestInfo;

            int prevMatched = m_SearchState.Matched;

            int result = 0;
            var findLen = result - offset;
            int remainlen = 0;
            //Debug.WriteLine("缓存：" + this.BufferSegments.Count);
            //判断缓存是否存在，如果存在说明之前数据没有取完，出现粘包
            if (this.BufferSegments.Count > 0)
            {
                if (packLength == 0)//说明之前的数据只有部分包头信息，没能进行解析包头信息
                {
                    //////////
                    //把缓存的数据加上新收到的数据合并的总接收到的数据，进行查找
                    int receivedLen = length + this.LeftBufferSize;
                    byte[] receivedData = new byte[receivedLen];
                    Buffer.BlockCopy(this.BufferSegments.ToArray(), 0, receivedData, 0, this.BufferSegments.Count);
                    Buffer.BlockCopy(readBuffer, offset, receivedData, this.BufferSegments.Count, length);

                    result = receivedData.SearchMark(0, length, m_SearchState);
                    
                    findLen = result - this.LeftBufferSize;
                    if (findLen > 0)//找到包头的数据
                    {
                        byte[] headerdata = new byte[findLen + this.BufferSegments.Count];
                        Buffer.BlockCopy(this.BufferSegments.ToArray(), 0, headerdata, 0, this.BufferSegments.Count);
                        Buffer.BlockCopy(readBuffer, offset, headerdata, this.BufferSegments.Count, findLen);

                        ChainProtocol hp = new ChainProtocol();
                        chainHeader = hp.GetChainHander(headerdata);
                        if (chainHeader.Method == "GET" || chainHeader.Method == "DELETE")
                        {
                            findLen += m_SearchState.Mark.Length;
                        }
                        else
                        {
                            int contentLen = Convert.ToInt32(chainHeader.Data["Content-Length"]);

                            packLength = headerdata.Length + contentLen + m_SearchState.Mark.Length;

                            //包的长度大于接收的长度，则需要缓存下来等下一个包数据过来进行拼接
                            if (packLength > (length + this.LeftBufferSize))
                            {
                                m_ParsedLengthInBuffer += length;

                                this.AddArraySegment(readBuffer, offset + length - m_ParsedLengthInBuffer, m_ParsedLengthInBuffer, toBeCopied);
                                m_ParsedLengthInBuffer = 0;
                                m_OffsetDelta = 0;

                                return NullRequestInfo;
                            }
                            else if (packLength <= (length + this.LeftBufferSize))//包的长度小于接收的数据长度加上缓存长度，则生成该包，并把多余的数据缓存下来
                            {
                                //包的长度刚好等于接收的数据长度，则生成该包即可
                                //Debug.WriteLine("packLength：{0} < length：{1}".FormatString(packLength,length));
                                findLen = packLength - this.LeftBufferSize;
                            }
                           
                        }
                    }
                    else//第一个包的数据不够一个包头的长度
                    {

                        m_ParsedLengthInBuffer += length;
                        this.AddArraySegment(readBuffer, offset + length - m_ParsedLengthInBuffer, m_ParsedLengthInBuffer, toBeCopied);
                        m_ParsedLengthInBuffer = 0;
                        m_OffsetDelta = 0;
                        return NullRequestInfo;
                    }


                    ///////////
                    
                }
                else
                {
                    //剩余的数据包大小
                    remainlen = packLength - this.BufferSegments.Count;
                    if (remainlen > 0 && remainlen <= length)//说明剩余的数据等于本次接收的数据//剩余数据小于本次接收的数据长度，则需要重新组包
                    {
                        findLen = remainlen;
                    }
                    else if (remainlen > 0 && remainlen > length)//剩余数据大于接收得到数据长度，需要缓存下来，等下一个包过来进行拼接
                    {

                        m_ParsedLengthInBuffer += length;

                        this.AddArraySegment(readBuffer, offset + length - m_ParsedLengthInBuffer, m_ParsedLengthInBuffer, toBeCopied);
                        m_ParsedLengthInBuffer = 0;
                        m_OffsetDelta = 0;
                       //////
                       // m_ParsedLengthInBuffer += length;
                       // m_ParsedLengthInBuffer = 0;
                       // m_OffsetDelta = 0;

                        return NullRequestInfo;

                    }
                    else if (remainlen < 0)//不存在
                    {

                    }
                   
                }
            }
            else//没有缓存则需要直接获取数据
            {
                result = readBuffer.SearchMark(offset, length, m_SearchState);
                findLen = result - offset;
                if (findLen > 0)//找到包头的数据
                {
                    byte[] headerdata = new byte[findLen];
                    Buffer.BlockCopy(readBuffer, offset, headerdata, 0, findLen);
                    ChainProtocol hp = new ChainProtocol();
                    chainHeader = hp.GetChainHander(headerdata);
                    if (chainHeader.Method == "GET" || chainHeader.Method == "DELETE")
                    {
                        findLen += m_SearchState.Mark.Length;
                    }
                    else
                    {
                        int contentLen = Convert.ToInt32(chainHeader.Data["Content-Length"]);

                        packLength = headerdata.Length + contentLen + m_SearchState.Mark.Length;
                        
                        //包的长度大于接收的长度，则需要缓存下来等下一个包数据过来进行拼接
                        if (packLength > length)
                        {
                            m_ParsedLengthInBuffer += length;

                            this.AddArraySegment(readBuffer, offset + length - m_ParsedLengthInBuffer, m_ParsedLengthInBuffer, toBeCopied);
                            m_ParsedLengthInBuffer = 0;
                            m_OffsetDelta = 0;

                            return NullRequestInfo;
                        }
                        else if (packLength < length)//包的长度小于接收的数据长度，则生成该包，并把多余的数据缓存下来
                        {//包的长度刚好等于接收的数据长度，则生成该包即可
                            findLen = packLength;
                            //把多余的数据缓存下来
                           // int remainLength = length - packLength;
                           // this.AddArraySegment(readBuffer, offset + length - remainLength, remainLength, toBeCopied);
                           // m_ParsedLengthInBuffer = 0;
                            //m_OffsetDelta = remainLength;

                        }else if(packLength == length)
                        {
                            findLen = packLength;
                        }
                       
                    }
                }
                else//第一个包的数据不够一个包头的长度
                {
                    m_ParsedLengthInBuffer += length;

                    this.AddArraySegment(readBuffer, offset + length - m_ParsedLengthInBuffer, m_ParsedLengthInBuffer, toBeCopied);
                    m_ParsedLengthInBuffer = 0;
                    m_OffsetDelta = 0;

                    return NullRequestInfo;
                }

            }
            //计算偏移
            rest = length - findLen + prevMatched;


            if (findLen > 0)
            {
                if (this.BufferSegments != null && this.LeftBufferSize > 0)
                {
                    //Debug.WriteLine("包大小{0}，缓存大小{1}，接收大小{2}".FormatString(packLength,this.LeftBufferSize, length));
                    this.AddArraySegment(readBuffer, offset - m_ParsedLengthInBuffer, findLen + m_ParsedLengthInBuffer, toBeCopied);
                    requestInfo = ProcessMatchedRequest(BufferSegments, 0, BufferSegments.Count);
                }
                else
                {
                    requestInfo = ProcessMatchedRequest(readBuffer, offset - m_ParsedLengthInBuffer, findLen + m_ParsedLengthInBuffer);
                }
            }
            else if (prevMatched > 0)
            {
                if (m_ParsedLengthInBuffer > 0)
                {
                    if (m_ParsedLengthInBuffer < prevMatched)
                    {
                        BufferSegments.TrimEnd(prevMatched - m_ParsedLengthInBuffer);
                        requestInfo = ProcessMatchedRequest(BufferSegments, 0, BufferSegments.Count);
                    }
                    else
                    {
                        if (this.BufferSegments != null && this.BufferSegments.Count > 0)
                        {
                            this.AddArraySegment(readBuffer, offset - m_ParsedLengthInBuffer, m_ParsedLengthInBuffer - prevMatched, toBeCopied);
                            requestInfo = ProcessMatchedRequest(BufferSegments, 0, BufferSegments.Count);
                        }
                        else
                        {
                            requestInfo = ProcessMatchedRequest(readBuffer, offset - m_ParsedLengthInBuffer, m_ParsedLengthInBuffer - prevMatched);
                        }
                    }
                }
                else
                {
                    BufferSegments.TrimEnd(prevMatched);
                    requestInfo = ProcessMatchedRequest(BufferSegments, 0, BufferSegments.Count);
                }
            }
            else
            {
                if (this.BufferSegments != null && this.BufferSegments.Count > 0)
                {
                    requestInfo = ProcessMatchedRequest(BufferSegments, 0, BufferSegments.Count);
                }
                else
                {
                    requestInfo = ProcessMatchedRequest(readBuffer, offset - m_ParsedLengthInBuffer, m_ParsedLengthInBuffer);
                }
            }


            InternalReset();
            if (rest == 0)
            {
                m_OffsetDelta = 0;
                
            }
            else
            {
                m_OffsetDelta += (length - rest);
            }

            return requestInfo;
        }

        private void InternalReset()
        {
            m_ParsedLengthInBuffer = 0;
            m_SearchState.Matched = 0;
            //receivedLength = 0;
            packLength = 0;
            base.Reset();
        }

        /// <summary>
        /// Resets this instance.
        /// </summary>
        public override void Reset()
        {
            InternalReset();
            m_OffsetDelta = 0;
        }


        private TRequestInfo ProcessMatchedRequest(ArraySegmentList data, int offset, int length)
        {
            var targetData = data.ToArrayData(offset, length);
            return ProcessMatchedRequest(targetData, 0, length);
        }

        /// <summary>
        /// Resolves the specified data to TRequestInfo.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        protected abstract TRequestInfo ProcessMatchedRequest(byte[] data, int offset, int length);


        private int m_OffsetDelta;

        int IOffsetAdapter.OffsetDelta
        {
            get { return m_OffsetDelta; }
        }
    }

    /// <summary>
    /// TerminatorRequestFilter
    /// </summary>
    public class ChainTerminatorReceiveFilter : ChainTerminatorReceiveFilter<ChainPackage>
    {
        private readonly Encoding m_Encoding;
        private readonly IRequestInfoParser<ChainPackage> m_RequestParser;

        /// <summary>
        /// Initializes a new instance of the <see cref="TerminatorReceiveFilter"/> class.
        /// </summary>
        /// <param name="terminator">The terminator.</param>
        /// <param name="encoding">The encoding.</param>
        //public HttpTerminatorReceiveFilter(byte[] terminator, Encoding encoding)
        //    : this(terminator, encoding, BasicRequestInfoParser.DefaultInstance)
        //{

        //}
        /// <summary>
        /// Initializes a new instance of the <see cref="TerminatorReceiveFilter"/> class.
        /// </summary>
        /// <param name="terminator">The terminator.</param>
        /// <param name="encoding">The encoding.</param>
        /// <param name="requestParser">The request parser.</param>
        public ChainTerminatorReceiveFilter(byte[] terminator, Encoding encoding, IRequestInfoParser<ChainPackage> requestParser)
            : base(terminator)
        {
            m_Encoding = encoding;
            m_RequestParser = requestParser;
        }

        /// <summary>
        /// Resolves the specified data to StringRequestInfo.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        protected override ChainPackage ProcessMatchedRequest(byte[] data, int offset, int length)
        {
            if (length == 0)
                return m_RequestParser.ParseRequestInfo(new byte[0]);
            byte[] f_data = new byte[length];
            Buffer.BlockCopy(data, offset, f_data, 0, length);

            return m_RequestParser.ParseRequestInfo(f_data);
        }
    }
}

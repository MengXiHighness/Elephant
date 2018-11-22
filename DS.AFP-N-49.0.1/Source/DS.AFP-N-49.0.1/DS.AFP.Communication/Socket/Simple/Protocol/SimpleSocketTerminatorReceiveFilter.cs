using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.AFP.Communication.SocketNameSpace;
using DS.AFP.Common.Core;
using System.Text.RegularExpressions;
using DS.AFP.Communication.SocketBase.Protocol;
using DS.AFP.Communication.Common;
using DS.AFP.Communication.SocketBase;

namespace DS.AFP.Communication.SocketNameSpace
{
    /// <summary>
    /// 接收过滤器的具体实例
    /// </summary>
    /// <typeparam name="TRequestInfo">The type of the request info.</typeparam>
    public abstract class SocketTerminatorReceiveFilter<TRequestInfo> : ReceiveFilterBase<TRequestInfo>, IOffsetAdapter, IReceiveFilterInitializer
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

        /// <summary>
        /// Null RequestInfo
        /// </summary>
        protected static readonly TRequestInfo NullRequestInfo = default(TRequestInfo);

        private int m_ParsedLengthInBuffer = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="TerminatorReceiveFilter&lt;TRequestInfo&gt;"/> class.
        /// </summary>
        /// <param name="terminator">The terminator.</param>
		protected SocketTerminatorReceiveFilter(byte[] terminator)
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
			
			TRequestInfo requestInfo = ProcessMatchedRequest(readBuffer, offset, length );
           
            return requestInfo;
        }

        private void InternalReset()
        {
            m_ParsedLengthInBuffer = 0;
            m_SearchState.Matched = 0;
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
    public class SocketTerminatorReceiveFilter : SocketTerminatorReceiveFilter<SocketPackage>
    {
        private readonly Encoding m_Encoding;
        private readonly IRequestInfoParser<SocketPackage> m_RequestParser;

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
		public SocketTerminatorReceiveFilter(byte[] terminator, Encoding encoding, IRequestInfoParser<SocketPackage> requestParser)
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
        protected override SocketPackage ProcessMatchedRequest(byte[] data, int offset, int length)
        {
			if (length == 0)
				return m_RequestParser.ParseRequestInfo(new byte[0]);
			byte[] f_data = new byte[length];
			Buffer.BlockCopy(data, offset, f_data, 0, length);

			return m_RequestParser.ParseRequestInfo(f_data);
        }
    }
}

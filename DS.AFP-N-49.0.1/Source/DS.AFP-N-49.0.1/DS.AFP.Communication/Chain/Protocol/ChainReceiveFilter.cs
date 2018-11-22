using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.AFP.Communication.SocketNameSpace;
using DS.AFP.Communication.SocketNameSpace.Protocol;
using DS.AFP.Common.Core;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace DS.AFP.Communication.Chain
{
    

    /// <summary>
    /// TerminatorRequestFilter
    /// </summary>
    public class ChainReceiveFilter : FixedHeaderReceiveFilter<ChainPackage>
    {
       
        private ChainProtocol protocol = new ChainProtocol();
       
        public ChainReceiveFilter()
            
        {
           
        }

        protected override int GetBodyLengthFromHeader(byte[] header, int offset, int length)
        {
           int bodyLength = 0;
           ChainHeader chainHeader = protocol.GetChainHeader(header);
           if (chainHeader.Method == "GET" || chainHeader.Method == "DELETE")
           {
               bodyLength += 4;
           }
           else
           {
               int contentLen = Convert.ToInt32(chainHeader.Data["Content-Length"]);

               bodyLength = length + contentLen + 4;
           }

           return bodyLength;
        }



        protected override ChainPackage ResolveRequestInfo(ArraySegment<byte> header, byte[] bodyBuffer, int offset, int length)
        {
            byte[] body = new byte[length];
            Buffer.BlockCopy(bodyBuffer, offset, body, 0, length);
            return protocol.ResolveProtocol(header.Array, body);
        }
    }
}

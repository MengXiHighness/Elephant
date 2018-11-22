using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.AFP.Common.Core;
using DS.AFP.Communication.SocketNameSpace.Protocol;

namespace DS.AFP.Communication.SocketNameSpace
{
    /// <summary>
    /// 在已经按SocketParams.Terminator截包后的字符串，中收到有包头的信息
    /// </summary>
    public class CommonRequestInfoParser : IRequestInfoParser<StringRequestInfo>
    {
        private readonly string defaultCommand = SocketParams.DefaultCommand;
        private const string m_Spliter = SocketParams.Spliters;
        private readonly string[] m_ParameterSpliters;

        public CommonRequestInfoParser()
        {
            m_ParameterSpliters = new string[] { m_Spliter };
        }

        #region ICommandParser Members

        public StringRequestInfo ParseRequestInfo(byte[] source)
        {
            int pos = source.IndexOf(m_Spliter);

            string name = string.Empty;
            string param = string.Empty;

            if (pos > 0)
            {
                name = source.Substring(0, pos);
                param = source.Substring(pos + m_Spliter.Length);
            }
            else
            {
                name = defaultCommand;
                param = source;
            }

            return new StringRequestInfo(name, param,
                param.Split(m_ParameterSpliters, StringSplitOptions.RemoveEmptyEntries));
        }

        #endregion
    }
}

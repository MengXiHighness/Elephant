using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.AFP.Common.Core;
using DS.AFP.Communication.SocketBase.Protocol;
using DS.AFP.Communication.Http;

namespace DS.AFP.Communication
{
    public class CommonRequestInfoParser : IRequestInfoParser<HttpRequestInfo>
    {
        private const string m_Spliter = SocketParams.Spliters;
        private readonly string[] m_ParameterSpliters;
        private readonly string defaultCommand = SocketParams.DefaultCommand;
        public CommonRequestInfoParser()
        {
            //defaultCommand = "DefaultCommand";
            m_ParameterSpliters = new string[] { m_Spliter };
        }
        #region ICommandParser Members

        public StringRequestInfo ParseRequestInfo(string source)
        {
            /////解压
            string sourcedata = source;
           

            int pos = sourcedata.IndexOf(m_Spliter);

            string name = string.Empty;
            string param = string.Empty;

            if (pos > 0)
            {
                name = sourcedata.Substring(0, pos);
                param = sourcedata.Substring(pos + 1);
            }
            else
            {
                name = defaultCommand;
                param = sourcedata;
            }
            try
            {
                param = ZipHelper.DecompressString(param);
            }
            catch
            {
                /////不做处理
            }

            return new StringRequestInfo(name, param,
                param.Split(m_ParameterSpliters, StringSplitOptions.RemoveEmptyEntries));


        }

        #endregion
    }
}

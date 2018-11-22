///by 姜宁
///2014-2-11
///

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.AFP.Common.Core;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using DS.AFP.Communication.SocketNameSpace;


namespace DS.AFP.Communication.Chain
{
    /// <summary>
    ///  Chain协议类
    ///  该协议兼容HTTP 1.0协议，可以支持包头的动态数据，和包体的动态数据
    ///  对于包的总的大小也支持动态接收和解析
    ///  
    /// </summary>
    public class ChainProtocol : IPackageProtocol<ChainPackage>
    {

        
        string  _Content = string.Empty;
        private readonly SearchMarkState<byte> m_SearchState = new SearchMarkState<byte>(new byte[] { 13,10,13,10 });

        private byte[] chainCache = null;
        private int cachePackageLen = 0;

       /// <summary>
       /// 获得包头的信息：包的内容长度
       /// </summary>
       /// <param name="readBuffer"></param>
       /// <param name="receiveLen"></param>
       /// <returns></returns>
        public byte[] GetSinglePackage(IList<byte> readBuffer,ref int offset,ref int resolveLen)
        {
            if (chainCache != null && (readBuffer.Count() + chainCache.Count()) >= cachePackageLen)
            {
                byte[] cache_single_package = new byte[cachePackageLen];
                Buffer.BlockCopy(chainCache.ToArray(), 0, cache_single_package, 0, chainCache.Count());
                Buffer.BlockCopy(readBuffer.ToArray(), 0, cache_single_package, chainCache.Count(), cachePackageLen - chainCache.Count());
                offset += cachePackageLen - chainCache.Count();
                resolveLen += offset;
                chainCache = null;
                cachePackageLen = 0;
                return cache_single_package;

            }

            int headlen = readBuffer.SearchMark(offset, readBuffer.Count, m_SearchState);
            if (headlen == -1)
                throw new Exception("Message head is Missed");
            byte[] head_data = new byte[headlen-offset];
            Buffer.BlockCopy(readBuffer.ToArray(), offset, head_data, 0, headlen - offset);
            ChainHeader temp = GetChainHander(head_data);
            int packageLen = headlen - offset + Convert.ToInt32(temp.Data[HeadKeys.ContentLen]) + m_SearchState.Mark.Count();
            byte[] single_package = new byte[packageLen] ;
            if ((readBuffer.Count() - offset) < packageLen && chainCache == null)
            {
                chainCache = new byte[readBuffer.Count() - offset];
                cachePackageLen = packageLen;
                Buffer.BlockCopy(readBuffer.ToArray(), offset, chainCache, 0, readBuffer.Count()-offset);
                return null;
            }
            else {

                Buffer.BlockCopy(readBuffer.ToArray(), offset, single_package, 0, packageLen);
                offset += packageLen;
                resolveLen += packageLen;
            }
            return single_package;
        }

       
        /// <summary>
        /// 得到头数据
        /// </summary>
        /// <param name="packages"></param>
        /// <returns></returns>
        public ChainHeader GetChainHander(byte[] header)
        {
            ChainHeader chainHeader = new ChainHeader();
            NameValueCollection headers = new NameValueCollection();
            string package = Encoding.UTF8.GetString(header);
            string[] tokens = package.Split(' ');
            string[] splits = Regex.Split(package, "\r\n\r\n", RegexOptions.IgnoreCase);
            if (splits.Length > 0)
            {
                string[] heads = Regex.Split(splits[0], "\r\n", RegexOptions.IgnoreCase);
                if (heads.Length > 0)
                {
                    parseRequest(chainHeader, heads[0]);
                    chainHeader.Data.Add(readHeaders(heads));
                }
            }
            return chainHeader;
        }

        Regex reg = new Regex(@"(?<=(\?|&)(?<key>.*?)=)(?<value>.*?)(?=($|&))", RegexOptions.IgnoreCase);

        /// <summary>
        /// 解析HttpHeader，得到其实例
        /// </summary>
        /// <param name="httpHeader"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        NameValueCollection parseRequest(ChainHeader chainHeader, String request)
        {
            NameValueCollection headparams = new NameValueCollection();
            //String request = streamReadLine(inputStream);
            string[] tokens = request.Split(' ');
            if (tokens.Length != 3)
            {
                throw new Exception("invalid http request line");
            }
            //_HttpMethod = tokens[0].ToUpper();
            chainHeader.Method = tokens[0].ToUpper();
            chainHeader.Url = tokens[1];
            MatchCollection mc = reg.Matches(tokens[1]);
           // chainHeader.QueryString.Add(System.Web.HttpUtility.ParseQueryString(tokens[1], Encoding.UTF8));
            foreach (Match m in mc)
            {
                string key = m.Groups["key"].Value;
                string value = m.Groups["value"].Value;
                if (!string.IsNullOrEmpty(key) && headparams.GetValue(key).IsNullOrEmpty())
                    chainHeader.QueryString.Add(key, value);
            }
            chainHeader.Protocol = tokens[2];
            return headparams;
            //http_protocol_versionstring = tokens[2];
        }
       
        /// <summary>
        /// 把byte[]解析成Chain消息包
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        public ChainPackage ResolveProtocol(byte[] package)
        {
            
            int headlen = package.ToList<byte>().SearchMark(0, package.ToList<byte>().Count, m_SearchState);
            if (headlen == -1)
                headlen = package.Length;
            byte[] head_data = new byte[headlen];
            Buffer.BlockCopy(package, 0, head_data, 0, headlen );
           // NameValueCollection _Headers = GetHeader(head_data);
            ChainHeader httpHeader = GetChainHander(head_data);
            ChainPackage chainRequest = null;

            switch (httpHeader.Method)
            {
                case "GET":
                case "DELETE":
                    {
                        chainRequest = new ChainPackage(new byte[0],httpHeader);
                        return chainRequest;
                    }
                case "POST":
                case "PUT":
                    {

                        int contentLen = Convert.ToInt32(httpHeader.Data[HeadKeys.ContentLen]);
                        byte[] contentdata = new byte[contentLen];
                        Buffer.BlockCopy(package, headlen + m_SearchState.Mark.Length, contentdata, 0, contentLen);
                        chainRequest = new ChainPackage(contentdata, httpHeader);
                        return chainRequest;
                    }
                case "CHAIN":
                    {
                        int headlength = headlen + 4;
                        int bodylength = Convert.ToInt32(httpHeader.Data[HeadKeys.ContentLen]);
                        byte[] body = new byte[bodylength];
                        if (bodylength > 0)
                            Buffer.BlockCopy(package, headlength, body, 0, bodylength);
                       
                        if (httpHeader.Data[HeadKeys.CmdName] != null)
                        {
                            chainRequest = new ChainPackage(body, httpHeader.Data[HeadKeys.CmdName], httpHeader);
                        }
                        else
                        {
                            chainRequest = new ChainPackage(body, httpHeader);

                        }
                        return chainRequest;
                    }

            }
        
                  
            return chainRequest;
                     
           
        }

               
        /// <summary>
        /// 把Chain消息包解析成byte[]
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        public byte[] ResolveProtocol(ChainPackage package)
        {
            StringBuilder sb = new StringBuilder("{0} {1} {2}\r\n".FormatString(package.Header.Method, package.Header.Url, package.Header.Protocol));
            //添加head输出
            if (package.Header != null && package.Header.Data.Count > 0)
            {
                foreach (var key in package.Header.Data.AllKeys)
                {
                    if(key==HeadKeys.Action )
                    {
                        sb.Append(string.Format("{0} ", package.Header.Data[key]));
                    }
                    else if (key == HeadKeys.URI)
                    {
                        sb.AppendLine(string.Format("{0}", package.Header.Data[key]));
                    }
                    else
                    {
                        sb.AppendLine(string.Format("{0}:{1}", key, package.Header.Data[key]));
                    }
                }
            }
            sb.AppendLine();
            //byte[] tag = Encoding.UTF8.GetBytes("Send / Chain/1.0 \r\n");
            byte[] head = Encoding.UTF8.GetBytes(sb.ToString());
            byte[] data = new byte[head.Length + package.Body.Length];
            Buffer.BlockCopy(head, 0, data, 0, head.Length);
            Buffer.BlockCopy(package.Body, 0, data, head.Length, package.Body.Length);
            return data;
        }

        /// <summary>
        /// 解析头信息
        /// </summary>
        /// <param name="_Headers"></param>
        /// <param name="header"></param>
        void parseRequest(NameValueCollection _Headers,String header)
        {
            //String request = streamReadLine(inputStream);
            string[] tokens = header.Split(' ');
            if (tokens.Length != 2)
            {
                throw new Exception("invalid http request line");
            }
            _Headers.Add("Action", tokens[0].ToUpper());
            _Headers.Add("URI", tokens[1].ToUpper());
            //_Headers.Add("Protocol", tokens[2].ToUpper());


        }

        NameValueCollection readHeaders(string[] heads)
        {
            NameValueCollection _Headers = new NameValueCollection();
            String[] line;            
            for (int i = 1; i < heads.Length; i++)
            {
                if (heads[i] == "")
                    continue;
                line = heads[i].Split(':');                
                String name = line[0].Trim();
                //Cn: keep-alive
                
                string value = line.Length > 0 ? heads[i].Substring(name.Length + 1) : string.Empty;
                _Headers.Add(name, value);
            }
            return _Headers;
        }

        /// <summary>
        /// 把IList<byte>转成多包
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        public IList<ChainPackage> ResolveProtocol(IList<byte> package)
        {
            int resolveLen = 0;
            IList<ChainPackage> packageList = new List<ChainPackage>();
            int offset =0;
            while (resolveLen < package.Count())
            {
                //try
                //{
                    byte[] singlePackage = GetSinglePackage(package, ref offset,ref resolveLen);
                    //resolveLen += singlePackage.Length;
                    //offset += singlePackage.Length;
                    if (singlePackage == null)
                        return packageList;
                    packageList.Add(ResolveProtocol(singlePackage));
                //}
                //catch { return null; }
            }
            return packageList;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;

namespace DS.AFP.Common.Core.Utility
{
    /// <summary>
    /// Http方法常量（Post、Get、Delete、PUT）
    /// </summary>
    public class HttpMethod
    {
        public const string Post = "POST";
        public const string Get = "GET";
        public const string Delete = "DELETE";
        public const string PUT = "PUT";
    }

    /// <summary>
    /// Http工具类（封装Get、Post等Http常用方法）
    /// </summary>
    public class HttpHelper
    {



        #region post
        public static OT PostAsJson<IT, OT>(string uri, IT value)
        {
            using (var httpClient = new HttpClient())
            {
                var httpResponseMessage = httpClient.PostAsJsonAsync<IT>(uri, value);
                httpResponseMessage.Wait();
                var res = httpResponseMessage.Result.Content.ReadAsAsync<OT>();
                res.Wait();
                return res.Result;
            }
        }


        public static OT PostAsXml<IT, OT>(string uri, IT value)
        {
            using (var httpClient = new HttpClient())
            {
                var httpResponseMessage = httpClient.PostAsXmlAsync<IT>(uri, value);
                httpResponseMessage.Wait();
                var res = httpResponseMessage.Result.Content.ReadAsAsync<OT>();
                res.Wait();
                return res.Result;
            }
        }
        #endregion


        #region get

        static HttpResponseMessage GetHttpResponseMessage(string uri)
        {
            using (var httpClient = new HttpClient())
            {
                var httpResponseMessage = httpClient.GetAsync(uri);
                httpResponseMessage.Wait();
                return httpResponseMessage.Result;
            }
        }

        public static OT Get<OT>(string uri)
        {
            return GetHttpResponseMessage(uri).Content.ReadAsAsync<OT>().Result;
        }

        public static Stream GetStream(string uri)
        {
            return GetHttpResponseMessage(uri).Content.ReadAsStreamAsync().Result;
        }

        public static Byte[] GetBytes(string uri)
        {
            return GetHttpResponseMessage(uri).Content.ReadAsByteArrayAsync().Result;
        }

        public static string GetString(string uri)
        {
            return GetHttpResponseMessage(uri).Content.ReadAsStringAsync().Result;
        }
        #endregion


        #region put
        public static OT PutAsJson<IT, OT>(string uri, IT value)
        {
            using (var httpClient = new HttpClient())
            {
                var httpResponseMessage = httpClient.PutAsJsonAsync<IT>(uri, value);
                httpResponseMessage.Wait();
                var res = httpResponseMessage.Result.Content.ReadAsAsync<OT>();
                res.Wait();
                return res.Result;
            }
        }

        public static OT PutAsXml<IT, OT>(string uri, IT value)
        {
            using (var httpClient = new HttpClient())
            {
                var httpResponseMessage = httpClient.PutAsXmlAsync<IT>(uri, value);
                httpResponseMessage.Wait();
                var res = httpResponseMessage.Result.Content.ReadAsAsync<OT>();
                res.Wait();
                return res.Result;
            }
        }
        #endregion


        #region delete
        public static string Delete(string uri)
        {
            using (var httpClient = new HttpClient())
            {
                var httpResponseMessage = httpClient.DeleteAsync(uri);
                httpResponseMessage.Wait();
                var res = httpResponseMessage.Result.Content.ReadAsStringAsync();
                res.Wait();
                return res.Result;
            }
        }
        #endregion

        ///// <summary>
        ///// 下载一个页面
        ///// </summary>
        ///// <param name="uri"></param>
        ///// <returns></returns>
        //public static string Get(Uri uri)
        //{
        //   // HttpMessageHandler header = new 
        //    //HttpClient hc = new HttpClient();
        //    //hc.GetAsync("").ContinueWith(o => { });
        //    return Request(HttpMethod.Get,uri, null);
        //}

        ///// <summary>
        ///// 下载一个页面
        ///// </summary>
        ///// <param name="url"></param>
        ///// <returns></returns>
        //public static string Get(string url)
        //{
        //    return Request(HttpMethod.Get, new Uri(url), null);
        //}

        ///// <summary>
        ///// Post数据
        ///// </summary>
        ///// <param name="uri"></param>
        ///// <param name="msg"></param>
        ///// <returns></returns>
        //public static string Post(Uri uri, string msg)
        //{
        //    return Request(HttpMethod.Post,uri, msg);
        //}
        ///// <summary>
        ///// Post数据
        ///// </summary>
        ///// <param name="url"></param>
        ///// <param name="msg"></param>
        ///// <returns></returns>
        //public static string Post(string url, string msg)
        //{

        //    return Request(HttpMethod.Post,new Uri(url), msg);
        //}


        ///// <summary>
        ///// 执行请求
        ///// </summary>
        ///// <param name="uri"></param>
        ///// <param name="msg"></param>
        ///// <param name="tryTimes"></param>
        ///// <returns></returns>
        //private static string Request(string method, Uri uri, string msg)
        //{

        //    StringBuilder httpHand = new StringBuilder();
        //    httpHand.Append("{0} / HTTP/1.1\r\n".FormatString(method));
        //    httpHand.Append("Accept: Accept:text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8\r\n");
        //    httpHand.Append("Accept-Language: zh-CN,zh;q=0.\r\n");
        //    httpHand.Append("Accept-Charset: GBK,utf-8;q=0.7,*;q=0.3\r\n");
        //    httpHand.Append("Content-Type: application/x-www-form-urlencoded\r\n");
        //    httpHand.Append("Host: {0}\r\n".FormatString(uri.Host));
        //    if(msg!=null)
        //         httpHand.Append("Content-Length: {0}\r\n".FormatString(msg.Length));
        //    httpHand.Append("Connection: Close");
        //    if (msg != null)
        //         httpHand.Append("\r\n\r\n{0}".FormatString(msg.ToUTF8()));
        //    return InnerRequest(uri.Host, uri.Port, httpHand.ToString(), Encoding.UTF8);

        //}


        ///// <summary>
        ///// 发出请求并获取响应
        ///// </summary>
        ///// <param name="host"></param>
        ///// <param name="port"></param>
        ///// <param name="body"></param>
        ///// <param name="encode"></param>
        ///// <returns></returns>
        //private static string InnerRequest(string host, int port, string body, Encoding encode)
        //{
        //    string strResult = string.Empty;
        //    byte[] bteSend = encode.GetBytes(body);
        //    byte[] bteReceive = new byte[1024];
        //    int intLen = 0;
        //    byte[] tempReceive = new byte[1024];

        //    using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
        //    {
        //        try
        //        {
        //            socket.Connect(host, port);
        //            if (socket.Connected)
        //            {
        //                int sendresult = socket.Send(bteSend, bteSend.Length, 0);
        //                int start = 0;
        //                int i = 0;
        //                while ((intLen = socket.Receive(bteReceive, bteReceive.Length, 0 )) > 0)
        //                {

        //                    if(tempReceive!=null)
        //                        tempReceive = new byte[((tempReceive.Length*i) + intLen)];
        //                    start += intLen*i ;
        //                    bteReceive.CopyTo(tempReceive, start);
        //                    i++;
        //                }
        //            }
        //            socket.Close();
        //        }
        //        catch { }
        //    }

        //    return strResult;
        //}



    }
}

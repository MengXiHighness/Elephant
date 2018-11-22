using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.AFP.Communication.SocketNameSpace
{
    /// <summary>
    /// string与byte[]的转换
    /// </summary>
    public static class StringEncoding
    {
        /// <summary>
        /// 按指定编码格式，将string转换成byte[]
        /// </summary>
        /// <param name="str">待转换的string</param>
        /// <param name="encodeing">编码格式</param>
        /// <returns>转换后的byte[]</returns>
        public static  byte[] ToEncoding(this string str,Encoding encodeing)
        {
           return encodeing.GetBytes(str);
        }
    }
}

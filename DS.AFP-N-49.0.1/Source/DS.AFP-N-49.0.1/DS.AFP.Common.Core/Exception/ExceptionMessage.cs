using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.AFP.Common.Core
{
    /// <summary>
    /// 异常的描述信息
    /// </summary>
    public class ExceptionMessage
    {
        public static KeyValuePair<string, string> C00001 = new KeyValuePair<string, string>("C00001", "");
        public static KeyValuePair<string, string> C00002;
        public static KeyValuePair<string, string> AFP00000;


        static ExceptionMessage()
        {
          
            IResource res = new Resource("","ExceptionResource");
            AFP00000 = new KeyValuePair<string, string>("AFP00000", res.GetString("AFP00000"));
            C00001 = new KeyValuePair<string, string>("C00001", "");
            C00002 = new KeyValuePair<string, string>("C00002", res.GetString("C00002"));
        }
    }
}

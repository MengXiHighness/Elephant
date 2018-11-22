using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.AFP.Communication.Chain
{
    public static class ChainPackageExtension
    {
        public static byte[] ToHtmlByte(this ChainPackage package)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("HTTP/1.0 200 OK");
            if (package.Header != null && package.Header.Data.Count > 0)
            {
                foreach (var key in package.Header.Data.AllKeys)
                {
                    if (key == HeadKeys.Action)
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
            sb.AppendLine("Connection: close");
            sb.AppendLine("");
            byte[] head = Encoding.UTF8.GetBytes(sb.ToString());
            byte[] data = new byte[head.Length + package.Body.Length];
            Buffer.BlockCopy(head, 0, data, 0, head.Length);
            return data;
        }
    }
}

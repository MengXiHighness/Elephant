using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DS.AFP.Common.Core.Utility
{
    /// <summary>
    /// 文件帮助类（提供把指定内容字符串生成到指定路径的文件的方法）
    /// </summary>
    public class FileHelper
    {
        /// <summary>
        /// 把指定内容字符串生成到指定路径的文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="content"></param>
        public static void CreateFile(string fileName, string content)
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            byte[] data = Encoding.UTF8.GetBytes(content);
            using (FileStream stream = File.Create(fileName, data.Length, FileOptions.WriteThrough))
            {
                stream.Write(data, 0, data.Length);
            }
        }

    }
}

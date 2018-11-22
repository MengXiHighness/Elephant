using System;
using System.IO;
using System.Diagnostics;
using Microsoft.Win32;
using System.Text;
using System.IO.Compression;
using Ionic.Zip;

///压缩、解压缩类
namespace DS.AFP.Common.Core.Utility
{
    /// <summary>
    /// 压缩帮助类（支持ZIP的压缩与解压）
    /// </summary>
    public class ZipHelper
    {
        /// <summary>
        /// 压缩
        /// </summary>
        /// <param name="FileToZip">待压缩的文件目录</param>
        /// <param name="ZipedFile">生成的目标文件</param>
        /// <param name="level">压缩等级</param>
        public static bool Zip(String FileToZip, String ZipedFile, int level = 6)
        {
            return FastZip(FileToZip, ZipedFile);
        }

        /// <summary>
        /// 压缩
        /// </summary>
        /// <param name="FileToZip">待压缩的文件目录</param>
        /// <param name="ZipedFile">生成的目标文件</param>
        /// <param name="encoding">编码格式</param>
        public static bool FastZip(String FileOrDirToZip, String ZipedFile, string Password = null, Encoding encoding = null)
        {
            try
            {
                Encoding encodingtemp = encoding;
                if (encoding == null)
                {
                    encodingtemp = Encoding.Default;
                }
                using (ZipFile zip = new ZipFile(ZipedFile, Encoding.Default))
                {
                    //加密压缩  
                    if (!string.IsNullOrEmpty(Password))
                    {
                        zip.Password = Password;
                    }
                    if (Directory.Exists(FileOrDirToZip))
                    {
                        //将要压缩的文件夹添加到zip对象中去(要压缩的文件夹路径和名称)  
                        zip.AddDirectory(FileOrDirToZip);
                    }
                    else if (File.Exists(FileOrDirToZip))
                    {
                        //将要压缩的文件添加到zip对象中去,如果文件不存在抛错FileNotFoundExcept  
                        zip.AddFile(FileOrDirToZip);
                    }
                    zip.Save();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 解压
        /// </summary>
        /// <param name="FileToUpZip">待解压的文件</param>
        /// <param name="ZipedFolder">解压目标存放目录</param>
        public static void UnZip(string FileToUpZip, string ZipedFolder)
        {
            FastUnZip(FileToUpZip, ZipedFolder);
        }

        /// <summary>
        /// 解压
        /// </summary>
        /// <param name="FileToUpZip">待解压的文件</param>
        /// <param name="ZipedFolder">解压目标存放目录</param>
        public static void FastUnZip(string FileToUpZip, string ZipedFolder, string Password = null, Encoding encoding = null)
        {
            if (!File.Exists(FileToUpZip))
            {
                return;
            }
            if (!Directory.Exists(ZipedFolder))
            {
                Directory.CreateDirectory(ZipedFolder);
            }
            try
            {
                ReadOptions ro = new ReadOptions() { Encoding = Encoding.Default };
                if (null != encoding)
                {
                    ro.Encoding = encoding;
                }
                using (ZipFile zip = ZipFile.Read(FileToUpZip, ro))
                {
                    zip.Password = Password;//密码解压  
                    foreach (ZipEntry entry in zip)
                    {
                        //Extract解压zip文件包的方法，参数是保存解压后文件的路基  
                        entry.Extract(ZipedFolder);
                    }
                }
            }
            catch
            {
            }
        }


        /// <summary>   
        /// 对字符串进行压缩   
        /// </summary>
        /// <param name="str">待压缩的字符串</param>   
        /// <returns>压缩后的字符串</returns>   
        public static string CompressString(string str)
        {
            if (str.IsNullOrEmpty())
                return str;
            byte[] bycompress = ZipStr(str);
            return Convert.ToBase64String(bycompress);
        }

        /// <summary>
        /// 对字符串进行解压缩   
        /// </summary>
        /// <param name="compressString">待解压缩的字符串</param>
        /// <returns>解压缩后的字符串</returns>
        public static string DecompressString(string compressString)
        {
            if (compressString.IsNullOrEmpty())
                return compressString;
            byte[] bys = Convert.FromBase64String(compressString);

            return UnZipStr(bys);
        }

        /// <summary>
        /// 将字符串压缩成byte[]
        /// </summary>
        public static byte[] ZipStr(String str)
        {
            using (MemoryStream output = new MemoryStream())
            {
                using (DeflateStream gzip =
                  new DeflateStream(output, CompressionMode.Compress))
                {
                    using (StreamWriter writer =
                      new StreamWriter(gzip, System.Text.Encoding.UTF8))
                    {
                        writer.Write(str);
                    }
                }

                return output.ToArray();
            }
        }

        /// <summary>
        /// 将byte[]解压成字符串
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string UnZipStr(byte[] input)
        {
            using (MemoryStream inputStream = new MemoryStream(input))
            {
                using (DeflateStream gzip =
                 new DeflateStream(inputStream, CompressionMode.Decompress))
                {
                    using (StreamReader reader =
                      new StreamReader(gzip, System.Text.Encoding.UTF8))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }

    }
}
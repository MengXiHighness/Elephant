using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using DS.AFP.Common.Core.ConfigurationNameSpace;
using System.Web;
using System.Reflection;


namespace DS.AFP.Common.Core
{
    /// <summary>
    /// 文件路径帮助类（主要功能：获取平台根目录、根据相对路径获取绝对路径、获取文件Uri）
    /// </summary>
    public class PathHelper
    {
        public const string RefFilePrefix = "file://";

        /// <summary>
        /// 获取平台根目录
        /// </summary>
        /// <returns></returns>
        public static string GetRootPath()
        {
            string rootPath = AppDomain.CurrentDomain.BaseDirectory;

            return rootPath;
        }

        /// <summary>
        /// 根据相对路径获取绝对路径
        /// <example>
        /// 
        /// </example>
        /// </summary>
        /// <param name="assemblyFullName">相对路径</param>
        /// <returns></returns>
        public static string GetFullPath(string relativePath)
        {
            string fullPath = null;

            string rootPath = GetRootPath();

            bool isWebApp = false;

            if (AppDomain.CurrentDomain.ShadowCopyFiles)
            {
                isWebApp = (HttpContext.Current != null);
            }

            if (isWebApp)
            {
                fullPath = rootPath + "/bin/" + relativePath;
            }
            else
            {
                fullPath = rootPath + relativePath;
            }

            fullPath = Path.GetFullPath(fullPath);

            return fullPath;
        }

        /// <summary>
        /// 获取文件Uri
        /// </summary>
        /// <param name="filePath">完整路径或相对路径</param>
        /// <param name="isFileFullPath">只是是否是完整路径</param>
        /// <returns></returns>
        public static string GetFileAbsoluteUri(string filePath,bool isFileFullPath = false)
        {
            UriBuilder uriBuilder = new UriBuilder();
            uriBuilder.Host = String.Empty;
            uriBuilder.Scheme = Uri.UriSchemeFile;
            uriBuilder.Path = ((isFileFullPath) ? filePath : PathHelper.GetFullPath(filePath));//Path.Combine(PathHelper.GetRootPath(),filePath);
            Uri fileUri = uriBuilder.Uri;
            return fileUri.ToString();
        }

      
        /// <summary>
        /// 获取我的文档目录
        /// </summary>
        /// <returns></returns>
        public static string GetMyDocumentUri()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }
            
               
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;

namespace DS.AFP.Common.Core
{
    /// <summary>
    /// 资源获取组件（获取string、object、Stream资源）
    /// </summary>
    public class Resource:IResource
    {
        private ResourceManager rm = null;
        public Resource(string relativePath, string resourceBaseName)
        {
            if (Thread.CurrentThread.CurrentCulture == null)
            {
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(Cultures.zhCN);
            }
            string currentCulture = Thread.CurrentThread.CurrentCulture.Name;
            if(relativePath!="")
                rm = ResourceManager.CreateFileBasedResourceManager(resourceBaseName, PathHelper.GetFullPath(Path.Combine("/",relativePath,"/", currentCulture, "/")), null);
            else
                rm = ResourceManager.CreateFileBasedResourceManager(resourceBaseName, PathHelper.GetFullPath(Path.Combine("/", currentCulture, "/")), null);

        }

        /// <summary>
        /// 获取字符串资源
        /// <code>
        /// res.GetString("AFP00000");
        /// </code>
        /// </summary>
        /// <param name="resKey">资源名</param>
        /// <returns>资源</returns>
        public string GetString(string resKey)
        {
            return rm.GetString(resKey);
        }

        /// <summary>
        /// 获取object资源
        /// <code>
        /// res.GetObject("AFP00000"));
        /// </code>
        /// </summary>
        /// <param name="resKey">资源名</param>
        /// <returns>资源</returns>
        public object GetObject(string resKey)
        {
            return rm.GetObject(resKey);
        }

        /// <summary>
        /// 获取Stream资源
        /// <code>
        /// res.GetStream("AFP00000"));
        /// </code>
        /// </summary>
        /// <param name="resKey">资源名</param>
        /// <returns>资源</returns>
        public System.IO.Stream GetStream(string resKey)
        {
            return rm.GetStream(resKey);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.AFP.Common.Core
{
    /// <summary>
    /// 共享信息（主要属性有版本号、分类、配置key、内容）
    /// <code>
    ///  SyncInfo syncInfo = dsEnvironment.ShareData.FirstOrDefault(o => o.GroupKey == "ds.afp.sys" && o.ConfigKey == configKey);
    ///  FileHelper.CreateFile(AddinInfo.ConfigurationFilePath, syncInfo.Content);
    /// </code>
    /// </summary>
    public class SyncInfo
    {
       
        /// <summary>
        /// 版本号
        /// </summary>
        public double Version
        {
            get;
            set;
        }

        /// <summary>
        /// 分类
        /// </summary>
        public string GroupKey
        {
            get;
            set;
        }

        /// <summary>
        /// 配置key
        /// </summary>
        public string ConfigKey
        {
            get;
            set;
        }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content
        {
            get;
            set;
        }
    }
}

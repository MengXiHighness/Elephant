using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Web.Configuration;

namespace DS.AFP.Common.Core.ConfigurationNameSpace
{
    /// <summary>
    /// 配置项缓存类（主属性有缓存中配置对象、是否纳入文件监控队列、配置项类型等）
    /// <code>
    /// ConfigurationItem ci;
    /// ci = configurationItemCache[key];
    /// TConfig tconfig = ci.Configuration.GetSection(sectionName) as TConfig;
    /// </code>
    /// </summary>
    public class ConfigurationItem
    {
        public string ConfigurationFilePath { get; set; }

        /// <summary>
        /// 获取缓存中的配置对象
        /// </summary>
        public System.Configuration.Configuration Configuration 
        {
            get
            {
                ObjectCache configurationCache = MemoryCache.Default;
                System.Configuration.Configuration config = configurationCache[ConfigurationFilePath] as System.Configuration.Configuration;
                if (config == null)
                {
                    CacheItemPolicy policy = new CacheItemPolicy();
                    HostFileChangeMonitor hfcm = new HostFileChangeMonitor(new List<string> { ConfigurationFilePath });
                    policy.ChangeMonitors.Add(hfcm);
                    if (File.Exists(ConfigurationFilePath))
                    {
                        //if (ConfigurationFilePath.IndexOf("web.config") != -1)
                        //{
                        //    config = WebConfigurationManager.OpenWebConfiguration("/web.config");
                        //}
                        //else
                        //{
                        //    config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationFilePath.TrimEndString(".config",true));
                        //}
                        ExeConfigurationFileMap ecfm = new ExeConfigurationFileMap()
                        {
                            ExeConfigFilename = ConfigurationFilePath
                        };
                        config = ConfigurationManager.OpenMappedExeConfiguration(ecfm,ConfigurationUserLevel.None);
                        
                        configurationCache.Set(ConfigurationFilePath, config, policy);
                    }
                    else
                    {
                        throw new ConfigurationFileNotExistException();
                    }
                }
                return config;

            }

        }
        /// <summary>
        /// 是否纳入文件监控队列中
        /// </summary>
        private bool isWatch = false;
        public bool IsWatch
        {
            get
            {
                return isWatch;
            }
            set
            {
                this.isWatch = value;
            }
        }

        public string Type { get; set; }
    }
}

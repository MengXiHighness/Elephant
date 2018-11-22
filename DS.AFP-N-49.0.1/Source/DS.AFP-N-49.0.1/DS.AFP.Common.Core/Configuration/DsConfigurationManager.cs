///<summary>
/// Copyright (c) 2011-2015 上海迪爱斯通信设备有限公司
/// 作  者：姜宁
/// 时  间：2013-11-13 11:13:09
/// 描  述：平台配置管理类
///</summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;
using System.Runtime.Caching;
using System.Collections.Concurrent;
using DS.AFP.Common.Core;
using DS.AFP.Common.Core.Utility;

namespace DS.AFP.Common.Core.ConfigurationNameSpace
{
    /// <summary>
    /// 平台配置管理类（支持配置项的缓存和动态监控功能）
    /// </summary>
    public class DsConfigurationManager:IDsConfigurationManager
    {
        //private static FileSystemWatcher watcher = new FileSystemWatcher();
        private static CacheItemPolicy policy = new CacheItemPolicy();
        private static ThreadSafeDictionary<string, ConfigurationItem> configurationItemCache = new ThreadSafeDictionary<string, ConfigurationItem>(new Dictionary<string, ConfigurationItem>());
      
        static DsConfigurationManager()
        {
            HostFileChangeMonitor hfcm = new HostFileChangeMonitor(new List<string> { ConfigurationFileHelper.GetRootConfigurationFilePath()});
            policy.ChangeMonitors.Add(hfcm);

            //FileSystemWatcher watcher = new FileSystemWatcher();
            //watcher.Path = watchPath;
            //watcher.Filter = "*.config";
            //watcher.NotifyFilter = NotifyFilters.Size;
            //watcher.Changed += new FileSystemEventHandler(watcher_Changed);
            //watcher.EnableRaisingEvents = true;
        }

        static void watcher_Changed(object sender, FileSystemEventArgs e)
        {
            //if (fileNameCache.ContainsKey(e.Name))
            //{
            //    string sectionName = fileNameCache[e.Name];
            //    sectionCache[sectionName] = ConfigurationManager.GetSection(sectionName);
            //}
        }

        /// <summary>
        /// 根据配置路径和段落名称获取配置对象
        /// </summary>
        /// <typeparam name="TConfig">配置对象</typeparam>
        /// <param name="configurationPath">配置文件全路径</param>
        /// <returns>配置对象</returns>
        public TConfig Get<TConfig>(string configurationPath) where TConfig : class
        {
            TConfig tconfig = Get<TConfig>(configurationPath, ConfigurationType.Addin, "");
            return tconfig;
        }
      
        /// <summary>
        /// 根据配置路径和段落名称获取配置对象
        /// </summary>
        /// <typeparam name="TConfig">配置对象</typeparam>
        /// <param name="configurationPath">配置文件全路径</param>
        /// <param name="sectionName">段落名称</param>
        /// <returns>配置对象</returns>
        public TConfig Get<TConfig>(string configurationPath,string sectionName) where TConfig : class
        {
            TConfig tconfig = Get<TConfig>(configurationPath, ConfigurationType.Addin, sectionName);
            return tconfig;
        }
        /// <summary>
        /// 根据配置路径和段落名称获取配置对象
        /// </summary>
        /// <typeparam name="TConfig"></typeparam>
        /// <param name="configurationPath"></param>
        /// <param name="type">区分平台和插件</param>
        /// <param name="sectionName"></param>
        /// <returns></returns>
        private TConfig Get<TConfig>(string configurationPath, ConfigurationType type, string sectionName) where TConfig : class
        {
            string key = configurationPath.ToLower();
            TConfig tconfig;
            ConfigurationItem ci;
            if (configurationItemCache.ContainsKey(key))
            {
                ci = configurationItemCache[key];
            }
            else
            {
                ci = new ConfigurationItem()
                {
                    ConfigurationFilePath = configurationPath,
                    Type=type.ToDescription()
                };
                configurationItemCache.Add(key, ci);
            }
            if (sectionName != "")
            {
                tconfig = ci.Configuration.GetSection(sectionName) as TConfig;
                if (tconfig == null)
                    throw new ConfigurationErrorsException("The configuration file {0} Lack of configuration section {1}".FormatString(ci.ConfigurationFilePath, sectionName));
            }
            else
                tconfig = ci.Configuration as TConfig;
            return tconfig;
        } 
        /// <summary>
        /// 根据配置路径注册配置对象
        /// </summary>
        /// <param name="configurationPath">配置文件全路径</param>
        /// <param name="configType">区分平台和插件</param>
        /// <returns>是否注册成功</returns>
        public bool RegisterConfigurationObject(string configurationPath,ConfigurationType configType)
        {
            string key = configurationPath.ToLower();
            ConfigurationItem ci = new ConfigurationItem()
            {
                ConfigurationFilePath = configurationPath,
                Type = configType.ToDescription()
            };
            if (!configurationItemCache.ContainsKey(key))
            {
                configurationItemCache.Add(key, ci);
                return false;
            }
            else
            {
                return false;
            }
        }      

        /// <summary>
        /// 获得平台配置对象
        /// </summary>
        public IDsConfigurationSection DsRootConfigurationSection
        {
            get
            {
                return Get<IDsConfigurationSection>(ConfigurationFileHelper.GetRootConfigurationFilePath(),ConfigurationType.Platform, GlobalParams.DsConfigurationSection);
            }           
        }

    }
}

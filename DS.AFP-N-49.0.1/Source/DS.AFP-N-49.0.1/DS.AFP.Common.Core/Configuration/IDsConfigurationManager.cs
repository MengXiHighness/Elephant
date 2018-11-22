using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Reflection;

namespace DS.AFP.Common.Core.ConfigurationNameSpace
{
    /// <summary>
    /// 平台配置管理类接口，支持配置项的缓存和动态监控功能（DsConfigurationManager基类）
    /// </summary>
    public interface IDsConfigurationManager
    {
        /// <summary>
        /// 获取配置类
        /// </summary>
        /// <typeparam name="TConfig"></typeparam>
        /// <param name="sectionName"></param>
        /// <returns>配置类</returns>
        TConfig Get<TConfig>(string configurationPath, string sectionName) where TConfig : class;

        /// <summary>
        /// 根据路径加载配置对象，并缓存到配置队列中。如果配置队列中存在则更新
        /// </summary>
        /// <param name="configPath">路径</param>
        bool RegisterConfigurationObject(string configurationPath, ConfigurationType configType);


        /// <summary>
        /// 获取默认DS配置对象
        /// </summary>
        IDsConfigurationSection DsRootConfigurationSection { get; } 

    }
}

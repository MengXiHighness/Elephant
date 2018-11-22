///<summary>
/// Copyright (c) 2011-2015 上海迪爱斯通信设备有限公司
/// 作  者：姜宁
/// 时  间：2013-8-24 10:13:09
/// 描  述：插件配置组类
///</summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Reflection;

namespace DS.AFP.Common.Core.ConfigurationNameSpace
{
    /// <summary>
    /// 插件配置组（主属性有：系统参数配置项、配置名称、是否同步系统配置、配置文件版本）
    /// </summary>
    public class DsAddinConfigurationSection : ConfigurationSection, IDsAddinConfigurationSection
    {
        /// <summary>
        /// 系统参数配置项
        /// </summary>
        [ConfigurationProperty("params")]
        public ParamsCollection Params
        {
            get
            {
                return (ParamsCollection)base["params"];
            }

        }

        /// <summary>
        /// 配置名称
        /// </summary>
        [ConfigurationProperty("name", DefaultValue = "")]
        public string Name
        {
            get
            { return (string)base["name"]; }
            set
            { base["name"] = value; }
        }

        /// <summary>
        /// 是否同步系统配置
        /// </summary>
        [ConfigurationProperty("sync", DefaultValue = true)]
        public bool Sync
        {
            get
            { return (bool)base["sync"]; }
            set
            { base["sync"] = value; }
        }

        /// <summary>
        /// 配置文件版本
        /// </summary>
        [ConfigurationProperty("version", DefaultValue = 1.0)]
        public double Version
        {
            get
            { return (double)base["version"]; }
            set
            { base["version"] = value; }
        }


        /// <summary>
        /// 主题
        /// </summary>
        [ConfigurationProperty("themes")]
        public ThemeCollection Themes
        {
            get
            {
                return (ThemeCollection)base["themes"];
            }
        }
    }
}

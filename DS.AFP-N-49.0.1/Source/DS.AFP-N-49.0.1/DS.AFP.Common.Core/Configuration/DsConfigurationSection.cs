///<summary>
/// Copyright (c) 2011-2015 上海迪爱斯通信设备有限公司
/// 作  者：姜宁
/// 时  间：2013-11-13 11:13:09
/// 描  述：平台配置组类
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
    /// 平台配置组（含系统参数配置项、数据库连接串配置项、通信节点配置项、模块配置项等）
    /// </summary>
    public class DsConfigurationSection : ConfigurationSection, IDsConfigurationSection
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
        /// 数据库连接串配置项
        /// </summary>
        [ConfigurationProperty("connections")]
        public ConnectionsCollection Connections
        {
            get
            {
                return (ConnectionsCollection)base["connections"];
            }

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

        /// <summary>
        /// 通信节点配置项
        /// </summary>
        [ConfigurationProperty("communications")]
        public CommunicationsCollection Communications
        {
            get
            {
                return (CommunicationsCollection)base["communications"];
            }

        }
        /// <summary>
        /// 模块配置项
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [ConfigurationProperty("modules", IsDefaultCollection = true, IsKey = false)]
        public ModuleConfigurationElementCollection Modules
        {
            get { return (ModuleConfigurationElementCollection)base["modules"]; }
            set { base["modules"] = value; }
        }

        [ConfigurationProperty("name")]
        public string Name
        {
            get
            { return (String)base["name"]; }
            set
            { base["name"] = value; }
        }

        [ConfigurationProperty("trace", DefaultValue = true)]
        public bool Trace
        {
            get
            { return (bool)base["trace"]; }
            set
            { base["trace"] = value; }
        }


        [ConfigurationProperty("nodeType", DefaultValue = "server")]
        public string NodeType
        {
            get
            { return (string)base["nodeType"]; }
            set
            { base["nodeType"] = value; }
        }


        [ConfigurationProperty("description", DefaultValue = "")]
        public string Description
        {
            get
            { return (string)base["description"]; }
            set
            { base["description"] = value; }
        }


        [ConfigurationProperty("version", DefaultValue = "")]
        public string Version
        {
            get
            { return (string)base["version"]; }
            set
            { base["version"] = value; }
        }

    }
}

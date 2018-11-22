///<summary>
/// Copyright (c) 2011-2015 上海迪爱斯通信设备有限公司
/// 作  者：姜宁
/// 时  间：2013-11-13 11:13:09
/// 描  述：Windows服务配置元素类
///</summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace DS.AFP.Common.Core.ConfigurationNameSpace
{
    /// <summary>
    /// Windows服务配置元素类（用于Windows服务配置）
    /// <code>
    /// return (WindowsServiceConfigurationElement)base["service"]; 
    /// </code>
    /// </summary>
    public class WindowsServiceConfigurationElement : ConfigurationElement
    {
        /// <summary>
        /// 服务名称
        /// </summary>
        [ConfigurationProperty("serviceName")]
        public string ServiceName
        {
            get
            { return (string)this["serviceName"]; }
            set
            { this["serviceName"] = value; }
        }

        /// <summary>
        /// 显示名称
        /// </summary>
        [ConfigurationProperty("displayName")]
        public string DisplayName
        {
            get
            { return (string)this["displayName"]; }
            set
            { this["displayName"] = value; }
        }

        /// <summary>
        /// 显示名称
        /// </summary>
        [ConfigurationProperty("monitor")]
        public bool Monitor
        {
            get
            { return (bool)this["monitor"]; }
            set
            { this["monitor"] = value; }
        }

    }
}

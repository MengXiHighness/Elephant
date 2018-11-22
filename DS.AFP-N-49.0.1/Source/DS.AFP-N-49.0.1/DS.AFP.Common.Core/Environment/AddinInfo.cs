///<summary>
/// Copyright (c) 2011-2015 上海迪爱斯通信设备有限公司
/// 作  者：姜宁
/// 时  间：2013-11-13 11:13:09
/// 描  述：插件信息
///</summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.AFP.Common.Core.ConfigurationNameSpace;

namespace DS.AFP.Common.Core
{
    /// <summary>
    /// 插件的基础信息（主属性有插件路径、插件配置文件路径、插件名称、插件配置项）
    /// <code>
    /// AddinInfo addin = de.AddinInfos[AddinInfo.AddinName];
    /// addin.ConfigurationFilePath = this.AddinInfo.ConfigurationFilePath;
    /// </code>
    /// </summary>
    public class AddinInfo
    {
        public AddinInfo()
        {
            //ShareConfigure = new ThreadSafeDictionary<string, string>(new Dictionary<string, string>());
        }

        public string AddinAssemblyName { get; set; }

        public string AddinNameSpace { get; set; }

        /// <summary>
        /// 插件路径
        /// </summary>
        public string AddinPath { get; set; }

        /// <summary>
        /// 插件配置文件路径
        /// </summary>
        public string ConfigurationFilePath { get; set; }

        /// <summary>
        /// 插件名称
        /// </summary>
        public string AddinName { get; set; }

        /// <summary>
        /// 插件配置项
        /// </summary>
        public ModuleConfigurationElement AddinConfigurationEle { get; set; }
        

    }
}

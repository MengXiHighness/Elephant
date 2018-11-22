///<summary>
/// Copyright (c) 2011-2015 上海迪爱斯通信设备有限公司
/// 作  者：姜宁
/// 时  间：2013-9-14 14:13:09
/// 描  述：模块配置类
///</summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace DS.AFP.Common.Core.ConfigurationNameSpace
{
    /// <summary>
    /// 插件配置类（用于插件配置）
    /// </summary>
    public class ModuleConfigurationElement : ConfigurationElement
    {
        public ModuleConfigurationElement()
        {
        }

        public ModuleConfigurationElement(string assemblyFile, string moduleType, string moduleName, bool startupLoaded)
        {
            base["assemblyFile"] = assemblyFile;
            base["moduleType"] = moduleType;
            base["moduleName"] = moduleName;
            base["startupLoaded"] = startupLoaded;
        }

       
        [ConfigurationProperty("assemblyFile", IsRequired = true)]
        public string AssemblyFile
        {
            get { return (string)base["assemblyFile"]; }
            set { base["assemblyFile"] = value; }
        }

        [ConfigurationProperty("moduleType", IsRequired = true)]
        public string ModuleType
        {
            get { return (string)base["moduleType"]; }
            set { base["moduleType"] = value; }
        }

        [ConfigurationProperty("moduleName", IsRequired = true)]
        public string ModuleName
        {
            get { return (string)base["moduleName"]; }
            set { base["moduleName"] = value; }
        }

        [ConfigurationProperty("startupLoaded", IsRequired = false, DefaultValue = true)]
        public bool StartupLoaded
        {
            get { return (bool)base["startupLoaded"]; }
            set { base["startupLoaded"] = value; }
        }

        [ConfigurationProperty("index", IsRequired = false, DefaultValue = 1000)]
        public int Index
        {
            get { return (int)base["index"]; }
            set { base["index"] = value; }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [ConfigurationProperty("dependencies", IsDefaultCollection = true, IsKey = false)]
        public ModuleDependencyCollection Dependencies
        {
            get { return (ModuleDependencyCollection)base["dependencies"]; }
            set { base["dependencies"] = value; }
        }
    }
}

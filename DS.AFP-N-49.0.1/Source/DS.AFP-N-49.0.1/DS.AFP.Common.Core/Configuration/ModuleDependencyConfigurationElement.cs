using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace DS.AFP.Common.Core.ConfigurationNameSpace
{
    /// <summary>
    /// 插件依赖配置元素（用于插件配置）
    /// <code>
    /// foreach (ModuleDependencyConfigurationElement dependency in element.Dependencies)
    /// {
    /// dependencies.Add(dependency.ModuleName);
    /// }
    ///</code>
    /// </summary>
    public class ModuleDependencyConfigurationElement : ConfigurationElement
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ModuleDependencyConfigurationElement"/>.
        /// </summary>
        public ModuleDependencyConfigurationElement()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ModuleDependencyConfigurationElement"/>.
        /// </summary>
        /// <param name="moduleName">A module name.</param>
        public ModuleDependencyConfigurationElement(string moduleName)
        {
            base["moduleName"] = moduleName;
        }

        /// <summary>
        /// Gets or sets the name of a module antoher module depends on.
        /// </summary>
        /// <value>The name of a module antoher module depends on.</value>
        [ConfigurationProperty("moduleName", IsRequired = true, IsKey = true)]
        public string ModuleName
        {
            get { return (string)base["moduleName"]; }
            set { base["moduleName"] = value; }
        }
    }
}

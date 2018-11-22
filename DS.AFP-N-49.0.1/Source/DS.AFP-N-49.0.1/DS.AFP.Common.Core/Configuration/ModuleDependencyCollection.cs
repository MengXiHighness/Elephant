using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace DS.AFP.Common.Core.ConfigurationNameSpace
{
    /// <summary>
    /// 插件依赖集合类(ModuleDependencyConfigurationElement集合)
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1010:CollectionsShouldImplementGenericInterface")]
    public class ModuleDependencyCollection : ConfigurationElementCollection
    {
        public ModuleDependencyCollection()
        {
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ModuleDependencyCollection(ModuleDependencyConfigurationElement[] dependencies)
        {
            if (dependencies == null)
                throw new ArgumentNullException("dependencies");

            foreach (ModuleDependencyConfigurationElement dependency in dependencies)
            {
                BaseAdd(dependency);
            }
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        
        protected override string ElementName
        {
            get { return "dependency"; }
        }

       
        public ModuleDependencyConfigurationElement this[int index]
        {
            get { return (ModuleDependencyConfigurationElement)base.BaseGet(index); }
        }

        
        protected override ConfigurationElement CreateNewElement()
        {
            return new ModuleDependencyConfigurationElement();
        }

       
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ModuleDependencyConfigurationElement)element).ModuleName;
        }
    }
}

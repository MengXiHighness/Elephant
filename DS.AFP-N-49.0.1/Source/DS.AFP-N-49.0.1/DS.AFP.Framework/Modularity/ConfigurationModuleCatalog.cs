

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DS.AFP.Common.Core;
using DS.AFP.Common.Core.ConfigurationNameSpace;
using DS.AFP.Framework;


namespace DS.AFP.Framework.Modularity
{
    /// <summary>
    /// ÅäÖÃ²å¼þÄ¿Â¼
    /// </summary>
    public class ConfigurationModuleCatalog : ModuleCatalog
    {
        public ConfigurationModuleCatalog()
        {
            this.Store = new ConfigurationStore();
        }

        public IConfigurationStore Store { get; set; }

        protected override void InnerLoad()
        {
            if (this.Store == null)
            {
                throw new InvalidOperationException(Resources.ConfigurationStoreCannotBeNull);
            }

            this.EnsureModulesDiscovered();
        }

       

        private void EnsureModulesDiscovered()
        {
            IDsConfigurationSection section = this.Store.RetrieveModuleConfigurationSection();

            if (section != null)
            {
                foreach (ModuleConfigurationElement element in section.Modules)
                {
                    IList<string> dependencies = new List<string>();

                    if (element.Dependencies.Count > 0)
                    {
                        foreach (ModuleDependencyConfigurationElement dependency in element.Dependencies)
                        {
                            dependencies.Add(dependency.ModuleName);
                        }
                    }
                    ModuleInfo moduleInfo = new ModuleInfo(element.ModuleName, element.ModuleType,element.Index)
                    {
                        Ref = PathHelper.GetFileAbsoluteUri(element.AssemblyFile),
                        InitializationMode = element.StartupLoaded ? InitializationMode.WhenAvailable : InitializationMode.OnDemand
                    };
                    moduleInfo.DependsOn.AddRange(dependencies.ToArray());
                    AddModule(moduleInfo);
                }
            }
        }
    }
}

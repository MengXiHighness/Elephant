
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using DS.AFP.Common.Core.ConfigurationNameSpace;
using DS.AFP.Common.Core;

namespace DS.AFP.Framework.Modularity
{

    /// <summary>
    /// 构件从配置文件的Modules中获取程序集以及其同级目录下的所有程序集
    /// </summary>
    public class FileDirectoryModuleCatalog : ModuleCatalog
    {
        private ILoggerFacade logger;
        public FileDirectoryModuleCatalog(ILoggerFacade logger)
        {
            this.Store = new ConfigurationStore();
            this.logger = logger;
        }

        public IConfigurationStore Store { get; set; }

        protected override void InnerLoad()
        {
            if (this.Store == null)
            {
                throw new InvalidOperationException(Resources.ConfigurationStoreCannotBeNull);
            }
            this.LoadAssemblybyConfigAndDir();
        }
       

        private void LoadAssemblybyConfigAndDir()
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
                    //防止重复模块加载，判断依据ModuleName是否一致
                    if (this.Modules.SingleOrDefault(m => m.ModuleName == element.ModuleName) != null)
                        continue;
                    string assemblyRef = PathHelper.GetFileAbsoluteUri(element.AssemblyFile);
                    //string SplashNameSpace = assemblyRef.Substring(assemblyRef.LastIndexOf('/') + 1).Replace(".dll", "");
                    //string SplashType = "{0}.{1}".FormatString(SplashNameSpace, "SplashPage");
                    //string assemblyPath = assemblyRef.Substring(0, assemblyRef.LastIndexOf('/') + 1);
                    ModuleInfo moduleInfo = new ModuleInfo(element.ModuleName, element.ModuleType, element.Index)
                    {
                        Ref = assemblyRef,
                        IsModuleEntrance = true,
                        InitializationMode = element.StartupLoaded ? InitializationMode.WhenAvailable : InitializationMode.OnDemand
                        //SplashPageUri = assemblyPath + "SplashPage.xaml"
                    };
                    string path = moduleInfo.Ref.Substring(PathHelper.RefFilePrefix.Length + 1);

                    if (!File.Exists(path))
                    {
                        string message = "{0}插件，文件不存在。路径：{1}".FormatString(moduleInfo.ModuleName, moduleInfo.Ref);
                        FileNotFoundException fe = new FileNotFoundException(message);
                        this.logger.Error(message,fe);
                        throw fe;
                    }
                    //try
                    //{
                    //    Assembly ea = Assembly.LoadFrom(moduleInfo.Ref);
                    //    //ResourceManager rm = new ResourceManager(ea.GetModules()[0].Name.Replace("dll", "") + "Resources.ModuleResource", ea);
                    //    //rm.IgnoreCase = true;                        
                    //    //if (rm.GetObject("ModuleName") != null)
                    //    //    moduleInfo.ModuleName = rm.GetString("ModuleName");
                    //    //if (rm.GetObject("ModuleTitle") != null)
                    //    //    moduleInfo.ModuleTitle = rm.GetString("ModuleTitle");
                    //    //if (rm.GetObject("ModuleDescription") != null)
                    //    //    moduleInfo.ModuleDescription = rm.GetString("ModuleDescription");
                    //}
                    //catch(Exception e)
                    //{
                    //    this.logger.Error("程序集加载异常。程序集路径：{0}".FormatString(moduleInfo.Ref),e);
                    //    throw e;
                    //}
                    moduleInfo.DependsOn.AddRange(dependencies.ToArray());
                    AddModule(moduleInfo);
                    //string assfile = Path.GetFullPath(element.AssemblyFile);
                    string assfile = PathHelper.GetFullPath(element.AssemblyFile);
                    DirectoryInfo dir = new DirectoryInfo(assfile.Replace(Path.GetFileName(assfile),string.Empty));
                    if (dir.Exists)
                    {
                        foreach (var f in dir.GetFiles("*.dll", SearchOption.TopDirectoryOnly))
                        {
                            ModuleInfo moduleInfo2 = new ModuleInfo(f.Name, "", element.Index)
                            {
                                Ref = PathHelper.GetFileAbsoluteUri(f.FullName,true),
                                IsModuleEntrance = false,
                                InitializationMode = InitializationMode.WhenAvailable
                            };
                            if (this.Modules.SingleOrDefault(m => m.ModuleName == moduleInfo2.ModuleName ) == null)
                                AddModule(moduleInfo2);
                        }
                    }
                }
            }
        }
    }


       
}

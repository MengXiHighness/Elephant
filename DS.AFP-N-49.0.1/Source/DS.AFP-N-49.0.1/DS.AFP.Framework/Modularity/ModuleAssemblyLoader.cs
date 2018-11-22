using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using DS.AFP.Common.Core;

namespace DS.AFP.Framework.Modularity
{
    //仅仅根据策略对程序集加载
    public class ModuleAssemblyLoader
    {
        private readonly IModuleCatalog moduleCatalog;
        private IEnumerable<IModuleTypeLoader> typeLoaders;
        private readonly ILoggerFacade loggerFacade;
        private HashSet<IModuleTypeLoader> subscribedToModuleTypeLoaders = new HashSet<IModuleTypeLoader>();

        public ModuleAssemblyLoader(IModuleCatalog moduleCatalog, ILoggerFacade logger)
        {
            this.moduleCatalog = moduleCatalog;
            this.loggerFacade = logger;
        }

        /// <summary>
        /// 加载程序集
        /// </summary>
        public void LoadAssembly()
        {
            moduleCatalog.Initialize();
            LoadAssemblyWhenAvailable();
        }

        private void LoadAssemblyWhenAvailable()
        {
            IEnumerable<ModuleInfo> whenAvailableModules = this.moduleCatalog.Modules.Where(m => m.InitializationMode == InitializationMode.WhenAvailable);
            IEnumerable<ModuleInfo> modulesToLoadTypes = this.moduleCatalog.CompleteListWithDependencies(whenAvailableModules);
            if (modulesToLoadTypes != null)
            {
                this.LoadModuleTypes(modulesToLoadTypes);
            }
        }
        //根据moduleInfo的状态信息来加载程序集
        private void LoadModuleTypes(IEnumerable<ModuleInfo> moduleInfos)
        {
            if (moduleInfos == null)
            {
                return;
            }
            //这里可能有bug，主要是类型所在程序集是否先被加载
            foreach (ModuleInfo moduleInfo in moduleInfos)
            {
                if (moduleInfo.State == ModuleState.NotStarted)
                {
                    if (this.ModuleNeedsRetrieval(moduleInfo))
                    {
                        this.BeginRetrievingModule(moduleInfo);
                    }
                    else
                    {
                        moduleInfo.State = ModuleState.ReadyForInitialization;
                    }
                }
            }
        }


        private bool AreDependenciesLoaded(ModuleInfo moduleInfo)
        {
            IEnumerable<ModuleInfo> requiredModules = this.moduleCatalog.GetDependentModules(moduleInfo);
            if (requiredModules == null)
            {
                return true;
            }

            int notReadyRequiredModuleCount =
                requiredModules.Count(requiredModule => requiredModule.State != ModuleState.Initialized);

            return notReadyRequiredModuleCount == 0;
        }

        private void BeginRetrievingModule(ModuleInfo moduleInfo)
        {
            ModuleInfo moduleInfoToLoadType = moduleInfo;
            IModuleTypeLoader moduleTypeLoader = this.GetTypeLoaderForModule(moduleInfoToLoadType);
            moduleInfoToLoadType.State = ModuleState.LoadingTypes;

            if (!this.subscribedToModuleTypeLoaders.Contains(moduleTypeLoader))
            {
                this.subscribedToModuleTypeLoaders.Add(moduleTypeLoader);
            }

            moduleTypeLoader.LoadModuleType(moduleInfo);
        }

        private IModuleTypeLoader GetTypeLoaderForModule(ModuleInfo moduleInfo)
        {
            foreach (IModuleTypeLoader typeLoader in this.ModuleTypeLoaders)
            {
                if (typeLoader.CanLoadModuleType(moduleInfo))
                {
                    return typeLoader;
                }
            }
            throw new ModuleTypeLoaderNotFoundException(moduleInfo.ModuleName, String.Format(CultureInfo.CurrentCulture, Resources.NoRetrieverCanRetrieveModule, moduleInfo.ModuleName), null);
        }

        public virtual IEnumerable<IModuleTypeLoader> ModuleTypeLoaders
        {
            get
            {
                if (this.typeLoaders == null)
                {
                    this.typeLoaders = new List<IModuleTypeLoader>
                                          {
                                              new FileModuleTypeLoader()
                                          };
                }
                return this.typeLoaders;
            }

            set
            {
                this.typeLoaders = value;
            }
        }

        protected virtual bool ModuleNeedsRetrieval(ModuleInfo moduleInfo)
        {
            if (moduleInfo == null) throw new ArgumentNullException("moduleInfo");

            if (moduleInfo.State == ModuleState.NotStarted)
            {
                bool isAvailable = Type.GetType(moduleInfo.ModuleType) != null;
                if (isAvailable)
                {
                    moduleInfo.State = ModuleState.ReadyForInitialization;
                }
                return !isAvailable;
            }

            return false;
        }

    }
}

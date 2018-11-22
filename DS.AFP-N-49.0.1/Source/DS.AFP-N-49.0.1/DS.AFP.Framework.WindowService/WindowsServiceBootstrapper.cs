using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.AFP.Common.Core;
using DS.AFP.Common.Core.Loader;
using DS.AFP.Framework.Events;
using DS.AFP.Framework.Modularity;
using DS.AFP.Framework.Spring;

namespace DS.AFP.Framework.WindowService
{
    /// <summary>
    /// WindowService系统入口
    /// </summary>
    public class WindowServiceBootstrapper : SpringBootstrapper
    {
        private IEventAggregator EventAggregator
        {
            get { return Container.GetObject("IEventAggregator") as IEventAggregator; }
        }

        protected override ILoggerFacade CreateLogger()
        {
            return new LoggerFacade() ;
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new FileDirectoryModuleCatalog(this.Logger);
        }

        protected override void ConfigureEnvironment()
        {
            this.Environment = new DsEnvironment()
            {
                EnvironmentType = EnvironmentType.WindowsService,
                HostName = EnvironmentType.WindowsService.ToDescription()
            };
        }

        protected virtual void ExectueLoading()
        {
            ILoadingManage loading = Container.GetObject("ILoadingManage") as ILoadingManage;
            if (loading != null)
                loading.Loaded();
        }

        /// <summary>
        /// 入口函数
        /// </summary>
        /// <param name="runWithDefaultConfiguration">是否采用默认配置</param>
        public override void Run(bool runWithDefaultConfiguration)
        {
            this.useDefaultConfiguration = runWithDefaultConfiguration;
            ConfigureEnvironment();
            this.Logger = this.CreateLogger();
            if (this.Logger == null)
            {
                throw new InvalidOperationException(Resources.NullLoggerFacadeException);
            }
            DsExceptionHandler.Logger = this.Logger;
            this.Logger.Info("----------------------------------------------------------------------------");
            this.Logger.Info("Platform began to load and initialize each plug-in");
            this.Logger.Debug(Resources.LoggerCreatedSuccessfully);
            this.Logger.Debug(Resources.CreatingModuleCatalogBegin);
            this.ModuleCatalog = this.CreateModuleCatalog();
            if (this.ModuleCatalog == null)
            {
                throw new InvalidOperationException(Resources.NullModuleCatalogException);
            }
            this.Logger.Debug(Resources.CreatingModuleCatalogEnd);

            this.Logger.Debug(Resources.ConfiguringModuleCatalogBegin);
            this.ConfigureModuleCatalog();
            this.Logger.Debug(Resources.ConfiguringModuleCatalogEnd);

            this.Logger.Debug(Resources.PreInitializeModulesBegin);
            this.PreInitializeModules();
            this.Logger.Debug(Resources.PreInitializeModulesEnd);

            this.Logger.Debug(Resources.CreatingSpringContainerBegin);
            this.Container = this.CreateContainer();
            if (this.Container == null)
            {
                throw new InvalidOperationException(Resources.NullUnityContainerException);
            }
            this.Logger.Debug(Resources.CreatingSpringContainerEnd);

            this.Logger.Debug(Resources.ConfiguringSpringContainerBegin);
            this.ConfigureContainer();
            this.Logger.Debug(Resources.ConfiguringSpringContainerEnd);

            this.Logger.Debug(Resources.ConfiguringServiceLocatorSingletonBegin);
            this.ConfigureServiceLocator();
            this.Logger.Debug(Resources.ConfiguringServiceLocatorSingletonEnd);

            this.Logger.Debug(Resources.RegisteringFrameworkExceptionTypesBegin);
            this.RegisterFrameworkExceptionTypes();
            this.Logger.Debug(Resources.RegisteringFrameworkExceptionTypesEnd);

            if (this.Container.ContainsObjectDefinition("IModuleManager"))
            {
                this.Logger.Debug(Resources.InitializingModulesBegin);
                this.InitializeModules();
                this.Logger.Debug(Resources.InitializingModulesEnd);
            }

            ExectueLoading();

            this.Logger.Debug(Resources.BootstrapperRunCompleted);
            this.Logger.Info("Platform loaded");
        }

        protected override void ConfigureContainer()
        {
            if (useDefaultConfiguration)
            {
              
                this.Container.RegisterTypeIfMissing<LoadingManage>("ILoadingManage", true);
                this.Container.RegisterTypeIfMissing<DsEnvironment>("IDsEnvironment", true);

            }
            base.ConfigureContainer();
        }

        protected override void InitializeModules()
        {
            //配置信息注入到容器中
            base.InitializeModules();
        }

    }
}

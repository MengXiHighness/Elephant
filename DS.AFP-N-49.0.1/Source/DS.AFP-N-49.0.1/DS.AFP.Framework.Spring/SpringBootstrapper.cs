using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring.Context;
using Microsoft.Practices.ServiceLocation;
using Spring.Context.Support;
using Spring.Objects.Factory.Support;
using DS.AFP.Framework.Modularity;
using DS.AFP.Common.Core;
using DS.AFP.Common.Core.ConfigurationNameSpace;
using DS.AFP.Framework.Events;
using DS.AFP.Framework.Message;
using System.IO;

namespace DS.AFP.Framework.Spring
{
    /// <summary>
    /// Spring框架入口
    /// </summary>
    public abstract class SpringBootstrapper : Bootstrapper
    {
        protected bool useDefaultConfiguration = true;
       
        /// <summary>
        /// IOC容器
        /// </summary>
        [CLSCompliant(false)]
        public IApplicationContext Container { get; protected set; }

        private void SetupAppDomain()
        {
            AppDomainSetup setup = new AppDomainSetup();
            setup.ApplicationName = "ApplicationLoader";
            setup.ApplicationBase = AppDomain.CurrentDomain.BaseDirectory;
            setup.PrivateBinPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "private");
            setup.CachePath = setup.ApplicationBase;
            setup.ShadowCopyFiles = "true";
            setup.ShadowCopyDirectories = setup.ApplicationBase;
            AppDomain.CurrentDomain.SetShadowCopyFiles();
            //setup.CachePath = @"E:\WorkSpace\程序影像\AssemblyApp\AssemblyApp\bin\Debug\Cache";
        }

        /// <summary>
        /// 模块加载前期准备
        /// </summary>
        protected override void PreInitializeModules()
        {
            //SetupAppDomain();
            ModuleAssemblyLoader ml = new ModuleAssemblyLoader(this.ModuleCatalog, this.Logger);
            ml.LoadAssembly();
            //base.PreInitializeModules();
        }

        /// <summary>
        /// 入口函数
        /// </summary>
        /// <param name="runWithDefaultConfiguration"></param>
        public override void Run(bool runWithDefaultConfiguration)
        {
            this.useDefaultConfiguration = runWithDefaultConfiguration;

            this.Logger = this.CreateLogger();
            if (this.Logger == null)
            {
                throw new InvalidOperationException(Resources.NullLoggerFacadeException);
            }
            this.Logger.Debug(Resources.LoggerCreatedSuccessfully);
            DsExceptionHandler.Logger = this.Logger;
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
            this.Logger.Info(Resources.BootstrapperRunCompleted);
        }

        protected override void ConfigureServiceLocator()
        {
            ServiceLocator.SetLocatorProvider(() => this.Container.GetObject("IServiceLocator") as IServiceLocator);
        }

        /// <summary>
        /// IOC容器的基本对象的注册
        /// </summary>
        protected virtual void ConfigureContainer()
        {
            this.Logger.Debug(Resources.ConfigureContainerBegin);
            //日志
            this.Container.RegisterInstance<ILoggerFacade>(this.Logger);
            //模块配置项
            this.Container.RegisterInstance<IModuleCatalog>(this.ModuleCatalog);
            //Spring容器
            this.Container.RegisterInstance<IServiceLocator>(new SpringServiceLocatorAdapter(this.Container));
           
            if (useDefaultConfiguration)
            {

                #region 框架内部
                this.Container.RegisterTypeIfMissing<ModuleInitializer>("IModuleInitializer",true);
                this.Container.RegisterTypeIfMissing<ModuleManager>("IModuleManager",  true);
             
                this.Container.RegisterTypeIfMissing<EventAggregator>("IEventAggregator",  true);
                this.Container.RegisterTypeIfMissing<DS21Message>("IDS21Message", true);
                this.Container.RegisterTypeIfMissing<IDsEnvironment>("IDsEnvironment", true);
                Container.RegisterInstance<IDsConfigurationSection>(new ConfigurationStore().RetrieveModuleConfigurationSection());

                #endregion
            }
            this.Logger.Debug(Resources.ConfigureContainerEnd);
        }

        /// <summary>
        /// 各个插件的类型加载，以及IOC进行注册
        /// </summary>
        protected override void InitializeModules()
        {
            IModuleManager manager;
            try
            {
                manager = this.Container.GetObject("IModuleManager") as IModuleManager;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("IModuleCatalog"))
                {
                    throw new InvalidOperationException(Resources.NullModuleCatalogException);
                }

                throw;
            }
            manager.Run();
        }

        /// <summary>
        /// 容器初始化，需要捕获相应异常
        /// </summary>
        /// <returns></returns>
        [CLSCompliant(false)]
        protected virtual IApplicationContext CreateContainer()
        {
            IApplicationContext container = null;
            try
            {
                container = ContextRegistry.GetContext();
                GlobalObject.Container = container;
            }
            catch (Exception e)
            {
                throw new ContainerInitializeException(Resources.ContainerInitializeException, e);
                this.Logger.Error("IApplicationContext Create a container abnormal", e);

            }
            return container;
        }

       
    }
}

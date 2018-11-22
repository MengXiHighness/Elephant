using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using DS.AFP.Common.Core;
using DS.AFP.Common.Core.Loader;
using DS.AFP.Framework.Events;
using DS.AFP.Framework.Modularity;
using DS.AFP.Framework.Spring;
using Spring.Context.Support;
using Spring.Web.Mvc;

namespace DS.AFP.Framework.Web
{
    /// <summary>
    /// Web系统入口
    /// </summary>
    public class WebBootstrapper : SpringBootstrapper
    {
        private static bool isInit = false;
        protected override global::Spring.Context.IApplicationContext CreateContainer()
        {
            var resolver = BuildDependencyResolver();
            RegisterDependencyResolver(resolver);

            return ((SpringMvcDependencyResolver)resolver).ApplicationContext;
        }

        protected virtual IDependencyResolver BuildDependencyResolver()
        {
            return new SpringMvcDependencyResolver(ContextRegistry.GetContext());
        }

        private void RegisterDependencyResolver(IDependencyResolver resolver)
        {
            ThreadSafeDependencyResolverRegistrar.Register(resolver);
        }

        protected class ThreadSafeDependencyResolverRegistrar
        {
            private static bool _isInitialized = false;
            private static readonly Object @lock = new Object();

            public static void Register(IDependencyResolver resolver)
            {
                if (_isInitialized)
                {
                    return;
                }

                lock (@lock)
                {
                    if (_isInitialized)
                    {
                        return;
                    }

                    DependencyResolver.SetResolver(resolver);

                    _isInitialized = true;
                }
            }
        }
        private IEventAggregator EventAggregator
        {
            get { return Container.GetObject("IEventAggregator") as IEventAggregator; }
        }

        protected override ILoggerFacade CreateLogger()
        {
            return new LoggerFacade();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new FileDirectoryModuleCatalog(this.Logger);
        }

        protected override void ConfigureEnvironment()
        {
            this.Environment = new DsEnvironment()
            {
                EnvironmentType = EnvironmentType.WebMvc
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
            if (isInit)
                return;
            isInit = true;
            this.useDefaultConfiguration = runWithDefaultConfiguration;
            ConfigureEnvironment();
            this.Logger = this.CreateLogger();
            if (this.Logger == null)
            {
                throw new InvalidOperationException(Resources.NullLoggerFacadeException);
            }
            this.Logger.Info(Resources.LoggerCreatedSuccessfully);
            this.Logger.Info(Resources.CreatingModuleCatalogBegin);
            this.ModuleCatalog = this.CreateModuleCatalog();
            if (this.ModuleCatalog == null)
            {
                throw new InvalidOperationException(Resources.NullModuleCatalogException);
            }
            this.Logger.Info(Resources.CreatingModuleCatalogEnd);

            this.Logger.Info(Resources.ConfiguringModuleCatalogBegin);
            this.ConfigureModuleCatalog();
            this.Logger.Info(Resources.ConfiguringModuleCatalogEnd);

            this.Logger.Info(Resources.PreInitializeModulesBegin);
            this.PreInitializeModules();
            this.Logger.Info(Resources.PreInitializeModulesEnd);

            this.Logger.Info(Resources.CreatingSpringContainerBegin);
            this.Container = this.CreateContainer();
            if (this.Container == null)
            {
                throw new InvalidOperationException(Resources.NullUnityContainerException);
            }
            this.Logger.Info(Resources.CreatingSpringContainerEnd);

            this.Logger.Info(Resources.ConfiguringSpringContainerBegin);
            this.ConfigureContainer();
            this.Logger.Info(Resources.ConfiguringSpringContainerEnd);

            this.Logger.Info(Resources.ConfiguringServiceLocatorSingletonBegin);
            this.ConfigureServiceLocator();
            this.Logger.Info(Resources.ConfiguringServiceLocatorSingletonEnd);

            this.Logger.Info(Resources.RegisteringFrameworkExceptionTypesBegin);
            this.RegisterFrameworkExceptionTypes();
            this.Logger.Info(Resources.RegisteringFrameworkExceptionTypesEnd);

            if (this.Container.ContainsObjectDefinition("IModuleManager"))
            {
                this.Logger.Info(Resources.InitializingModulesBegin);
                this.InitializeModules();
                this.Logger.Info(Resources.InitializingModulesEnd);
            }

            ExectueLoading();
            
            this.Logger.Info(Resources.BootstrapperRunCompleted);

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

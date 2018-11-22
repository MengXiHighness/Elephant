
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using DS.AFP.Common.Core;
using DS.AFP.Common.Core.Loader;
using DS.AFP.Framework;
using DS.AFP.Framework.Events;
using DS.AFP.Framework.Modularity;
using DS.AFP.Framework.Regions;
using DS.AFP.Framework.Regions.Behaviors;
using DS.AFP.Framework.Spring;
using DS.AFP.Framework.WPF.Portal;
using DS.AFP.Framework.WPF.Browser;


namespace DS.AFP.Framework.WPF
{
    /// <summary>
    /// WPF框架入口
    /// </summary>
    public class WPFBootstrapper : SpringBootstrapper
    {

        protected DependencyObject Shell { get; set; }

        private IEventAggregator EventAggregator
        {
            get { return Container.GetObject("IEventAggregator") as IEventAggregator; }
        }
        
        protected virtual DependencyObject CreateShell()
        {
            return Container.GetObject("IShell") as DependencyObject;
        }

        protected virtual void InitializeShell()
        {
            //#region 初始化浏览器
            
            //try
            //{
            //    //初始化浏览器
            //    Xpcom.Initialize(XULRunnerLocator.GetXULRunnerLocation("Libs/xulrunner"));
            //    //欺骗服务器
            //    GeckoPreferences.User["intl.User_Agent"] = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/38.0.2125.104 Safari/537.36";
            //    //默认是中文
            //    GeckoPreferences.User["intl.accept_languages"] = "zh-cn";
            //}
            //catch (Exception ex)
            //{
            //    Logger.Error("xulrunner init error",ex);
            //}
            //#endregion
        }

        /// <summary>
        /// 结束Splash页面
        /// 执行初始化加载
        /// </summary>
        protected virtual void SplashFinished()
        {
            if (Shell != null)
            {
                IShell shell = Shell as IShell;
                if (shell != null)
                    shell.Active();
            }
        }

        protected virtual void ExectueLoading()
        {
            ILoadingManage loading = Container.GetObject("ILoadingManage") as ILoadingManage;
            if (loading != null)
                loading.Loaded();
        }

        protected override ILoggerFacade CreateLogger()
        {
            return new LoggerFacade(this.GetType());
        }
      

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new FileDirectoryModuleCatalog(this.Logger);
          
        }

        protected override void ConfigureEnvironment()
        {
            this.Environment = new DsEnvironment()
            {
                EnvironmentType = EnvironmentType.WPF,
                HostName = EnvironmentType.WPF.ToDescription()
            };
        }

        /// <summary>
        /// 入口函数
        /// </summary>
        /// <param name="runWithDefaultConfiguration"></param>
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
            //this.Logger.Info("----------------------------------------------------------------------------");
            this.Logger.Info("Platform began to load and initialize each plug-in ");
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

            this.Logger.Debug(Resources.ConfigureRegionAdapterMappingsBegin);
            this.ConfigureRegionAdapterMappings();
            this.Logger.Debug(Resources.ConfigureRegionAdapterMappingsEnd);

            this.Logger.Debug(Resources.ConfigureDefaultRegionBehaviorsBegin);
            this.ConfigureDefaultRegionBehaviors();
            this.Logger.Debug(Resources.ConfigureDefaultRegionBehaviorsEnd);

            this.Logger.Debug(Resources.RegisteringFrameworkExceptionTypesBegin);
            this.RegisterFrameworkExceptionTypes();
            this.Logger.Debug(Resources.RegisteringFrameworkExceptionTypesEnd);

            this.Shell = this.CreateShell();
            if (this.Shell != null)
            {
                RegionManager.SetRegionManager(this.Shell, this.Container.GetObject("IRegionManager") as IRegionManager);
                RegionManager.UpdateRegions();
                this.InitializeShell();
            }

            if (this.Container.ContainsObjectDefinition("IModuleManager"))
            {
                this.Logger.Debug(Resources.InitializingModulesBegin);
                this.InitializeModules();
                this.Logger.Debug(Resources.InitializingModulesEnd);
            }

            ExectueLoading();

            SplashFinished();
            this.Logger.Debug(Resources.BootstrapperRunCompleted);
            this.Logger.Info("Platform loaded");

        }

        protected override void ConfigureContainer()
        {
            if (useDefaultConfiguration)
            {
                //注册浏览器管理对象(单例)
                //this.Container.RegisterTypeIfMissing<WebBrowserManager>("WebBrowserManager", true);

                this.Container.RegisterTypeIfMissing<RegionAdapterMappings>("RegionAdapterMappings", true);
                this.Container.RegisterTypeIfMissing<RegionManager>("IRegionManager", true);
                this.Container.RegisterTypeIfMissing<RegionViewRegistry>("IRegionViewRegistry", true);
                this.Container.RegisterTypeIfMissing<RegionBehaviorFactory>("IRegionBehaviorFactory", true);
                this.Container.RegisterTypeIfMissing<RegionNavigationJournalEntry>("IRegionNavigationJournalEntry", false);
                this.Container.RegisterTypeIfMissing<RegionNavigationJournal>("IRegionNavigationJournal", false);
                this.Container.RegisterTypeIfMissing<RegionNavigationService>("IRegionNavigationService", false);
                this.Container.RegisterTypeIfMissing<RegionNavigationContentLoader>("IRegionNavigationContentLoader", true);

                this.Container.RegisterTypeIfMissing<SelectorRegionAdapter>("SelectorRegionAdapter", true);
                this.Container.RegisterTypeIfMissing<ItemsControlRegionAdapter>("ItemsControlRegionAdapter", true);
                this.Container.RegisterTypeIfMissing<ContentControlRegionAdapter>("ContentControlRegionAdapter", true);
                this.Container.RegisterTypeIfMissing<RegionBehaviorFactory>("IRegionBehaviorFactory", true);
                this.Container.RegisterTypeIfMissing<WindowRegionAdapter>("WindowRegionAdapter", false);

                this.Container.RegisterTypeIfMissing<DelayedRegionCreationBehavior>("DelayedRegionCreationBehavior", false);

                this.Container.RegisterTypeIfMissing<AutoPopulateRegionBehavior>("AutoPopulateRegionBehavior", false);
                this.Container.RegisterTypeIfMissing<BindRegionContextToDependencyObjectBehavior>("BindRegionContextToDependencyObjectBehavior", false);
                this.Container.RegisterTypeIfMissing<RegionActiveAwareBehavior>("RegionActiveAwareBehavior", false);
                this.Container.RegisterTypeIfMissing<SyncRegionContextWithHostBehavior>("SyncRegionContextWithHostBehavior", false);
                this.Container.RegisterTypeIfMissing<RegionManagerRegistrationBehavior>("RegionManagerRegistrationBehavior", false);
                this.Container.RegisterTypeIfMissing<RegionMemberLifetimeBehavior>("RegionMemberLifetimeBehavior", false);
                this.Container.RegisterTypeIfMissing<ClearChildViewsRegionBehavior>("ClearChildViewsRegionBehavior", false);

                this.Container.RegisterTypeIfMissing<LoadingManage>("ILoadingManage", true);
                this.Container.RegisterTypeIfMissing<DsEnvironment>("IDsEnvironment", true);
                this.Container.RegisterTypeIfMissing<MainPortal>("IPortal", true);



                //this.Container.RegisterTypeIfMissing<MessageAggregator>("IMessageAggregator", true);
            }
            base.ConfigureContainer();
        }

        protected virtual RegionAdapterMappings ConfigureRegionAdapterMappings()
        {
            RegionAdapterMappings regionAdapterMappings = Container.GetObject("RegionAdapterMappings") as RegionAdapterMappings;
            if (regionAdapterMappings != null)
            {
                regionAdapterMappings.RegisterMapping(typeof(Selector), Container.GetObject<SelectorRegionAdapter>());
                regionAdapterMappings.RegisterMapping(typeof(ItemsControl), Container.GetObject<ItemsControlRegionAdapter>());
                regionAdapterMappings.RegisterMapping(typeof(ContentControl), Container.GetObject<ContentControlRegionAdapter>());
                WindowRegionAdapter wra = Container.GetObject<WindowRegionAdapter>();
                try
                {
                    var style = Application.Current.FindResource("WindowTemplate");
                    if (style != null)
                        wra.WindowStyle = (Style)style;
                    
                }
                catch { };
                regionAdapterMappings.RegisterMapping(typeof(Window), wra);

            }
            return regionAdapterMappings;
        }

        protected virtual IRegionBehaviorFactory ConfigureDefaultRegionBehaviors()
        {
            var defaultRegionBehaviorTypesDictionary = Container.GetObject<IRegionBehaviorFactory>();

            if (defaultRegionBehaviorTypesDictionary != null)
            {
                defaultRegionBehaviorTypesDictionary.AddIfMissing(AutoPopulateRegionBehavior.BehaviorKey,
                                                                  typeof(AutoPopulateRegionBehavior));

                defaultRegionBehaviorTypesDictionary.AddIfMissing(BindRegionContextToDependencyObjectBehavior.BehaviorKey,
                                                                  typeof(BindRegionContextToDependencyObjectBehavior));

                defaultRegionBehaviorTypesDictionary.AddIfMissing(RegionActiveAwareBehavior.BehaviorKey,
                                                                  typeof(RegionActiveAwareBehavior));

                defaultRegionBehaviorTypesDictionary.AddIfMissing(SyncRegionContextWithHostBehavior.BehaviorKey,
                                                                  typeof(SyncRegionContextWithHostBehavior));

                defaultRegionBehaviorTypesDictionary.AddIfMissing(RegionManagerRegistrationBehavior.BehaviorKey,
                                                                  typeof(RegionManagerRegistrationBehavior));

                defaultRegionBehaviorTypesDictionary.AddIfMissing(RegionMemberLifetimeBehavior.BehaviorKey,
                                                  typeof(RegionMemberLifetimeBehavior));

                defaultRegionBehaviorTypesDictionary.AddIfMissing(ClearChildViewsRegionBehavior.BehaviorKey,
                                                  typeof(ClearChildViewsRegionBehavior));

            }
            return defaultRegionBehaviorTypesDictionary;
        }

        protected override void ConfigureModuleCatalog()
        {
            //FileDirectoryModuleCatalog configurationCatalog = new FileDirectoryModuleCatalog();
            //((AggregateModuleCatalog)ModuleCatalog).AddCatalog(configurationCatalog);
            //ModuleCatalog = new FileDirectoryModuleCatalog();
        }

        void manager_ModuleDownloadProgressChanged(object sender, ModuleDownloadProgressChangedEventArgs e)
        {
        }

        protected override void InitializeModules()
        {
            //配置信息注入到容器中
           base.InitializeModules();
        }

        void manager_LoadModuleCompleted(object sender, LoadModuleCompletedEventArgs e)
        {
            //EventAggregator.GetEvent<MessageUpdateEvent>().Publish(new MessageUpdateEvent { Message = e.ModuleInfo.ModuleName });
            
        }
    }
}

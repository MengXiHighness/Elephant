
namespace DS.AFP.WPF.App
{
    using System;
    using System.Windows;
    using DS.AFP.Common.Core;
    using DS.AFP.Framework.Events;
    using DS.AFP.Framework.Modularity;
    using DS.AFP.Framework.WPF;
    using DS.AFP.Framework;
    using System.Windows.Threading;
    using System.Windows.Controls;
    using System.Threading;
    using DS.AFP.Framework.Regions;
    using DS.AFP.Framework.Regions.Behaviors;
    using DS.AFP.Common.Core.ConfigurationNameSpace;
    using System.Windows.Forms;
    using CefSharp.DSCT.Controls;

    /// <summary>
    /// 系统入口
    /// </summary>
    public class Bootstrapper : WPFBootstrapper
    {
        private ILoggerFacade Logger = null;

        public Bootstrapper(ILoggerFacade logger)
        {
            Logger = logger;
        }

        public static IEventAggregator EventAggregator
        {
            get; set;
        }


        //public IEventAggregator EventAggregator
        //{
        //    get { return Container.GetObject("IEventAggregator") as IEventAggregator; }
        //}

        protected override DependencyObject CreateShell()
        {
            //this.Container.RegisterTypeIfMissing<ThemeViewModel>("ThemeViewModel", true);
            var win = this.Container.GetObject("IShell") as MainWindow;

            #region 设置处于第几屏
            var config = this.Container.GetObject("IDsConfigurationSection") as IDsConfigurationSection;
            if (config != null)
            {
                var ScreenNumberValue = config.Params["ScreenNumber"].Value;
                if (!ScreenNumberValue.IsNullOrEmpty())
                {
                    var ScreenNumber = Convert.ToInt32(ScreenNumberValue);//2
                    if (Screen.AllScreens.Length >= ScreenNumber && ScreenNumber != 1)
                    {
                        Screen s = Screen.AllScreens[(ScreenNumber - 1)];

                        System.Drawing.Rectangle r = s.WorkingArea;
                        win.Top = r.Top;
                        win.Left = r.Left;
                    }
                }
            }
            #endregion
            return win;
        }


        protected override void ConfigureEnvironment()
        {
            this.Environment = new DsEnvironment()
            {
                EnvironmentType = EnvironmentType.WPF,
                HostName = EnvironmentType.WPF.ToDescription()
            };
            //base.ConfigureEnvironment();
        }

        /// <summary>
        ///  初始化Shell
        /// </summary>
        protected override void InitializeShell()
        {
            EventAggregator = Container.GetObject("IEventAggregator") as IEventAggregator;
            //IShell shell = base.Shell as IShell;
            //if (shell != null)
            //{
            //    shell.Show();
            //    //EventAggregator.GetEvent<CloseSplashEvent>().Publish(new CloseSplashEvent());
            //}
            //注册浏览器管理对象(单例)
            this.Container.RegisterTypeIfMissing<WebBrowserManager>("WebBrowserManager", true);
            base.InitializeShell();
        }

        /// <summary>
        /// SplashFinished 控制
        /// </summary>
        protected override void SplashFinished()
        {


            EventAggregator.GetEvent<CloseSplashEvent>().Publish(new CloseSplashEvent());

            IShell shell = base.Shell as IShell;
            if (shell != null)
            {
                shell.SetWindowStyle(new WindowStyleWays() { StyleWays = StyleWays.ShowTaskBar, WindowState = System.Windows.WindowState.Normal });
                shell.Show();
            }


            base.SplashFinished();
        }

        /// <summary>
        /// 创建日志实例
        /// </summary>
        /// <returns></returns>
        protected override ILoggerFacade CreateLogger()
        {
            return Logger;// new LoggerFacade().GetInstance("DS.AFP.WPF.App");
        }

        /// <summary>
        ///容器配置
        /// </summary>
        protected override void ConfigureContainer()
        {
            Container.RegisterTypeIfMissing<MainWindow>("IShell", true);
            base.ConfigureContainer();
        }

        //protected override Framework.Regions.IRegionBehaviorFactory ConfigureDefaultRegionBehaviors()
        //{
        //    IRegionBehaviorFactory rbf = base.ConfigureDefaultRegionBehaviors();
        //    rbf.AddIfMissing(AutoPopulateRegionBehavior.BehaviorKey, typeof(AutoPopulateRegionBehavior));
        //    return rbf;
        //}

        //void manager_ModuleDownloadProgressChanged(object sender, ModuleDownloadProgressChangedEventArgs e)
        //{
        //    //Dispatcher.Invoke(new DS.AFP.WPF.App.App.LoadingDelegate((dp, v) =>
        //    //{
        //    //    LoadingManage.loadingPage.loadingProgress.SetValue(dp, v);
        //    //}), System.Windows.Threading.DispatcherPriority.Background, ProgressBar.ValueProperty, currentValue);

        //}

        protected override void InitializeModules()
        {
            //配置信息注入到容器中
            IModuleManager manager = this.Container.GetObject("IModuleManager") as IModuleManager;
            //manager.ModuleDownloadProgressChanged += new EventHandler<ModuleDownloadProgressChangedEventArgs>(manager_ModuleDownloadProgressChanged);
            manager.LoadModuleCompleted += new EventHandler<LoadModuleCompletedEventArgs>(manager_LoadModuleCompleted);
            base.InitializeModules();
        }
        private const int delayMilliseconds = 2000;
        private DateTime StartDateTime = new DateTime(delayMilliseconds, 1, 1);

        void manager_LoadModuleCompleted(object sender, LoadModuleCompletedEventArgs e)
        {

            string moduleType = e.ModuleInfo.ModuleType;
            string temptype = moduleType.Substring(moduleType.IndexOf(','));
            //DS.AFP.UIFramework.UIFrameworkModule,DS.AFP.UIFramework, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
            string SplashType = e.ModuleInfo.Ref.Substring(e.ModuleInfo.Ref.LastIndexOf('/') + 1).Replace(".dll", "") + ".SplashPage";
            Type t = Type.GetType(SplashType + temptype);
            e.ModuleInfo.SplashType = t;
            LoadModuleEvent lme = new LoadModuleEvent()
            {
                LoadModuleCompletedEventArgs = e

            };
            if (StartDateTime == new DateTime(delayMilliseconds, 1, 1))
                StartDateTime = System.DateTime.Now;
            int sub = System.DateTime.Now.Subtract(StartDateTime).Milliseconds;
            if (sub > delayMilliseconds)
            {
                lme.OffSetTime = 0;
            }
            else
            {
                lme.OffSetTime = delayMilliseconds - sub;
            }
            StartDateTime = System.DateTime.Now;
            EventAggregator.GetEvent<LoadModuleEvent>().Publish(lme);
        }
    }
}

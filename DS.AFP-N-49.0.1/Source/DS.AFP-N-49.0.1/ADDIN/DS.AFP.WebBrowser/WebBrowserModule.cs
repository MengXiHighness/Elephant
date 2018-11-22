using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using DS.AFP.Common.Core;
using DS.AFP.Common.Core.ConfigurationNameSpace;
using DS.AFP.Common.Core.Loader;
using DS.AFP.Framework.Modularity;
using DS.AFP.Framework.Regions;
using DS.AFP.Framework.WPF;
using Spring.Context;
using DS.AFP.Framework.Events;
using System.Threading;
using System.Windows.Threading;
using System.Windows.Controls;
using DS.AFP.Framework;
using DS.AFP.Framework.WPF.Portal;
using CefSharp.Wpf;
using CefSharp.DSCT;

namespace DS.AFP.WebBrowser
{
    public class WebBrowserModule : ModuleBase
    {
        public static IRegionManager UIRegionManager
        {
            get;
            private set;
        }

        public static IApplicationContext Container
        {
            get;
            private set;
        }

        private AutoResetEvent WaitForSign { get; set; }

        private Thread InnerThread { get; set; }

        private IShell MainWindow
        {
            get;
            set;
        }

        public static IDsConfigurationSection DsConfiguration
        {
            get;
            private set;
        }

        public static IPortalSection PortalSection
        {
            get;
            private set;
        }

        public static IDsAddinConfigurationSection DsAddinConfig
        {
            get;
            private set;
        }

        public static ILoggerFacade Logger
        {
            get;
            private set;
        }

        public static string AddinPath
        {
            get;
            private set;
        }

        public IDsEnvironment DsEnvironment
        {
            get;
            private set;
        }

        public static IEventAggregator EventAggregator { get; private set; }



        public static IPortal portaldata { get; set; }

        public WebBrowserModule(IApplicationContext container, IPortal portal, IDsConfigurationSection dsconfig, IShell shell, ILoadingManage loadingManage, IDsEnvironment dsEnvironment, IRegionManager regionManager
            , IEventAggregator eventAggregator, ILoggerFacade logger)
            : base(container, dsconfig)
        {
            AddinPath = base.AddinInfo.AddinPath.Substring(0, base.AddinInfo.AddinPath.LastIndexOf('\\'));
            EventAggregator = eventAggregator;
            Container = base.Container;
            UIRegionManager = regionManager;
            MainWindow = shell;
            portaldata = portal;
            DsConfiguration = dsconfig;
            Logger = logger;
            DsEnvironment = dsEnvironment;
            DsAddinConfig = base.CurrentAddinConfiguration.GetSection("ds/base") as IDsAddinConfigurationSection;

            string Language = string.Empty;
            var LanguagePar = DsConfiguration.Params["Language"];
            if (LanguagePar != null)
            {
                Language = LanguagePar.Value;
            }

           // CefManager.Strat();


            string dxcx = (new DS.AFP.Common.Core.ResManager()).GetResource("DS.AFP.WebBrowser", "Close").ToString();
        }

        public override void Initialize()
        {
            //加载主界面
            UIRegionManager.Register(RegionNames.WindowAreaRoot, typeof(Browser), this);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using DS.AFP.Common.Core;
using DS.AFP.Common.Core.ConfigurationNameSpace;
using DS.AFP.Common.Core.Dynamic;
using DS.AFP.Communication.SocketNameSpace;
using Application = System.Windows.Application;
using System.Windows.Forms;
using System.Windows;
using DS.AFP.WPF.App.Utility;
using System.Windows.Threading;
using DS.AFP.Framework.Events;
using System.IO;
using System.Net;
using DS.AFP.Framework.WPF;
using DS.AFP.Framework.WPF.Browser.EventSignal;
using CefSharp.DSCT;
using CefSharp;
using CefSharp.Wpf;
using CefSharp.DSCT.Handlers;

namespace DS.AFP.WPF.App
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        //public delegate void LoadingDelegate(DependencyProperty dp,double v);


        private static NotifyIcon trayIcon;
        public static event Action<MouseEventArgs> iconMouseEvent;
        public static AlertManager alertManager = null;
        public static IEventAggregator EventAggregator;
        public static ILoggerFacade logger = null;
        public static Bootstrapper bootstrapper;

        protected override void OnStartup(StartupEventArgs e)
        {
            

            string cachePath = AppDomain.CurrentDomain.BaseDirectory + "cache";
            if (Directory.Exists(cachePath))
            {
                Directory.Delete(cachePath, true);
            }
            cachePath = AppDomain.CurrentDomain.BaseDirectory + "cookies";
            if (Directory.Exists(cachePath))
            {
                Directory.Delete(cachePath, true);
            }


            CefManager.Strat();

            logger = new LoggerFacade().GetInstance("DS.AFP.WPF.App");
            logger.Debug("WPF宿主容器开始启动");
            bootstrapper = new Bootstrapper(logger);
            bootstrapper.Run();
            EventAggregator = Bootstrapper.EventAggregator;
            logger.Debug("WPF宿主容器启动完成，运行正常!");


            EventAggregator.GetEvent<BrowserExitEventSignal>().Subscribe(o =>
            {
                App.Current.Shutdown();

            }, ThreadOption.UIThread, true);

            AddTrayIcon();
        }

        /// <summary>
        /// 退出系统
        /// </summary>
        public void LogOut()
        {
            try
            {
                var clientDataStr = (Framework.Spring.GlobalObject.Container.GetObject("IDsEnvironment") as AFP.Common.Core.IDsEnvironment).ShareData["ClientData"] as string;
                if (!string.IsNullOrEmpty(clientDataStr))
                {
                    var ClientData = clientDataStr.DeserializeFromJson<ClientData>();
                    if (ClientData != null)
                    {
                        var BrowserLogOutInfo = new BrowserLogOutInfo
                        {
                            Uri = ClientData.configure.logOutUri,
                            CallBack = () =>
                            {
                                //Application.Current.Dispatcher.Invoke(new Action(() =>
                                //{
                                //    App.Current.Shutdown();
                                //}));
                            }
                        };
                        EventAggregator.GetEvent<BrowserLogOutEventSignal>().Publish(BrowserLogOutInfo);
                        //using (WebClient wc = new WebClient())
                        //{
                        //    wc.DownloadString(ClientData.configure.logOutUri);
                        logger.Info("browser logout");
                        //}
                    }
                    else
                    {
                        App.Current.Shutdown();
                    }
                }
                else
                {
                    App.Current.Shutdown();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                App.Current.Shutdown();
            }
        }



        Window MainWindow = null;
        string[] windows = { "DS.AFP.WPF.DSCT.MainWindow", "DS.AFP.WPF.App.MainWindow" };
        public Window GetMainWindow()
        {
            if (MainWindow == null)
            {
                foreach (Window w in Application.Current.Windows)
                {
                    if (windows.Contains(w.GetType().FullName))
                    {
                        MainWindow = w;
                        break;
                    }
                }
            }
            return MainWindow;
        }



        private void AddTrayIcon()
        {
            if (trayIcon != null)
            {
                return;
            }

            trayIcon = new NotifyIcon
            {
                Icon = DS.AFP.WPF.App.Properties.Resources.Coward,
                Text = "DS.AFP.WPF.App"
            };
            trayIcon.Visible = true;
            trayIcon.Click += (s, e) =>
            {
                var mea = e as System.Windows.Forms.MouseEventArgs;
                if(iconMouseEvent!=null)
                    iconMouseEvent.Invoke(mea);
            };


            ContextMenu menu = new ContextMenu();

            MenuItem closeItem = new MenuItem();
            closeItem.Text = "Close";
            closeItem.Click += new EventHandler(delegate
            {
                LogOut();
                //App.Current.Shutdown();
            });

            //MenuItem addItem = new MenuItem();
            //addItem.Text = "Menu";

            //menu.MenuItems.Add(addItem);
            menu.MenuItems.Add(closeItem);

            trayIcon.ContextMenu = menu;    //设置NotifyIcon的右键弹出菜单


            #region 提示功能相关
            readDataTimer.Tick += ReadDataTimer_Tick;
            readDataTimer.Interval = new TimeSpan(0, 0, 1);//一秒执行一次

            alertManager = new AlertManager();
            alertManager.CallBackEvent += AlertManager_CallBackEvent;
            #endregion
        }

        private void ReadDataTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                trayIcon.Icon = (trayIcon.Icon == DS.AFP.WPF.App.Properties.Resources.Coward ? DS.AFP.WPF.App.Properties.Resources.Coward2 : DS.AFP.WPF.App.Properties.Resources.Coward);
            }
            catch (Exception ex) { }
        }

        private DispatcherTimer readDataTimer = new DispatcherTimer();
        private void AlertManager_CallBackEvent(int obj)
        {
            try
            {
                if (obj == 1)//开始播放提示音
                {
                    readDataTimer.Start();
                }
                else//停止播放提示音
                {
                    readDataTimer.Stop();
                    trayIcon.Icon = DS.AFP.WPF.App.Properties.Resources.Coward;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }







        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {

        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            CefManager.Close();
        }
    }
}

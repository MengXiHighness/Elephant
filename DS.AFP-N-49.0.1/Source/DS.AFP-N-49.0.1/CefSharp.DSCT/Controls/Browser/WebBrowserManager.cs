using CefSharp;
using CefSharp.DSCT.Browser;
using CefSharp.DSCT.Handlers;
using CefSharp.Wpf;
using DS.AFP.Common.Core;
using DS.AFP.Common.Core.ConfigurationNameSpace;
using DS.AFP.Framework.Events;
using DS.AFP.Framework.WPF;
using DS.AFP.Framework.WPF.Browser;
using DS.AFP.Framework.WPF.Browser.EventSignal;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Windows.Threading;

namespace CefSharp.DSCT.Controls
{
    public class WebBrowserManager
    {
        public ConcurrentBag<ChromiumWebBrowser> CacheWebBrowsers = null;

        public IEventAggregator EventAggregator
        {
            get
            {
                return DS.AFP.Framework.Spring.GlobalObject.Container.GetObject("IEventAggregator") as IEventAggregator;
            }
        }

        public ILoggerFacade Logger
        {
            get
            {
                return DS.AFP.Framework.Spring.GlobalObject.Container.GetObject("ILoggerFacade") as ILoggerFacade;
            }
        }

        private void ReceiveWebBrowser(string msg)
        {
            try
            {
                SendMessageToJs(msg);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        public WebBrowserManager()
        {
            if (CacheWebBrowsers == null)
            {
                CacheWebBrowsers = new ConcurrentBag<ChromiumWebBrowser>();//new ConcurrentStack<string,ChromiumWebBrowser>();

                //收到浏览器消息后转发给其它浏览器,同时其它c/s界面如果订阅了也可以收到
                EventAggregator.GetEvent<ReceiveWebBrowserMessageEventSignal>().Subscribe(ReceiveWebBrowser, true);

                //发送消息给所有浏览器,供c/s程序发送使用
                EventAggregator.GetEvent<SendMessageToWebBrowserEventSignal>().Subscribe(ReceiveWebBrowser,ThreadOption.BackgroundThread, true);

                //js需要弹出窗口显示在指定屏上
                EventAggregator.GetEvent<OpenWindowEventSignal>().Subscribe(o =>
                {
                    try
                    {
                        OpenNewWindow(o.Uri, o.ScreenNumber);
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex);
                    }
                }, DS.AFP.Framework.Events.ThreadOption.UIThread, true);


                //退出时进行操作
                EventAggregator.GetEvent<BrowserLogOutEventSignal>().Subscribe(o =>
                {
                    try
                    {
                        SendLogOutMessageToJs();
                        //mainChromiumWebBrowser.FrameLoadEnd += (s, e) =>
                        //{
                        //    o.CallBack();
                        //};
                        //mainChromiumWebBrowser.Load(o.Uri);
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex);
                    }
                }, true);
            }
        }
        void SendLogOutMessageToJs()
        {
            Logger.Info("WebBrowserManager SendLogOutMessageToJs:");
            foreach (var cd in CacheWebBrowsers)
            {
                try
                {                    
                   cd.ExecuteScriptAsync("dsExt.LogOut()");
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                }
            }
        }
        void SendMessageToJs(string message)
        {
            Logger.Info("WebBrowserManager SendMessageToJs:" + message);
            foreach (var cd in CacheWebBrowsers)
            {
                try
                {
                    IBrowser browser = cd.GetBrowser();
                    if (browser != null)
                    {
                        List<long> frameindex = browser.GetFrameIdentifiers();
                        foreach (long index in frameindex)
                        {
                            using (var frame = browser.GetFrame(index))
                            {
                                if(frame!=null && frame.IsValid)
                                    frame.ExecuteScriptAsync("dsExt.ReceiveMessage", System.Web.HttpUtility.JavaScriptStringEncode(message));
                            }
                        }
                    }
                    //cd.Value.ExecuteScriptAsync("dsExt.ReceiveMessage", System.Web.HttpUtility.JavaScriptStringEncode(message));
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                }
            }
        }

        //ChromiumWebBrowser mainChromiumWebBrowser;
        public bool AddBrowser(ChromiumWebBrowser Browser)
        {
            try
            {
                //if (CacheWebBrowsers.Count == 0)
                //    mainChromiumWebBrowser = Browser;
               
                CacheWebBrowsers.Add(Browser);
              
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return false;
        }

        public bool RemoveBrowser(ChromiumWebBrowser Browser)
        {
            try
            {
                //if (CacheWebBrowsers.Count == 0)
                //    mainChromiumWebBrowser = Browser;
                
                 CacheWebBrowsers.TryTake(out Browser);
                 Browser = null;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return false;
        }
        public void OpenNewWindow(string uri, int screenNumber)
        {
            try
            {
                var popup = new OpenWindow();

                var chromiumBrowser = new ChromiumWebBrowser
                {
                    Address = uri
                };

                chromiumBrowser.LifeSpanHandler = new LifeSpanHandler();
                chromiumBrowser.DownloadHandler = new DownloadHandler();
                chromiumBrowser.ShowPromptEvent += (count,type) =>
                {
                    EventAggregator.GetEvent<ShowPromptEventSignal>().Publish(new ShowPromptEventArgs() { Count = count, Type = type });
                };
                //IntPtr hwnd = new WindowInteropHelper(popup).Handle;
                chromiumBrowser.RegisterJsObject("afpbrowser", new Browser.DS_AFP_Browser_Common(chromiumBrowser, chromiumBrowser.OnStartProcess, chromiumBrowser.OnMinimize, chromiumBrowser.OnMaximize, chromiumBrowser.OnShowPrompt, chromiumBrowser.OnStartPlayer));

                AddBrowser(chromiumBrowser);


                popup.SetBrowser(chromiumBrowser);

                popup.Closed += (o, e) =>
                {
                    var w = o as OpenWindow;
                    if (w != null && w.Content is IWebBrowser)
                    {
                        (w.Content as IWebBrowser).Dispose();
                        w.Content = null;
                    }
                };
                SetScreen(popup, screenNumber);
                popup.Show();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

        }

        /// <summary>
        /// 设置窗口处于第几屏
        /// </summary>
        /// <param name="win"></param>
        /// <param name="screenNumber"></param>
        public void SetScreen(Window win, int screenNumber)
        {
            #region 设置处于第几屏
            try
            {
                var ScreenNumber = screenNumber;
                if (Screen.AllScreens.Length >= ScreenNumber && ScreenNumber != 1)
                {
                    Screen s = Screen.AllScreens[(ScreenNumber - 1)];

                    System.Drawing.Rectangle r = s.WorkingArea;
                    win.Top = r.Top;
                    win.Left = r.Left;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            #endregion
        }




    }
}

using CefSharp;
using CefSharp.Wpf;
using DS.AFP.Common.Core;
using DS.AFP.Framework.Events;
using DS.AFP.Framework.WPF;
using DS.AFP.Framework.WPF.Browser.EventSignal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Interop;
using DS.AFP.Framework.Spring;
using CefSharp.DSCT.Controls;
using CefSharp.DSCT.Browser;

namespace CefSharp.DSCT.Handlers
{
    public class LifeSpanHandler : ILifeSpanHandler
    {
        public IEventAggregator EventAggregator
        {
            get
            {
                return GlobalObject.Container.GetObject("IEventAggregator") as IEventAggregator;
            }
        }

        public ILoggerFacade Logger
        {
            get
            {
                return GlobalObject.Container.GetObject("ILoggerFacade") as ILoggerFacade;
            }
        }


        public WebBrowserManager WebBrowserManager
        {
            get
            {
                return GlobalObject.Container.GetObject("WebBrowserManager") as WebBrowserManager;
            }
        }

        public bool OnBeforePopup(IWebBrowser browserControl, IBrowser browser, IFrame frame, string targetUrl, string targetFrameName, WindowOpenDisposition targetDisposition, bool userGesture, IPopupFeatures popupFeatures, IWindowInfo windowInfo, IBrowserSettings browserSettings, ref bool noJavascriptAccess, out IWebBrowser newBrowser)
        {
            System.Console.WriteLine("弹出窗地址："+targetUrl);
            newBrowser = null;
            if (!targetUrl.IsNullOrEmpty() && targetUrl.ToLower() == "about:blank")
                return true;

            //  return false;

            //////NOTE: This is experimental
            var chromiumWebBrowser = (ChromiumWebBrowser)browserControl;

            ChromiumWebBrowser chromiumBrowser = null;

            var windowX = (windowInfo.X == int.MinValue) ? double.NaN : windowInfo.X;
            var windowY = (windowInfo.Y == int.MinValue) ? double.NaN : windowInfo.Y;
            var windowWidth = (windowInfo.Width == int.MinValue) ? double.NaN : windowInfo.Width;
            var windowHeight = (windowInfo.Height == int.MinValue) ? double.NaN : windowInfo.Height;

            chromiumWebBrowser.Dispatcher.Invoke(new Action(() =>
            {
                var owner = Window.GetWindow(chromiumWebBrowser);
                chromiumBrowser = new ChromiumWebBrowser
                {
                    Address = targetUrl,
                };
                CefSharpSettings.WcfEnabled = true;
                chromiumBrowser.SetAsPopup();
                //chromiumBrowser.DisplayHandler = new DisplayHandler();
                chromiumBrowser.LifeSpanHandler = this;// chromiumWebBrowser.LifeSpanHandler;
                chromiumBrowser.DownloadHandler = new DownloadHandler();
                chromiumBrowser.ShowPromptEvent += (obj, type) =>
                {
                    EventAggregator.GetEvent<ShowPromptEventSignal>().Publish(new ShowPromptEventArgs() { Count = obj, Type = type });
                };

                var popup = new OpenWindow
                {
                    Left = windowX,
                    Top = windowY,
                    Width = windowWidth,
                    Height = windowHeight,
                    Content = chromiumBrowser,
                    Owner = owner,
                    Title = targetFrameName
                };
                var windowInteropHelper = new WindowInteropHelper(popup);
                //Create the handle Window handle (In WPF there's only one handle per window, not per control)
                var handle = windowInteropHelper.EnsureHandle();
                //chromiumBrowser.Handle = handle;
                chromiumBrowser.RegisterAsyncJsObject("afpbrowser", new DS_AFP_Browser_Common(chromiumBrowser, chromiumBrowser.OnStartProcess, chromiumBrowser.OnMinimize, chromiumBrowser.OnMaximize, chromiumBrowser.OnShowPrompt, chromiumBrowser.OnStartPlayer));
                //chromiumBrowser.RegisterJsObject("window", new WindowJavaScript());

                //popup.SetBrowser(chromiumBrowser);       

                //The parentHandle value will be used to identify monitor info and to act as the parent window for dialogs,
                //context menus, etc. If parentHandle is not provided then the main screen monitor will be used and some
                //functionality that requires a parent window may not function correctly.
                windowInfo.SetAsWindowless(handle,true);

                popup.Closed += (o, e) =>
                {
                    var w = o as OpenWindow;
                    if (w != null && w.Content is IWebBrowser)
                    {
                        (w.Content as IWebBrowser).Dispose();
                        w.Content = null;
                    }
                };

            }));

            newBrowser = chromiumBrowser;
            return false;


        }

        void ILifeSpanHandler.OnAfterCreated(IWebBrowser browserControl, IBrowser browser)
        {
            Console.WriteLine(browserControl.GetHashCode() + ";" + browser.GetHashCode());

            WebBrowserManager.AddBrowser((ChromiumWebBrowser)browserControl);

            if (!browser.IsDisposed && browser.IsPopup)
            {
                var chromiumWebBrowser = (ChromiumWebBrowser)browserControl;

                chromiumWebBrowser.Dispatcher.Invoke(new Action(() =>
                {
                    var owner = Window.GetWindow(chromiumWebBrowser) as OpenWindow;

                    if (owner != null && owner.Content == browserControl)
                    {
                        owner.Show();
                    }
                }));
            }
        }

        bool ILifeSpanHandler.DoClose(IWebBrowser browserControl, IBrowser browser)
        {
            //WebBrowserManager.RemoveBrowser((ChromiumWebBrowser)browserControl);

            if (!browser.IsDisposed && browser.IsPopup && browser.MainFrame.Url == "chrome-devtools://devtools/devtools_app.html")
            {
                return false;
            }
            else
            {
                OnBeforeClose(browserControl, browser);
                return true;
            }
        }

        public void OnBeforeClose(IWebBrowser browserControl, IBrowser browser)
        {
            if (!browser.IsDisposed && browser.IsPopup)
            {
                var chromiumWebBrowser = (ChromiumWebBrowser)browserControl;
                WebBrowserManager.RemoveBrowser((ChromiumWebBrowser)browserControl);
                chromiumWebBrowser.Dispatcher.Invoke((Action)delegate ()
                {
                    var owner = Window.GetWindow(chromiumWebBrowser) as OpenWindow;

                    if (owner != null && owner.Content == browserControl)
                    {
                        owner.Close();
                    }

                });
            }

        }


    }
}
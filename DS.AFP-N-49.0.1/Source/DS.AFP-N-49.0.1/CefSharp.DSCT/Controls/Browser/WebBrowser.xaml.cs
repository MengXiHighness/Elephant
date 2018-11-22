using CefSharp;
using CefSharp.DSCT.Browser;
using CefSharp.DSCT.Controls;
using CefSharp.DSCT.Handlers;
using CefSharp.Wpf;
using DS.AFP.Common.Core;
using DS.AFP.Framework;
using DS.AFP.Framework.Events;
using DS.AFP.Framework.Spring;
using DS.AFP.Framework.WPF.Browser.EventSignal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CefSharp.DSCT.Browser
{
    /// <summary>
    /// WebBrowser.xaml 的交互逻辑
    /// </summary>
    public partial class WebBrowser : UserControl
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
                return DS.AFP.Framework.Spring.GlobalObject.Container.GetObject("ILoggerFacade") as ILoggerFacade;
            }
        }



        #region 扩展属性
        static readonly DependencyProperty UrlProperty;

        /// <summary>
        /// 浏览器默认开启的地址
        /// </summary>
        public string Url
        {
            get
            {
                return (string)GetValue(UrlProperty);
            }
            set
            {
                SetValue(UrlProperty, value);
            }
        }
        static void UrlChange(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            WebBrowser webBrowser = (WebBrowser)sender;
            if ((!string.IsNullOrEmpty(webBrowser.browser.Address) && !string.IsNullOrEmpty(webBrowser.Url) && webBrowser.browser.Address.ToLower() != webBrowser.Url.ToLower())
                || (string.IsNullOrEmpty(webBrowser.browser.Address) && !string.IsNullOrEmpty(webBrowser.Url))
                )
            {
                webBrowser.browser.Address = webBrowser.Url;
            }
        }

        static WebBrowser()
        {
            UrlProperty = DependencyProperty.Register("Url", typeof(string), typeof(WebBrowser), new FrameworkPropertyMetadata(null, UrlChange));
        }
        #endregion



        public WebBrowser()
        {
            InitializeComponent();

            if (DS.AFP.Framework.Spring.GlobalObject.Container.IsTypeRegistered(DS.AFP.Framework.Spring.GlobalObject.Container.GetType()))
                DS.AFP.Framework.Spring.GlobalObject.Container.RegisterTypeIfMissing<WebBrowserManager>("WebBrowserManager", true);

            //this.browser.AllowDrop = true;
            CefSharpSettings.WcfEnabled = true;
           // this.browser.DisplayHandler = new DisplayHandler();
            this.browser.LifeSpanHandler = new LifeSpanHandler();
            this.browser.DownloadHandler = new DownloadHandler();
            //this.browser.AccessibilityHandler = new AccessibilityHandler();
            //this.browser.LoadHandler = new LoadHandler();
            this.browser.DragHandler = new DragHandler(); 
            // this.browser.KeyboardHandler = new KeyBoardHander();
            this.browser.MenuHandler = new MenuHandler();
            this.browser.ShowPromptEvent += (count, type) =>
            {
                EventAggregator.GetEvent<ShowPromptEventSignal>().Publish(new ShowPromptEventArgs() { Count = count, Type = type });
            };


            this.browser.RegisterAsyncJsObject("afpbrowser", new DS_AFP_Browser_Common(this.browser, browser.OnStartProcess, browser.OnMinimize, browser.OnMaximize, browser.OnShowPrompt, browser.OnStartPlayer));
           

            this.KeyUp += (s, e) =>
            {
                if (e.Key == Key.F12)
                {
                    try
                    {
                        this.browser.ShowDevTools();
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex);
                    }
                }
                if (e.Key == Key.F5)
                {
                    try
                    {
                        this.browser.Reload();
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex);
                    }
                }
            };
           

        }

      


        /// <summary>
        /// 加载一个新的地址
        /// </summary>
        /// <param name="url"></param>
        public void LoadUri(string url)
        {
            this.browser.Load(url);
        }

        public ChromiumWebBrowser interWebBrowser
        {
            get
            {
                return this.browser;
            }
        }
    }
}

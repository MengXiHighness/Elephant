using DS.AFP.Framework.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CefSharp;
using DS.AFP.Common.Core;
using CefSharp.Wpf;
using DS.AFP.Common.Core.ConfigurationNameSpace;
using System.Windows.Forms;
using DS.AFP.Framework.WPF.Browser.EventSignal;

namespace DS.AFP.WebBrowser
{
    /// <summary>
    /// Browser.xaml 的交互逻辑
    /// </summary>
    public partial class Browser : System.Windows.Controls.UserControl
    {
        public bool isSubscribe = false;
        public static ChromiumWebBrowser mainChromiumWebBrowser = null;
        public static IWindowInfo windowInfo = null;
        public Browser()
        {
            InitializeComponent();

            

            var DefaultUri = WebBrowserModule.DsAddinConfig.Params["DefaultUri"].Value;
            this.browser.Url = DefaultUri;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
                WebBrowserModule.EventAggregator.GetEvent<SendMessageToWebBrowserEventSignal>().Publish("aaaaaaaaaaaaa");
        }
    }
}

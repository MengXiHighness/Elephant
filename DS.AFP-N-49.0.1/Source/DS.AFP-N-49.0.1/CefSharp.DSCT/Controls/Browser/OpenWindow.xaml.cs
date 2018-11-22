using CefSharp.Wpf;
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
using System.Windows.Shapes;
using CefSharp;

using DS.AFP.Framework.Spring;
using CefSharp.DSCT.Controls;

namespace CefSharp.DSCT
{
    /// <summary>
    /// OpenWindow.xaml 的交互逻辑
    /// </summary>
    public partial class OpenWindow : Window
    {
       // public ChromiumWebBrowser contentControl = null;
        public WebBrowserManager WebBrowserManager
        {
            get
            {
                return GlobalObject.Container.GetObject("WebBrowserManager") as WebBrowserManager;
            }
        }
        public OpenWindow()
        {
           
            InitializeComponent();
            this.Closed += OpenWindow_Closed;

            //this.move.MouseLeftButtonDown += (s, e) =>
            //{
            //    this.DragMove();
            //};

            //this.img_Minimized.MouseLeftButtonUp += (s, e) =>
            //{
            //    this.WindowState = WindowState.Minimized;
            //};

            //this.img_Maximized.MouseLeftButtonUp += (s, e) =>
            //{
            //    this.WindowState = (this.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized);
            //};

            //this.img_Close.MouseLeftButtonUp += (s, e) =>
            //{
            //    this.Close();
            //};

            //this.border_Minimized.MouseEnter += (s, e) =>
            //{
            //    MOver(s, 0);
            //};
            //this.border_Maximized.MouseEnter += (s, e) =>
            //{
            //    MOver(s, 0);
            //};


            //this.border_Minimized.MouseLeave += (s, e) =>
            //{
            //    MOver(s, 1);
            //};
            //this.border_Maximized.MouseLeave += (s, e) =>
            //{
            //    MOver(s, 1);
            //};



            //this.border_Close.MouseEnter += (s, e) =>
            //{
            //    BorderCloseMOver(s, 0);
            //};

            //this.border_Close.MouseLeave += (s, e) =>
            //{
            //    BorderCloseMOver(s, 1);
            //};

            //this.KeyUp += (s, e) =>
            //{
            //    if (e.Key == Key.F12)
            //    {
            //        try
            //        {
            //            if (contentControl != null)
            //            {
            //                var browser = contentControl as ChromiumWebBrowser;
            //                if (browser != null)
            //                    browser.ShowDevTools();
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            WebBrowserModule.Logger.Error(ex);
            //        }
            //    }
            //};
        }

        private void OpenWindow_Closed(object sender, EventArgs e)
        {
            ChromiumWebBrowser c = this.Content as ChromiumWebBrowser;
            WebBrowserManager.RemoveBrowser(c);
        }

        public void SetBrowser(ChromiumWebBrowser contentControl)
        {
            //this.contentControl = contentControl;
            this.Content = contentControl;
            contentControl.TitleChanged += (s, e) =>
            {
                this.Title = e.NewValue.ToString();
                this.ltitle.Content = e.NewValue.ToString();
            };
            //this.grid.Children.Add(contentControl);
            //this.Content = contentControl;
        }


        public void MOver(object obj, int type)
        {
            var border = obj as Border;
            if (type == 0)
            {
                Color c = (Color)ColorConverter.ConvertFromString("#e5e5e5");
                border.Background = new SolidColorBrush(c);
            }
            else
            {
                border.Background = new SolidColorBrush(Colors.Transparent);// ColorConverter.ConvertFromString("#e5e5e5") as Brush;
            }
        }

        public void BorderCloseMOver(object obj, int type)
        {
            var border = obj as Border;
            if (type == 0)
            {
                Color c = Colors.Red;
                border.Background = new SolidColorBrush(c);
                img_Close.Source= new BitmapImage(new Uri("pack://application:,,,/DS.AFP.Framework.WPF;component/Controls/Browser/images/gb2.png", UriKind.RelativeOrAbsolute));
            }
            else
            {
                border.Background = new SolidColorBrush(Colors.Transparent);
                img_Close.Source = new BitmapImage(new Uri("pack://application:,,,/DS.AFP.Framework.WPF;component/Controls/Browser/images/gb.png", UriKind.RelativeOrAbsolute));
            }
        }

        
    }
}

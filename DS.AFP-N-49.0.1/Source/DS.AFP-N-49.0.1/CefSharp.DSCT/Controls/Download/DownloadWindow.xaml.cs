using DS.AFP.Framework.WPF.Browser.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CefSharp.DSCT
{
    /// <summary>
    /// DownloadWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DownloadWindow : Window
    {
        DownloadItemViewModel vm;
        //WebClient wc = new WebClient();

        public DownloadWindow()
        {
            InitializeComponent();
            //this.img.Source = Imaging.CreateBitmapSourceFromHBitmap(Properties.Resources.download.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            this.Loaded += DownloadWindow_Loaded;
            //this.Closing += DownloadWindow_Closing;
        }

        //private void DownloadWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        //{
        //    try
        //    {
        //        if (wc != null)
        //        {
        //            wc.CancelAsync();
        //            wc.Dispose();
        //            wc = null;
        //        }
        //    }
        //    catch (Exception ex) { }
        //}

        bool isload = false;
        private void DownloadWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //if (isload) return;

            //isload = true;
            //try
            //{
            //    vm = this.DataContext as DownloadItemViewModel;
            //    if (vm != null)
            //    {
            //        wc.DownloadProgressChanged += Wc_DownloadProgressChanged;
            //        DownloadFile();
            //    }
            //}
            //catch (Exception ex) { }
        }
        //void DownloadFile()
        //{
        //    try
        //    {
        //        wc.DownloadFileAsync(new Uri(vm.Url), vm.FileName);
        //    }
        //    catch (Exception ex) { }
        //}

        //private void Wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        //{
        //    try
        //    {
        //        vm.ReceivedBytes = e.BytesReceived;
        //        vm.TotalBytes = vm.TotalBytes;
        //    }
        //    catch (Exception ex) { }
        //}





    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DS.AFP.Common.Core.Dynamic;
using DS.AFP.Common.Core.Utility;
using DS.AFP.Framework.WPF;
using Spring.Context;
using DS.AFP.Common.Core;
using DS.AFP.Common.Core.ConfigurationNameSpace;
using System.Windows.Forms;
using DS.AFP.WPF.App.Utility;
using System.Windows.Interop;
using DS.AFP.Framework.WPF.Browser;

namespace DS.AFP.WPF.App
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : WindowBase, INotifiaction
    {
        private const double topOffset = 130;
        private const double leftOffset = 300;
        readonly DsNotification dsNotifications = new DsNotification();
        public static IDsEnvironment DsEnvironment
        {
            get;
            set;
        }

        public MainWindow(IDsEnvironment dsEnvironment, IDsConfigurationSection dsconfig)
        {
            InitializeComponent();

            dsNotifications.Top = SystemParameters.WorkArea.Height - topOffset;
            dsNotifications.Left = SystemParameters.WorkArea.Left + SystemParameters.WorkArea.Width - leftOffset;
            DsEnvironment = dsEnvironment;

            /////这里是在初始化加载控件之前就已经做了，故使用配置的方式进行加载
            ////1、创建默认主题（默认情况下是DeepBlue）
            if (dsconfig.Themes.Count > 0)
            {
                foreach (ThemeElement item in dsconfig.Themes)
                {
                    ThemeManage.CurrentTheme = item.Name;
                    break;
                }
            }
            System.Windows.Application.Current.Resources.MergedDictionaries.Clear();
            ////2、加载控件的样式
            foreach (ModuleConfigurationElement module in dsconfig.Modules)
            {
                string assemblyfile = module.AssemblyFile.Substring(module.AssemblyFile.LastIndexOf('/') + 1);
                string path = assemblyfile.Substring(0, assemblyfile.LastIndexOf('.'));
                ThemeManage.EnsureApplicationResources(ThemeManage.CurrentTheme, path);
            }
            //////这部分代码怎么实现的问题---gefx
            //if (dsEnvironment.AddinInfos == null || dsEnvironment.AddinInfos.Count <= 0)
            //    DsEnvironment = ParentContaioner.GetObject<IDsEnvironment>();

            //foreach (AddinInfo addininfo in DsEnvironment.AddinInfos.Values)
            //{
            //    ////这里又有一个问题就是命名空间要更程序集的名称对应，不然的话就需要截取字符串的形式啦！！
            //    string assemblyname = addininfo.AddinNameSpace;
            //    ThemeManage.EnsureApplicationResources(ThemeManage.CurrentTheme, assemblyname);
            //}
            #region Debug
            //base.SetWindowTheme("DeepBlue", "DS.AFP.UIFramework");
            #endregion

            App.iconMouseEvent += App_iconMouseEvent;

            FullScreenManager.RepairWpfWindowFullScreenBehavior(this);
        }

        private void App_iconMouseEvent(System.Windows.Forms.MouseEventArgs obj)
        {
            if (obj != null && obj.Button == MouseButtons.Left)
            {
                if (this.WindowState != WindowState.Maximized)
                {
                    this.WindowState = WindowState.Maximized;
                    this.Activate();
                }
            }
        }



        /// <summary>
        /// 设置显示方式
        /// </summary>
        /// <param name="wsw"></param>
        public override void SetWindowStyle(WindowStyleWays wsw)
        {
            switch (wsw.StyleWays)
            {
                case StyleWays.FullScreen:
                    {
                        this.WindowState = System.Windows.WindowState.Maximized;
                        dsNotifications.Top = SystemParameters.WorkArea.Height - topOffset;
                        break;
                    }
                case StyleWays.ShowTaskBar:
                    {
                        this.WindowState = System.Windows.WindowState.Normal;
                        this.Height = SystemParameters.WorkArea.Height;
                        this.Width = SystemParameters.WorkArea.Width;
                        this.Left = SystemParameters.WorkArea.X;
                        this.Top = SystemParameters.WorkArea.Y;
                        dsNotifications.Top = SystemParameters.WorkArea.Height - topOffset - 39;
                        dsNotifications.Left = SystemParameters.WorkArea.Left + SystemParameters.WorkArea.Width - leftOffset;

                        break;
                    }
            }
            switch (wsw.WindowState)
            {
                case System.Windows.WindowState.Maximized:
                    {
                        this.WindowState = System.Windows.WindowState.Maximized;
                        break;
                    }
                case System.Windows.WindowState.Normal:
                    {
                        this.WindowState = System.Windows.WindowState.Normal;
                        break;
                    }
                case System.Windows.WindowState.Minimized:
                    {
                        this.WindowState = System.Windows.WindowState.Minimized;
                        break;
                    }
            }
        }

        public void AddNotification(Notification notification)
        {
            dsNotifications.AddNotification(notification);
        }

        public void RemoveNotification(Notification notification)
        {
            dsNotifications.RemoveNotification(notification);
        }

        private void close_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }



        private void WindowBase_Loaded(object sender, RoutedEventArgs e)
        {
            //副屏幕显示代码 开始
            //this.WindowStartupLocation = WindowStartupLocation.Manual;
            //this.Top = 0;
            //this.Left = System.Windows.SystemParameters.MaximizedPrimaryScreenWidth;
            //this.WindowStyle = System.Windows.WindowStyle.SingleBorderWindow;
            //this.WindowState = System.Windows.WindowState.Normal;
            //this.WindowStyle = System.Windows.WindowStyle.None;
            //this.WindowState = System.Windows.WindowState.Maximized;  
            //副屏幕显示代码 结束



            //var wbm = App.bootstrapper.Container.GetObject("WebBrowserManager") as WebBrowserManager;
            //if (wbm != null)
            //{

            //}



        }



    }

    public class lParam
    {
        public string playerid { get; set; }
        public string json { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Spring.Context;
using Microsoft.Practices.ServiceLocation;
using System.Threading;

namespace DS.AFP.Framework.WPF
{
    /// <summary>
    /// 窗体的基类
    /// 提供消息通知功能，换肤功能
    /// </summary>
    public class WindowBase : Window, INotifiaction, IShell
    {
        //private static string themeName ; //"Office2007Blue";
        //private static string languageType;
        private static Window mainWindow = null;

        public static readonly DependencyProperty ThemeProperty
            = DependencyProperty.Register("ThemeName", typeof(string), typeof(WindowBase), new PropertyMetadata("DeepBlue"), new ValidateValueCallback((o) =>
            {
                return true;
            }));

        /// <summary>
        /// 主题
        /// </summary>
        public string ThemeName
        {
            get { return (string)GetValue(WindowBase.ThemeProperty); }
            set { SetValue(WindowBase.ThemeProperty, value); }
        }

        public Window MainWin
        {
            get
            {
                return mainWindow;
            }
        }

        public WindowBase()
        {
            if (mainWindow == null)
            {
                foreach (var w in Application.Current.Windows)
                {
                    if (w is IShell && w is INotifiaction)
                    {
                        mainWindow = w as Window;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 添加通知
        /// <code>
        /// AddNotification(new Notification(NotificationType.Info, "aaaaaaa", "fdfsdfsffffffffffffffffffffffffffffffffffdfdsfs"));
        /// </code>
        /// </summary>
        /// <param name="notification"></param>
        public void AddNotification(Notification notification)
        {
            INotifiaction n = mainWindow as INotifiaction;
            n.AddNotification(notification);
        }

        /// <summary>
        /// 移除通知
        /// <code>
        /// INotifiaction n = mainWindow as INotifiaction;
        /// n.RemoveNotification(notification);
        /// </code>
        /// </summary>
        /// <param name="notification"></param>
        public void RemoveNotification(Notification notification)
        {
            INotifiaction n = mainWindow as INotifiaction;
            n.RemoveNotification(notification);
        }


        public void Active()
        {
            this.Activate();
        }

        /// <summary>
        /// 设置窗体样式
        /// </summary>
        /// <param name="wsw"></param>
        public virtual void SetWindowStyle(WindowStyleWays wsw)
        {
            
        }

        /// <summary>
        /// 设置窗体主题
        /// </summary>
        /// <param name="themeName"></param>
        public void SetWindowTheme(string themeName, string HostName)
        {
            ThemeManage.EnsureApplicationResources(themeName, HostName);
        }

        

    }
}

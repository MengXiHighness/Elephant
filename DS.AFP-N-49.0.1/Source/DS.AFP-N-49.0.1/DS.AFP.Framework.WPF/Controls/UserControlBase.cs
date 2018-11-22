using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace DS.AFP.Framework.WPF
{
    /// <summary>
    /// 用户控件基类
    /// </summary>
    public class UserControlBase : UserControl, INotifiaction,IView
    {
        private static Window mainWindow = null;
        public UserControlBase()
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

        private string title = "窗体";
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                this.title = value;
            }

        }

        private Action closeAction = null;
        public void InitEvent(Action closeAction)
        {
            this.closeAction = closeAction;

        }

        public virtual void Close()
        {
            this.closeAction();
        }



        

        /// <summary>
        /// 添加通知
        /// <code>
        /// AddNotification(new Notification(NotificationType.Info, "aaaaaaa", "fdfsdfsfffffffffffdsfs"));
        /// </code>
        /// </summary>
        /// <param name="notification"></param>
        public void AddNotification(Notification notification)
        {
            INotifiaction n = mainWindow as INotifiaction;
            n.AddNotification(notification);
        }

        /// <summary>
        /// 移出通知
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

        public virtual void SetSource()
        {
          
        }

    }
}

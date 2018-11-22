using System.Collections.ObjectModel;
using System.ComponentModel;

namespace DS.AFP.Framework.WPF
{
    /// <summary>
    /// 通知类型枚举
    /// </summary>
    public enum NotificationType
    {
        Info=0
    }

    /// <summary>
    /// 通知类型
    /// </summary>
    public class Notification : INotifyPropertyChanged
    {
        /// <summary>
        /// 通知构造
        /// </summary>
        /// <param name="nt">通知类型</param>
        /// <param name="title">通知抬头</param>
        /// <param name="message">消息内容</param>
        public Notification(NotificationType nt,string title, string message)
        {
            switch (nt)
            {
                case NotificationType.Info:
                    {
                        ImageUrl = "pack://application:,,,/Resources/Images/notification-icon.png";
                        break;
                    }

            }
            Title = title;
            Message = message;
        }

        private string message;
        public string Message
        {
            get { return message; }

            set
            {
                if (message == value) return;
                message = value;
                OnPropertyChanged("Message");
            }
        }

        private int id;
        public int Id
        {
            get { return id; }

            set
            {
                if (id == value) return;
                id = value;
                OnPropertyChanged("Id");
            }
        }

        private string imageUrl;
        public string ImageUrl
        {
            get { return imageUrl; }

            set
            {
                if (imageUrl == value) return;
                imageUrl = value;
                OnPropertyChanged("ImageUrl");
            }
        }

        private string title;
        public string Title
        {
            get { return title; }

            set
            {
                if (title == value) return;
                title = value;
                OnPropertyChanged("Title");
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    /// <summary>
    /// Notification类型的ObservableCollection集合
    /// </summary>
    public class Notifications : ObservableCollection<Notification> { }
}
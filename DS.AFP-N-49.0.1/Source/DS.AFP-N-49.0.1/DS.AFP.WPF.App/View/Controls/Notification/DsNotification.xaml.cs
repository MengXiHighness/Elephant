using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using DS.AFP.Framework.WPF;
using Spring.Context;

namespace DS.AFP.WPF.App
{
    public partial class DsNotification:WindowBase
    {
        private const byte MAX_NOTIFICATIONS = 1;
        private int count;
        public Notifications Notifications = new Notifications();
        private readonly Notifications buffer = new Notifications();
        private static object lockObject = new object();

        public DsNotification()
        {
            
            DsNotifiactionsViewModel vm = new DsNotifiactionsViewModel();
            vm.Notifications = Notifications;
            InitializeComponent();
            NotificationsControl.DataContext = vm;
        }

        private void BeginStoryboard()
        {
            Storyboard story = NotificationsControl.FindResource("MovieStoryboard") as Storyboard;
            story.Stop();
            ThicknessAnimation anim = (ThicknessAnimation)story.Children[0];
            Thickness fromPosion = new Thickness(0, 100, 0, 0);
            Thickness toPosion = new Thickness(0, 0, 0, 0);
            if (MainWin != null)
            {
                if (MainWin.WindowState == System.Windows.WindowState.Maximized)
                {
                    fromPosion = new Thickness(0, 100, 0, 0);
                    toPosion = new Thickness(0, 0, 0, 0);
                }
                else
                {
                    toPosion = new Thickness(0, 0, 0, 0);
                    fromPosion = new Thickness(0, 100, 0, 0);
                }
            }

            anim.From = fromPosion;
            anim.To = toPosion;
            anim.Duration = new Duration(TimeSpan.FromMilliseconds(1000));
            story.Begin(this, true);
        }
        


        public void AddNotification(Notification notification)
        {
            //BeginStoryboard();
            lock (lockObject)
            {
                notification.Id = count++;
                if (Notifications.Count + 1 > MAX_NOTIFICATIONS)
                    buffer.Add(notification);
                else
                    Notifications.Add(notification);

                
                //Show window if there're notifications
                if (Notifications.Count >0 && !IsActive)
                {
                    if (!notification.Equals(Notifications[0]))
                        return;
                    Show();
                    BeginStoryboard();
                }
            }
        }

        public void RemoveNotification(Notification notification)
        {
            lock (lockObject)
            {
                if (Notifications.Contains(notification))
                    Notifications.Remove(notification);

                if (buffer.Count > 0)
                {
                    Notifications.Add(buffer[0]);
                    buffer.RemoveAt(0);
                }

                if (Notifications.Count > 0 )
                {
                    BeginStoryboard();
                }
                //关闭
                if (Notifications.Count < 1)
                    Hide();
               
            }
        }

        private void NotificationWindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Height != 0.0)
                return;
            var element = sender as Grid;
            RemoveNotification(Notifications.First(n => n.Id == Int32.Parse(element.Tag.ToString())));
        }
    }
}

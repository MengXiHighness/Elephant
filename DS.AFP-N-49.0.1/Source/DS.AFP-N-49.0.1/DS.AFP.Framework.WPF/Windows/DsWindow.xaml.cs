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

namespace DS.AFP.Framework.WPF
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class DsWindow : WindowBase
    {
        public DsWindow()
        {
            InitializeComponent();
        }

        private void CloseButton_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OkButton_Click_1(object sender, RoutedEventArgs e)
        {
            AddNotification(new Notification(NotificationType.Info, "aaaaaaa", "fdfsdfsffffffffffffffffffffffffffffffffffdfdsfs"));
        }
    }
}

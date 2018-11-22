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
using DS.AFP.Framework.WPF;

namespace DS.AFP.Framework.WPF
{
    /// <summary>
    /// AlterWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AlterWindow : WindowBase
    {
        public string Content
        {
            set
            {
                TextBlock lb1 = this.FindName("MessageContent") as TextBlock;
                lb1.Text = value;
            }
        }
        public AlterWindow()
        {
            InitializeComponent();
        }

        private void CloseButton_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OkButton_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
    }
}

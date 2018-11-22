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
    /// ConfirWin.xaml 的交互逻辑
    /// </summary>
    public partial class ConfirmWin : WindowBase
    {
        public ConfirmWin(WindowStyleBase winStyle)
        {
            InitializeComponent();
            this.ShowInTaskbar = false;
            if (winStyle.WinStartType == WindowStartLocationType.CenterScreen)
            {
                this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            }
            else
            {
                this.Left = winStyle.Pis_X;
                this.Top = winStyle.Pis_Y;
                //Rect rc = SystemParameters.WorkArea;//获取工作区大小
                //this.WindowStartupLocation = WindowStartupLocation.Manual;
                //if (winStyle.Pis_X + winStyle.Width > rc.Width)
                //{
                //    if (winStyle.Pis_Y < rc.Height-winStyle.Height)
                //    {
                //        this.Left = winStyle.Pis_X - winStyle.Width;
                //        this.Top = winStyle.Height;
                //    }
                //    else
                //    {
                //        this.Left = winStyle.Pis_X - winStyle.Width;
                //        this.Top = winStyle.Pis_Y;
                //    }
                //}
                //else if (winStyle.Pis_X < winStyle.Width)
                //{
                //    if (winStyle.Pis_Y < rc.Height - winStyle.Height)
                //    {
                //        this.Left = winStyle.Pis_X - winStyle.Width;
                //        this.Top = winStyle.Height;
                //    }
                //    else
                //    {
                //        this.Left = winStyle.Pis_X - winStyle.Width;
                //        this.Top = winStyle.Pis_Y;
                //    }
                //}
                //else
                //{
                //    this.Left = winStyle.Pis_X;
                //    this.Top = winStyle.Pis_Y;
                //}
            }
        }

        /// <summary>
        /// 标题
        /// </summary>
        public string WinTitle
        {
            set
            {
                TextBlock title = this.FindName("DS_AFP_N_Title") as TextBlock;
                title.Text = value;
            }
        }

        /// <summary>
        /// 内容
        /// </summary>
        public string WinContent
        {
            set
            {
                TextBlock content = this.FindName("DS_AFP_N_MessageContent") as TextBlock;
                content.Text = value;
            }
        }

        /// <summary>
        /// 确认按钮内容
        /// </summary>
        public string OkButtonText
        {
            set
            {
                Button btn = this.FindName("DS_AFP_N_OkButton") as Button;
                btn.Content = value;
            }
        }

        /// <summary>
        /// 取消按钮内容
        /// </summary>
        public string CancleButtonText
        {
            set
            {
                Button btn = this.FindName("DS_AFP_N_CancleButton") as Button;
                btn.Content = value;
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 移动窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackGround_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancleButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}

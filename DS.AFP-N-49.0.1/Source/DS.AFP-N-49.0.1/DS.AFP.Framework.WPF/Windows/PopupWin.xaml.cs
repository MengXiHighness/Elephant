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
using System.Collections.ObjectModel;
using Spring.Context;

namespace DS.AFP.Framework.WPF
{
    /// <summary>
    /// UserControlWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PopuWin : WindowBase
    {
        Rect rcnormal;//定义一个全局rect记录还原状态下窗口的位置和大小。

        public PopuWin()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //private void btnMin_Click(object sender, RoutedEventArgs e)
        //{
        //    this.WindowState = WindowState.Minimized;
        //}

        //private void btnMax_Click(object sender, RoutedEventArgs e)
        //{
        //    Rect rc = SystemParameters.WorkArea;//获取工作区大小
        //    if (this.Width == rc.Width)
        //    {//Normal
        //        this.Left = rcnormal.Left;
        //        this.Top = rcnormal.Top;
        //        this.Width = rcnormal.Width;
        //        this.Height = rcnormal.Height;
        //    }
        //    else//最大化
        //    {
        //        rcnormal = new Rect(this.Left, this.Top, this.Width, this.Height);//保存下当前位置与大小
        //        this.Left = 0;//设置位置
        //        this.Top = 0;
        //        this.Width = rc.Width;
        //        this.Height = rc.Height;
        //    }
        //}

        private void BackGround_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

    }
}

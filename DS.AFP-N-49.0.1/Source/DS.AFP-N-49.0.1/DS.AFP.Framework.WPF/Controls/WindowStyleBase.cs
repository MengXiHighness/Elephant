using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace DS.AFP.Framework.WPF
{
    /// <summary>
    /// 窗体样式
    /// </summary>
    public class WindowStyleBase
    {
        private double width=300;
        /// <summary>
        /// 窗口宽度
        /// </summary>
        public double Width
        {
            get { return width; }
            set { width = value; }
        }

        private double height=300;
        /// <summary>
        /// 窗体高度
        /// </summary>
        public double Height
        {
            get { return height; }
            set { height = value; }
        }

        private string title;
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private string description;
        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private string okBtnText = "确定";
        /// <summary>
        /// 确定按钮内容
        /// </summary>
        public string OkBtnText
        {
            get { return okBtnText; }
            set { okBtnText = value; }
        }

        private string cancleBtnText = "取消";
        /// <summary>
        /// 取消按钮内容
        /// </summary>
        public string CancleBtnText
        {
            get { return cancleBtnText; }
            set { cancleBtnText = value; }
        }

        private double pis_X = 0;
        /// <summary>
        /// 窗口上边缘相对于桌面位置
        /// </summary>
        public double Pis_X
        {
            get { return pis_X; }
            set { pis_X = value; }
        }

        private double pis_Y = 0;
        /// <summary>
        /// 窗口左边缘相对于桌面位置
        /// </summary>
        public double Pis_Y
        {
            get { return pis_Y; }
            set { pis_Y = value; }
        }

        private WindowStartLocationType winStartType = WindowStartLocationType.CenterScreen;

        public WindowStartLocationType WinStartType
        {
            get { return winStartType; }
            set { winStartType = value; }
        }

        private windowType wintype=windowType.AlertWindow;
        /// <summary>
        /// 窗口类型
        /// </summary>
        public windowType Wintype
        {
            get { return wintype; }
            set { wintype = value; }
        }
    }

    /// <summary>
    /// 窗口类型枚举
    /// </summary>
    public enum windowType
    { 
        AlertWindow=0,
        ConfirmWindow 
    }

    /// <summary>
    /// 窗口启动位置枚举
    /// </summary>
    public enum WindowStartLocationType
    {
        CenterScreen = 0,
        FollowParent
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace DS.AFP.Framework.WPF
{
    /// <summary>
    /// Shell接口（提供设置窗体显示方式与皮肤等功能）
    /// </summary>
    public interface IShell
    {
        /// <summary>
        /// 显示
        /// </summary>
        void Show();

        /// <summary>
        /// 激活状态
        /// </summary>
        void Active();

        /// <summary>
        /// 窗体显示方式
        /// </summary>
        /// <param name="wsw"></param>
        void SetWindowStyle(WindowStyleWays wsw);

        /// <summary>
        /// 设置皮肤
        /// </summary>
        /// <param name="themeName"></param>
        void SetWindowTheme(string themeName, string HostName);

        string ThemeName { get; set; }

    }

    /// <summary>
    /// 窗体展示风格枚举（全屏、任务栏显示等）
    /// </summary>
    public enum StyleWays
    {
        FullScreen=0,
        ShowTaskBar=1
    }

    public class WindowStyleWays
    {
        public StyleWays StyleWays { get; set; }

        public System.Windows.WindowState WindowState { get; set; }
    }
}

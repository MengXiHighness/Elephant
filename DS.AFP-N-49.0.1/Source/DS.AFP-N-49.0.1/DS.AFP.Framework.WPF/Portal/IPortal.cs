using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.AFP.Framework.Regions;

namespace DS.AFP.Framework.WPF.Portal
{
    /// <summary>
    /// 入口接口
    /// </summary>
    public interface IPortal
    {
        /// <summary>
        /// Logo
        /// </summary>
        string Logo { get; set; }

        /// <summary>
        ///公司信息
        /// </summary>
        string ComapnyInfo { get; set; }

        /// <summary>
        /// 公司Logo
        /// </summary>
        string ComapnyLogo { get; set; }

        /// <summary>
        /// 系统版本
        /// </summary>
        string SystemVersion { get; set; }

        /// <summary>
        /// 许可证信息
        /// </summary>
        string LicenseMsg { get; set; }

        /// <summary>
        /// 背景
        /// </summary>
        string ImgBackground { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// 左侧导航
        /// </summary>
        Navigation Navigation { get; set; }

        /// <summary>
        /// 底部导航
        /// </summary>
        Navigation BottomNavigation { get; set; }

        /// <summary>
        /// 下拉导航
        /// </summary>
        ToolBar ToolBar { get; set; }

        /// <summary>
        /// 状态灯
        /// </summary>
        StatusBar StatusBar { get; set; }
    }
}

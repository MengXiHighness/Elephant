///<summary>
/// Copyright (c) 2011-2015 上海迪爱斯通信设备有限公司
/// 作  者：姜宁
/// 时  间：2013-11-13 11:13:09
/// 描  述：Windows服务配置节接口
///</summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.AFP.Common.Core.ConfigurationNameSpace
{
    /// <summary>
    /// Windows服务配置节接口（WindowsServiceConfigurationSection基类）
    /// </summary>
    public interface IWindowsServiceConfigurationSection
    {
        /// <summary>
        /// 服务元素
        /// </summary>
        WindowsServiceConfigurationElement Service { get; }
    }
}

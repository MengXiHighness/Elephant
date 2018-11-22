///<summary>
/// Copyright (c) 2011-2015 上海迪爱斯通信设备有限公司
/// 作  者：姜宁
/// 时  间：2013-11-13 11:13:09
/// 描  述：Windows服务配置节类
///</summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace DS.AFP.Common.Core.ConfigurationNameSpace
{
    /// <summary>
    /// Windows服务配置节类(主参数有Service)
    /// <code>
    /// GlobalParams.ServiceName = GlobalParams.WindowsService.Service.ServiceName;
    /// </code>
    /// </summary>
    public class WindowsServiceConfigurationSection : ConfigurationSection, IWindowsServiceConfigurationSection
    {

        /// <summary>
        /// 服务
        /// </summary>
        [ConfigurationProperty("service")]
        public WindowsServiceConfigurationElement Service
        {
            get
            { 
                return (WindowsServiceConfigurationElement)base["service"]; 
            }
        }
    }
}

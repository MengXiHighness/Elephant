///<summary>
/// Copyright (c) 2011-2015 上海迪爱斯通信设备有限公司
/// 作  者：姜宁
/// 时  间：2013-11-13 11:13:09
/// 描  述：平台配置组类
///</summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Reflection;

namespace DS.AFP.Common.Core.ConfigurationNameSpace
{
    /// <summary>
    /// 平台配置组（含系统参数配置项、数据库连接串配置项、通信节点配置项、模块配置项等）
    /// </summary>
    public class ThemelSection : ConfigurationSection
    {
        [ConfigurationProperty("themes", IsDefaultCollection = true, IsKey = false)]
        public ThemeCollection Themes
        {
            get
            {
                return (ThemeCollection)base["themes"];
            }
        }

    }
}

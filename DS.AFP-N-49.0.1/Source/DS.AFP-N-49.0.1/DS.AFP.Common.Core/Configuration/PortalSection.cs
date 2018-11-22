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
    public class PortalSection : ConfigurationSection, IPortalSection
    {

        [ConfigurationProperty("description", DefaultValue = "")]
        public string Description
        {
            get
            { return (string)base["description"]; }
            set
            { base["description"] = value; }
        }



        [ConfigurationProperty("statusBar", IsDefaultCollection = true, IsKey = false)]
        public StatusBarCollection StatusBar
        {
            get
            {
                return (StatusBarCollection)base["statusBar"];
            }
        }

        [ConfigurationProperty("navigation", IsDefaultCollection = true, IsKey = false)]
        public NavigationCollection Navigation
        {
            get
            {
                return (NavigationCollection)base["navigation"];
            }
        }

        [ConfigurationProperty("toolBar", IsDefaultCollection = true, IsKey = false)]
        public ToolBarCollection ToolBar
        {
            get
            {
                return (ToolBarCollection)base["toolBar"];
            }
        }

        [ConfigurationProperty("logo")]
        public string Logo
        {
            get
            {
                return (string)base["logo"];
            }

        }

        [ConfigurationProperty("comapnyLogo")]
        public string ComapnyLogo
        {
            get
            {
                return (string)base["comapnyLogo"];
            }
        }

        [ConfigurationProperty("imgBackground")]
        public string ImgBackground
        {
            get
            {
                return (string)base["imgBackground"];
            }
        }

        [ConfigurationProperty("title")]
        public string Title
        {
            get
            {
                return (string)base["title"];
            }
        }

    }
}

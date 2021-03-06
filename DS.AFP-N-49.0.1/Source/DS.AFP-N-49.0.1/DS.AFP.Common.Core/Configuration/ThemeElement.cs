﻿///<summary>
/// Copyright (c) 2011-2015 上海迪爱斯通信设备有限公司
/// 作  者：姜宁
/// 时  间：2013-11-13 11:13:09
/// 描  述：通信配置元素类
///</summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace DS.AFP.Common.Core.ConfigurationNameSpace
{
    /// <summary>
    /// 通信配置项（主要属性有地址、Key、描述）
    /// <code>
    /// IDsConfigurationSection dsConfig = ConfigurationManager.GetSection("ds/base") as IDsConfigurationSection;
    /// string address = dsConfig.Communications["dsShareData"].Address;
    /// </code>
    /// </summary>
    public class ThemeElement : ConfigurationElement
    {

        [ConfigurationProperty("name",DefaultValue = "default")]
        public string Name
        {
            get
            { return (String)this["name"]; }
            set
            { this["name"] = value; }
        }

        [ConfigurationProperty("culture", DefaultValue = "zh-cn")]
        public string Culture
        {
            get
            { return (String)this["culture"]; }
            set
            { this["culture"] = value; }
        }

        [ConfigurationProperty("description")]
        public string Description
        {
            get
            { return (String)this["description"]; }
            set
            { this["description"] = value; }
        }

        
    }
}

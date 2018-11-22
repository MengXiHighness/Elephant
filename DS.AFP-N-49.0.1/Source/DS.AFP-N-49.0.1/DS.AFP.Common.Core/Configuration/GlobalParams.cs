using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DS.AFP.Common.Core.ConfigurationNameSpace
{
    /// <summary>
    /// 全局参数（Log、Name、Value、DsConfigurationSection）
    /// </summary>
    public class GlobalParams
    {
        public const string Log = "Log";
        public const string Name = "Name";
        public const string Value = "Value";
        public const string DsConfigurationSection = "ds/base";
    }

    /// <summary>
    /// 配置类型（Platform、Addin）
    /// </summary>
    public enum ConfigurationType
    {
        [DescriptionAttribute("Platform")]
        Platform=0,
        [DescriptionAttribute("Addin")]
        Addin=1
    }
}

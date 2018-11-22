using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.AFP.Common.Core.ConfigurationNameSpace
{
    /// <summary>
    /// 插件配置组接口（DsAddinConfigurationSection基类）
    /// </summary>
    public interface IDsAddinConfigurationSection
    {
        ParamsCollection Params { get; }

        bool Sync { get; }

        string Name { get; }

        double Version { get; }

        ThemeCollection Themes { get; }
    }
}

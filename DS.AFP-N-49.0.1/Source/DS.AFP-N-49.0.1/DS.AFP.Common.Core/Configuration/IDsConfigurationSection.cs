using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.AFP.Common.Core.ConfigurationNameSpace
{
    /// <summary>
    /// 平台配置组接口（DsConfigurationSection基类）
    /// </summary>
    public interface IDsConfigurationSection
    {
        ParamsCollection Params { get; }

        ConnectionsCollection Connections { get; }

        CommunicationsCollection Communications { get; }

        ModuleConfigurationElementCollection Modules { get;  }

        ThemeCollection Themes { get; }

        bool Trace { get; }

        string NodeType { get; }

        string Description { get; }

        string Version { get; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.AFP.Common.Core.ConfigurationNameSpace
{
    /// <summary>
    /// UI框架配置
    /// </summary>
    public interface IPortalSection
    {
        StatusBarCollection StatusBar { get; }

        NavigationCollection Navigation { get; }

        ToolBarCollection ToolBar { get; }


        string Logo { get; }

        string ComapnyLogo { get; }

        string ImgBackground { get; }

        string Title { get; }


    }
}

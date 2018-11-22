
namespace DS.AFP.Framework.WPF
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Globalization;

    /// <summary>
    /// 
    /// </summary>
    public interface IResourceProvider
    {
        object GetResource(string nodeName, string key);
    }
}

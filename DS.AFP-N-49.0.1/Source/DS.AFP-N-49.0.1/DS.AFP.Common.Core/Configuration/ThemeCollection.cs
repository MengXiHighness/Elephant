using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace DS.AFP.Common.Core.ConfigurationNameSpace
{
    /// <summary>
    /// 皮肤配置项
    /// <code>
    /// IDsConfigurationSection dsConfig = ConfigurationManager.GetSection("ds/base") as IDsConfigurationSection;
    /// string address = dsConfig.Communications["dsShareData"].Address;
    /// </code>
    /// </summary>
    [ConfigurationCollection(typeof(ThemeElement), AddItemName = "theme")]
    public class ThemeCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ThemeElement();
        }
        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as ThemeElement).Name;
        }
        new public ThemeElement this[string name]
        {
            get { return (ThemeElement)this.BaseGet(name); }
        }
    }
}

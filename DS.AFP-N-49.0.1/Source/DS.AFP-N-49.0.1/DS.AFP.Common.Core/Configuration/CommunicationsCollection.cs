using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace DS.AFP.Common.Core.ConfigurationNameSpace
{
    /// <summary>
    /// 通信配置项集合（CommunicationElement的集合）
    /// <code>
    /// IDsConfigurationSection dsConfig = ConfigurationManager.GetSection("ds/base") as IDsConfigurationSection;
    /// string address = dsConfig.Communications["dsShareData"].Address;
    /// </code>
    /// </summary>
    [ConfigurationCollection(typeof(CommunicationElement), AddItemName = "communication")]
    public class CommunicationsCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new CommunicationElement();
        }
        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as CommunicationElement).Key;
        }
        new public CommunicationElement this[string key]
        {
            get { return (CommunicationElement)this.BaseGet(key); }
        }
    }
}

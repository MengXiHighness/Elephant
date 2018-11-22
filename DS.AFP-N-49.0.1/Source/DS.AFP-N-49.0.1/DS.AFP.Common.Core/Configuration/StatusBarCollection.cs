using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace DS.AFP.Common.Core.ConfigurationNameSpace
{
    [ConfigurationCollection(typeof(StatusBarElement), AddItemName = "statusBarItem")]
    public class StatusBarCollection : ConfigurationElementCollection
    { 
        protected override ConfigurationElement CreateNewElement()
        {
            return new StatusBarElement();
        }
        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as StatusBarElement).Name;
        }
        new public StatusBarElement this[string id]
        {
            get { return (StatusBarElement)this.BaseGet(id); }
        }
    }
}

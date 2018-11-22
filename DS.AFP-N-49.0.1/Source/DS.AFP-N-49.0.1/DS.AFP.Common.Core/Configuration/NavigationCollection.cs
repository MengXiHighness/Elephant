using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace DS.AFP.Common.Core.ConfigurationNameSpace
{
    [ConfigurationCollection(typeof(NavigationElement), AddItemName = "navigationItem")]
    public class NavigationCollection : ConfigurationElementCollection
    { 
        protected override ConfigurationElement CreateNewElement()
        {
            return new NavigationElement();
        }
        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as NavigationElement).Name;
        }
        new public NavigationElement this[string id]
        {
            get { return (NavigationElement)this.BaseGet(id); }
        }
    }
}

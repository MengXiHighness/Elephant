using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace DS.AFP.Common.Core.ConfigurationNameSpace
{
    [ConfigurationCollection(typeof(StatusBarElement), AddItemName = "toolBarItem")]
    public class ToolBarCollection : ConfigurationElementCollection
    { 
        protected override ConfigurationElement CreateNewElement()
        {
            return new ToolBarElement();
        }
        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as ToolBarElement).Name;
        }
        new public ToolBarElement this[string id]
        {
            get { return (ToolBarElement)this.BaseGet(id); }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace DS.AFP.Common.Core.ConfigurationNameSpace
{
    public class logTraceElement : ConfigurationElement
    {

        [ConfigurationProperty("isOpen", DefaultValue = true)]
        public bool isOpen
        {
            get
            { return (bool)this["isOpen"]; }
            set
            { this["isOpen"] = value; }
        }

        [ConfigurationProperty("address", DefaultValue = "http://localhost:4444/LogTraceTool")]
        public string address
        {
            get
            { return (String)this["address"]; }
            set
            { this["address"] = value; }
        }
    }
}

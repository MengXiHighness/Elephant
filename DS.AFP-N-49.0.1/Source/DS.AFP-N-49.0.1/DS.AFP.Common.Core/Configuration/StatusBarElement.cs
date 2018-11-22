using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace DS.AFP.Common.Core.ConfigurationNameSpace
{
    /// <summary>
    /// iconUri="" index="" title="" description=""
    /// </summary>
    public class StatusBarElement : ConfigurationElement
    { 
        [ConfigurationProperty("iconUri")]
        public string IconUri
        {
            get
            { return (String)this["iconUri"]; }
            set
            { this["iconUri"] = value; }
        }

        [ConfigurationProperty("index")]
        public int Index
        {
            get
            { return (int)this["index"]; }
            set
            { this["index"] = value; }
        }

        [ConfigurationProperty("title")]
        public string Title
        {
            get
            { return (string)this["title"]; }
            set
            { this["title"] = value; }
        }

        [ConfigurationProperty("description")]
        public string Description
        {
            get
            { return (string)this["description"]; }
            set
            { this["description"] = value; }
        }

        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get
            { return (string)this["name"]; }
            set
            { this["name"] = value; }
        }
    }
}

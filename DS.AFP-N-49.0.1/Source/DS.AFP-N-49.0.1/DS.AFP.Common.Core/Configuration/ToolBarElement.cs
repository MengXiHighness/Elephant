using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace DS.AFP.Common.Core.ConfigurationNameSpace
{
    /// <summary>
    ///  iconUri="" openStyle="" openUri=""  title="" index=""
    /// </summary>
    public class ToolBarElement : ConfigurationElement
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

        [ConfigurationProperty("parentIndex",DefaultValue = "0")]
        public int ParentIndex
        {
            get
            { return (int)this["parentIndex"]; }
            set
            { this["parentIndex"] = value; }
        }

        [ConfigurationProperty("title")]
        public string Title
        {
            get
            { return (string)this["title"]; }
            set
            { this["title"] = value; }
        }

        [ConfigurationProperty("openStyle")]
        public string OpenStyle
        {
            get
            { return (string)this["openStyle"]; }
            set
            { this["openStyle"] = value; }
        }

        [ConfigurationProperty("openUri")]
        public string OpenUri
        {
            get
            { return (string)this["openUri"]; }
            set
            { this["openUri"] = value; }
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

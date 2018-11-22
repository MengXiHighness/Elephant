using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace DS.AFP.Common.Core.ConfigurationNameSpace
{
    /// <summary>
    /// 参数配置元素（用于参数配置）
    /// <code>
    /// return (element as ParamElement).Key
    /// </code>
    /// </summary>
    public class ParamElement : ConfigurationElement
    {

        [ConfigurationProperty("value",DefaultValue = "")]
        public string Value
        {
            get
            { return (String)this["value"]; }
            set
            { this["value"] = value; }
        }

        [ConfigurationProperty("key")]
        public string Key
        {
            get
            { return (String)this["key"]; }
            set
            { this["key"] = value; }
        }

        [ConfigurationProperty("description", DefaultValue = "")]
        public string Description
        {
            get
            { return (String)this["description"]; }
            set
            { this["description"] = value; }
        }

        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace DS.AFP.Common.Core.ConfigurationNameSpace
{
    /// <summary>
    /// 参数配置集合类（ParamElement集合）
    /// <code>
    /// ParamsCollection Params { get; }
    /// </code>
    /// </summary>
    [ConfigurationCollection(typeof(CommunicationElement), AddItemName = "param")]
    public class ParamsCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ParamElement();
        }
        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as ParamElement).Key;
        }
        new public ParamElement this[string key]
        {
            get { return (ParamElement)this.BaseGet(key); }
        }
    }
}

///<summary>
/// Copyright (c) 2011-2015 上海迪爱斯通信设备有限公司
/// 作  者：姜宁
/// 时  间：2013-8-13 11:13:09
/// 描  述：数据库配置集合类
///</summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace DS.AFP.Common.Core.ConfigurationNameSpace
{
    /// <summary>
    /// 数据库连接配置集合（ConnectionElement集合）
    /// <code>
    /// ConnectionElement ConnectionElement = new DS.AFP.Common.Core.ConfigurationNameSpace.DsConfigurationManager().DsRootConfigurationSection.Connections[key];
    /// </code>
    /// </summary>
    [ConfigurationCollection(typeof(ConnectionElement), AddItemName = "connection")] 
    public class ConnectionsCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ConnectionElement();
        }
        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as ConnectionElement).ID;
        }
        new public ConnectionElement this[string id]
        {
            get { return (ConnectionElement)this.BaseGet(id); }
        }
    }
}

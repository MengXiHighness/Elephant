///<summary>
/// Copyright (c) 2011-2015 上海迪爱斯通信设备有限公司
/// 作  者：姜宁
/// 时  间：2013-11-13 11:13:09
/// 描  述：XML扩展类
///</summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace DS.AFP.Common.Core
{
    /// <summary>
    /// xml的扩展（主要功能：获取指定XmlNode的指定属性）
    /// </summary>
    public static class XmlExtensioins
    {
        /// <summary>
        /// 获取指定XmlNode的指定属性
        /// </summary>
        /// <param name="xmlNode">指定的XML 文档中的节点</param>
        /// <param name="attrName">属性名称</param>
        /// <returns>指定XmlNode的指定属性值</returns>
        public static XmlAttribute GetAttribute(this XmlNode xmlNode,string attrName)
        {
            return xmlNode.Attributes[attrName];
        }
    }
}

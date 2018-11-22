
using System;
using System.Xml;

using Spring.Util;
using Spring.Objects.Factory.Xml;
using Spring.Objects.Factory.Config;
using Spring.Objects.Factory.Support;
using Spring.Core.TypeResolution;
using Spring.Objects;
using DS.AFP.Common.Core.ConfigurationNameSpace;
using DS.AFP.Common.Core;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using DS.AFP.Common.Core.Utility;
using RestSharp;

namespace DS.AFP.Communication.Rest.Config
{

    /// <summary>
    /// 配置文件解析器
    /// </summary>
    public class RestFactoryObjectDefinitionParser : ObjectsNamespaceParser, IObjectDefinitionParser
    {
        private static readonly string HostBaseUriAttribute = "hostBaseUri";
        private static readonly string TimeOutAttribute = "timeOut";

        #region IObjectDefinitionParser Members

        private Type channelType = null;

        IObjectDefinition IObjectDefinitionParser.ParseElement(XmlElement element, ParserContext parserContext)
        {
            AssertUtils.ArgumentNotNull(parserContext, "parserContext");

            string id = element.GetAttribute(ObjectDefinitionConstants.IdAttribute);
            string hostBaseUri = element.GetAttribute(HostBaseUriAttribute);
            string timeout = element.GetAttribute(TimeOutAttribute);
            if (hostBaseUri.IsNullOrEmpty())
            {
                Console.WriteLine("创建Rest客户端实例异常：没有填写hostBaseUri配置");
                Exception ex = new Exception("创建Rest客户端实例异常：没有填写hostBaseUri配置");
                throw ex;
            }
            ConstructorArgumentValues cav = new ConstructorArgumentValues();
            cav.AddNamedArgumentValue("baseUrl", hostBaseUri);
            MutablePropertyValues mpv = new MutablePropertyValues();
            mpv.Add("TimeOut", timeout);
            RootObjectDefinition rod = new RootObjectDefinition(typeof(RestClient), cav, mpv);
            parserContext.Registry.RegisterObjectDefinition(id, rod);
            return null;
        }

        #endregion


    }
}


using System;
using System.Xml;

using Spring.Util;
using Spring.Objects.Factory.Xml;
using Spring.Objects.Factory.Config;
using Spring.Objects.Factory.Support;
using Spring.Core.TypeResolution;
using Spring.Objects;
using Spring.ServiceModel;
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


namespace DS.AFP.Communication.WCF.Config
{

    /// <summary>
    /// 通道工厂定义分析器
    /// </summary>
    public class DuplexChannelFactoryObjectDefinitionParser : ObjectsNamespaceParser, IObjectDefinitionParser
    {
        private static readonly string ChannelTypeAttribute = "channelType";
        private static readonly string EndpointConfigurationNameAttribute = "endpointConfigurationName";
        private static readonly string ConfigNameAttribute = "configFile";
        private static readonly string CallBackNameAttribute = "callBack";

        #region IObjectDefinitionParser Members

        private Type channelType = null;
      
        IObjectDefinition IObjectDefinitionParser.ParseElement(XmlElement element, ParserContext parserContext)
        {
            AssertUtils.ArgumentNotNull(parserContext, "parserContext");

            string id = element.GetAttribute(ObjectDefinitionConstants.IdAttribute);
            string unresolvedChannelType = element.GetAttribute(ChannelTypeAttribute);
            string endpointConfigurationName = element.GetAttribute(EndpointConfigurationNameAttribute);
            string configFile = element.GetAttribute(ConfigNameAttribute).ToLower();
            string callBackFile = element.GetAttribute(CallBackNameAttribute);
            IObjectDefinition channelFactoryDefinition;
            ServiceEndpoint clientEndpoint = null;
            try
            {
                clientEndpoint = GetServiceEndpointByConfig(configFile, endpointConfigurationName);
                if (channelType == null)
                    throw new Exception("ChannelType equal to null, the configuration file is correct");
                Type channelFactoryType = typeof(DuplexChannelFactoryObject<>).MakeGenericType(new Type[1] { channelType });
                channelFactoryDefinition = new RootObjectDefinition(channelFactoryType);
            }
            catch(Exception e)
            {
                channelFactoryDefinition = new RootObjectDefinition(
                    String.Format("Spring.ServiceModel.DuplexChannelFactoryObject<{0}>, Spring.Services", unresolvedChannelType), 
                    new ConstructorArgumentValues(), 
                    new MutablePropertyValues());
                throw e;
            }

            if (!StringUtils.HasText(id))
            {
                id = parserContext.ReaderContext.GenerateObjectName(channelFactoryDefinition);
            }
            InstanceContext instanceContext = new InstanceContext(GetServiceCallBack(callBackFile));
            if (instanceContext != null)
                channelFactoryDefinition.ConstructorArgumentValues.AddNamedArgumentValue("instanceContext", instanceContext);

            if (clientEndpoint!=null)
                channelFactoryDefinition.ConstructorArgumentValues.AddNamedArgumentValue("serviceEndpoint", clientEndpoint);

            foreach (PropertyValue pv in base.ParsePropertyElements(id, element, parserContext))
            {
                channelFactoryDefinition.PropertyValues.Add(pv);
            }

            parserContext.Registry.RegisterObjectDefinition(id, channelFactoryDefinition);
            return null;
        }

        #endregion

        private ServiceEndpoint GetServiceEndpointByConfig(string configName, string endpointConfigurationName)
        {
            ServiceEndpoint serviceEndpoint = null;
            DsConfigurationManager dcm = new DsConfigurationManager();
            System.Configuration.Configuration config = dcm.Get<System.Configuration.Configuration>(ConfigurationFileHelper.GetAddinConfigurationFilePath(configName));
            ClientSection cconfig = config.GetSection(GlobalParams.ClientHostSession) as ClientSection;
            if (cconfig != null)
            {
                if (cconfig.Endpoints.Count > 0)
                {
                    foreach (ChannelEndpointElement e in cconfig.Endpoints)
                    {
                        if (e.Name != endpointConfigurationName)
                            continue;
                        channelType = TypeResolutionUtils.ResolveType(e.Contract);
                        serviceEndpoint = new ServiceEndpoint(ContractDescription.GetContract(channelType), WCFMateHelper.BindingFactory(config, e), new EndpointAddress(e.Address));
                    }
                }
            }
            if (serviceEndpoint == null)
                throw new ArgumentNullException("The configuration file{0}，The client access to WCF configuration information is not correct".FormatString(configName));
            return serviceEndpoint;
        }

        private object GetServiceCallBack(string callBackFile)
        {
            object obj = null;
            if (callBackFile != null)
            {
                   Type callBackType = TypeResolutionUtils.ResolveType(callBackFile);
                   if (callBackFile.IndexOf(',') != -1)
                       callBackFile = callBackFile.Split(",")[0];
                   obj = callBackType.Assembly.CreateInstance(callBackFile); 

            }
            if (obj == null)
                throw new ArgumentNullException("The configuration file{0}，The client access to WCF configuration information is not correct".FormatString(callBackFile));
            return obj;
        }

        private ServiceEndpoint GetServiceEndpointByConfig(string configName, string endpointConfigurationName, Type channelType)
        {
            ServiceEndpoint serviceEndpoint = null;
            DsConfigurationManager dcm = new DsConfigurationManager();
            System.Configuration.Configuration config = dcm.Get<System.Configuration.Configuration>(ConfigurationFileHelper.GetAddinConfigurationFilePath(configName));
            ClientSection cconfig = config.GetSection(GlobalParams.ClientHostSession) as ClientSection;
            if (cconfig != null)
            {
                if (cconfig.Endpoints.Count > 0)
                {
                    foreach (ChannelEndpointElement e in cconfig.Endpoints)
                    {
                        if (e.Name != endpointConfigurationName)
                            continue;
                        serviceEndpoint = new ServiceEndpoint(ContractDescription.GetContract(channelType), WCFMateHelper.BindingFactory(config, e), new EndpointAddress(e.Address));
                    }
                }
            }
            if (serviceEndpoint == null)
                throw new ArgumentNullException("The configuration file{0}，The client access to WCF configuration information is not correct".FormatString(configName));
            return serviceEndpoint;
        }

        
    }
}

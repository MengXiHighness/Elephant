using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.Text;
using DS.AFP.Common.Core;
using System.ServiceModel.Description;
using System.Configuration;
using Spring.ServiceModel;

namespace DS.AFP.Communication.WCF
{
    /// <summary>
    /// 提供对WCF元数据的使用
    /// </summary>
    internal class WCFMateHelper
    {
        /// <summary>
        /// 生成Binding对象
        /// <code>
        /// WCFMateHelper.BindingFactory(config, e) 
        /// </code>
        /// </summary>
        /// <param name="config"></param>
        /// <param name="chanelEndpoint"></param>
        /// <returns></returns>
        public static Binding BindingFactory(System.Configuration.Configuration config, ChannelEndpointElement chanelEndpoint)
        {
            BindingsSection bindings = config.GetSection("system.serviceModel/bindings") as BindingsSection;

            BindingCollectionElement bc = bindings[chanelEndpoint.Binding];
            //chanelEndpoint.
            if (chanelEndpoint.Binding != "")
            {
                switch (chanelEndpoint.Binding.ToLower())
                {
                    case "nettcpbinding":
                        {
                            NetTcpBinding ntb = new NetTcpBinding();
                            NetTcpBindingElement bce = bc.ConfiguredBindings.FirstOrDefault(o => o.Name == chanelEndpoint.BindingConfiguration) as NetTcpBindingElement;
                            if (bce != null)
                            {
                                ntb.CloseTimeout = bce.CloseTimeout;
                                ntb.OpenTimeout = bce.OpenTimeout;
                                ntb.ReceiveTimeout = bce.ReceiveTimeout;
                                ntb.SendTimeout = bce.SendTimeout;
                                ntb.MaxBufferPoolSize = bce.MaxBufferPoolSize;
                                ntb.HostNameComparisonMode = bce.HostNameComparisonMode;
                                ntb.ListenBacklog = (bce.ListenBacklog != 0 ? bce.ListenBacklog : ntb.ListenBacklog);
                                ntb.MaxBufferSize = bce.MaxBufferSize;
                                ntb.MaxConnections = (bce.MaxConnections == 0 ? ntb.MaxConnections : bce.MaxConnections);
                                ntb.MaxReceivedMessageSize = bce.MaxReceivedMessageSize;
                                ntb.PortSharingEnabled = bce.PortSharingEnabled;
                                ntb.ReaderQuotas.MaxArrayLength = (bce.ReaderQuotas.MaxArrayLength != 0 ? bce.ReaderQuotas.MaxArrayLength : ntb.ReaderQuotas.MaxArrayLength);
                                ntb.ReaderQuotas.MaxDepth = (bce.ReaderQuotas.MaxDepth != 0 ? bce.ReaderQuotas.MaxDepth : ntb.ReaderQuotas.MaxDepth);
                                ntb.ReaderQuotas.MaxBytesPerRead = (bce.ReaderQuotas.MaxBytesPerRead != 0 ? bce.ReaderQuotas.MaxBytesPerRead : ntb.ReaderQuotas.MaxBytesPerRead);
                                ntb.ReaderQuotas.MaxNameTableCharCount = (bce.ReaderQuotas.MaxNameTableCharCount != 0 ? bce.ReaderQuotas.MaxNameTableCharCount : ntb.ReaderQuotas.MaxNameTableCharCount);
                                ntb.ReaderQuotas.MaxStringContentLength = (bce.ReaderQuotas.MaxStringContentLength != 0 ? bce.ReaderQuotas.MaxStringContentLength : ntb.ReaderQuotas.MaxStringContentLength);
                                ntb.ReliableSession = new OptionalReliableSession() { Enabled = bce.ReliableSession.Enabled, InactivityTimeout = bce.ReliableSession.InactivityTimeout, Ordered = bce.ReliableSession.Ordered };
                                ntb.Security = new NetTcpSecurity() { Mode = SecurityMode.None };
                                ntb.TransactionFlow = bce.TransactionFlow;
                                ntb.TransactionProtocol = bce.TransactionProtocol;
                                ntb.TransferMode = bce.TransferMode;
                            }
                            return ntb;
                        }
                    case "basichttpbinding":
                        {
                            BasicHttpBinding bhb = new BasicHttpBinding(BasicHttpSecurityMode.None);
                            BasicHttpBindingElement bce = bc.ConfiguredBindings.FirstOrDefault(o => o.Name == chanelEndpoint.BindingConfiguration) as BasicHttpBindingElement;
                            if (bce != null)
                            {
                                bhb.AllowCookies = bce.AllowCookies;
                                bhb.BypassProxyOnLocal = bce.BypassProxyOnLocal;
                                bhb.CloseTimeout = bce.CloseTimeout;
                                bhb.HostNameComparisonMode = bce.HostNameComparisonMode;
                                bhb.MaxBufferPoolSize = bce.MaxBufferPoolSize;
                                bhb.MaxBufferSize = bce.MaxBufferSize;
                                bhb.MaxReceivedMessageSize = bce.MaxReceivedMessageSize;
                                bhb.MessageEncoding = bce.MessageEncoding;
                                bhb.OpenTimeout = bce.OpenTimeout;
                                bhb.ProxyAddress = bce.ProxyAddress;
                                bhb.ReaderQuotas.MaxArrayLength = (bce.ReaderQuotas.MaxArrayLength != 0 ? bce.ReaderQuotas.MaxArrayLength : bhb.ReaderQuotas.MaxArrayLength);
                                bhb.ReaderQuotas.MaxDepth = (bce.ReaderQuotas.MaxDepth != 0 ? bce.ReaderQuotas.MaxDepth : bhb.ReaderQuotas.MaxDepth);
                                bhb.ReaderQuotas.MaxBytesPerRead = (bce.ReaderQuotas.MaxBytesPerRead != 0 ? bce.ReaderQuotas.MaxBytesPerRead : bhb.ReaderQuotas.MaxBytesPerRead);
                                bhb.ReaderQuotas.MaxNameTableCharCount = (bce.ReaderQuotas.MaxNameTableCharCount != 0 ? bce.ReaderQuotas.MaxNameTableCharCount : bhb.ReaderQuotas.MaxNameTableCharCount);
                                bhb.ReaderQuotas.MaxStringContentLength = (bce.ReaderQuotas.MaxStringContentLength != 0 ? bce.ReaderQuotas.MaxStringContentLength : bhb.ReaderQuotas.MaxStringContentLength);
                                bhb.ReceiveTimeout = bce.ReceiveTimeout;
                                bhb.SendTimeout = bce.SendTimeout;
                                bhb.TextEncoding = bce.TextEncoding;
                                bhb.TransferMode = bce.TransferMode;
                                bhb.UseDefaultWebProxy = bce.UseDefaultWebProxy;
                            }
                            return bhb;
                        }
                    case "wshttpbinding":
                        {
                            WSHttpBinding bhb = new WSHttpBinding(SecurityMode.None);
                            WSHttpBindingElement bce = bc.ConfiguredBindings.FirstOrDefault(o => o.Name == chanelEndpoint.BindingConfiguration) as WSHttpBindingElement;
                            if (bce != null)
                            {
                                bhb.AllowCookies = bce.AllowCookies;
                                bhb.BypassProxyOnLocal = bce.BypassProxyOnLocal;
                                bhb.CloseTimeout = bce.CloseTimeout;
                                bhb.HostNameComparisonMode = bce.HostNameComparisonMode;
                                bhb.MaxBufferPoolSize = bce.MaxBufferPoolSize;
                                bhb.MaxReceivedMessageSize = bce.MaxReceivedMessageSize;
                                bhb.MessageEncoding = bce.MessageEncoding;
                                bhb.OpenTimeout = bce.OpenTimeout;
                                bhb.ProxyAddress = bce.ProxyAddress;
                                bhb.ReaderQuotas.MaxArrayLength = (bce.ReaderQuotas.MaxArrayLength != 0 ? bce.ReaderQuotas.MaxArrayLength : bhb.ReaderQuotas.MaxArrayLength);
                                bhb.ReaderQuotas.MaxDepth = (bce.ReaderQuotas.MaxDepth != 0 ? bce.ReaderQuotas.MaxDepth : bhb.ReaderQuotas.MaxDepth);
                                bhb.ReaderQuotas.MaxBytesPerRead = (bce.ReaderQuotas.MaxBytesPerRead != 0 ? bce.ReaderQuotas.MaxBytesPerRead : bhb.ReaderQuotas.MaxBytesPerRead);
                                bhb.ReaderQuotas.MaxNameTableCharCount = (bce.ReaderQuotas.MaxNameTableCharCount != 0 ? bce.ReaderQuotas.MaxNameTableCharCount : bhb.ReaderQuotas.MaxNameTableCharCount);
                                bhb.ReaderQuotas.MaxStringContentLength = (bce.ReaderQuotas.MaxStringContentLength != 0 ? bce.ReaderQuotas.MaxStringContentLength : bhb.ReaderQuotas.MaxStringContentLength);
                                bhb.ReceiveTimeout = bce.ReceiveTimeout;
                                bhb.SendTimeout = bce.SendTimeout;
                                bhb.TextEncoding = bce.TextEncoding;
                                bhb.TransactionFlow = bce.TransactionFlow;
                                bhb.UseDefaultWebProxy = bce.UseDefaultWebProxy;
                            }
                            return bhb;
                        }
                }
            }

            throw new BindingNotFoundException(Resources.BindingNotFoundException);
        }

        /// <summary>
        /// 生成Binding对象
        /// <code>
        /// WCFMateHelper.BindingFactory(serviceMeta,see)
        /// </code>
        /// </summary>
        /// <param name="wCFServiceMeta"></param>
        /// <param name="serviceEndpoint"></param>
        /// <returns></returns>
        public static Binding BindingFactory(WCFServiceMeta wCFServiceMeta,ServiceEndpointElement serviceEndpoint)
        {
            BindingsSection bindings = wCFServiceMeta.ChildConfiguration.GetSection("system.serviceModel/bindings") as BindingsSection;
            BindingCollectionElement bc = bindings[serviceEndpoint.Binding];
            switch (bc.BindingName.ToLower())
            {
                case "nettcpbinding":
                    {
                        NetTcpBinding ntb = new NetTcpBinding();
                        NetTcpBindingElement bce = bc.ConfiguredBindings.FirstOrDefault(o => o.Name == serviceEndpoint.BindingConfiguration) as NetTcpBindingElement;
                        if (bce != null)
                        {
                            ntb.CloseTimeout = bce.CloseTimeout;
                            ntb.OpenTimeout = bce.OpenTimeout;
                            ntb.ReceiveTimeout = bce.ReceiveTimeout;
                            ntb.SendTimeout = bce.SendTimeout;
                            ntb.MaxBufferPoolSize = bce.MaxBufferPoolSize;
                            ntb.HostNameComparisonMode = bce.HostNameComparisonMode;
                            ntb.ListenBacklog = (bce.ListenBacklog != 0 ? bce.ListenBacklog : ntb.ListenBacklog);
                            ntb.MaxBufferSize = bce.MaxBufferSize;
                            ntb.MaxConnections = (bce.MaxConnections == 0 ? ntb.MaxConnections : bce.MaxConnections);
                            ntb.MaxReceivedMessageSize = bce.MaxReceivedMessageSize;
                            ntb.PortSharingEnabled = bce.PortSharingEnabled;
                            ntb.ReaderQuotas.MaxArrayLength = (bce.ReaderQuotas.MaxArrayLength != 0 ? bce.ReaderQuotas.MaxArrayLength : ntb.ReaderQuotas.MaxArrayLength);
                            ntb.ReaderQuotas.MaxDepth = (bce.ReaderQuotas.MaxDepth != 0 ? bce.ReaderQuotas.MaxDepth : ntb.ReaderQuotas.MaxDepth);
                            ntb.ReaderQuotas.MaxBytesPerRead = (bce.ReaderQuotas.MaxBytesPerRead != 0 ? bce.ReaderQuotas.MaxBytesPerRead : ntb.ReaderQuotas.MaxBytesPerRead);
                            ntb.ReaderQuotas.MaxNameTableCharCount = (bce.ReaderQuotas.MaxNameTableCharCount != 0 ? bce.ReaderQuotas.MaxNameTableCharCount : ntb.ReaderQuotas.MaxNameTableCharCount);
                            ntb.ReaderQuotas.MaxStringContentLength = (bce.ReaderQuotas.MaxStringContentLength != 0 ? bce.ReaderQuotas.MaxStringContentLength : ntb.ReaderQuotas.MaxStringContentLength);
                            ntb.ReliableSession = new OptionalReliableSession() { Enabled = bce.ReliableSession.Enabled, InactivityTimeout = bce.ReliableSession.InactivityTimeout, Ordered = bce.ReliableSession.Ordered };
                            ntb.Security = new NetTcpSecurity() { Mode = SecurityMode.None };
                            ntb.TransactionFlow = bce.TransactionFlow;
                            ntb.TransactionProtocol = bce.TransactionProtocol;
                            ntb.TransferMode = bce.TransferMode;
                        }
                        return ntb;
                    }
                case "basichttpbinding":
                    {
                        BasicHttpBinding bhb = new BasicHttpBinding(BasicHttpSecurityMode.None);
                        BasicHttpBindingElement bce = bc.ConfiguredBindings.FirstOrDefault(o => o.Name == serviceEndpoint.BindingConfiguration) as BasicHttpBindingElement;
                        if (bce != null)
                        {
                            bhb.AllowCookies = bce.AllowCookies;
                            bhb.BypassProxyOnLocal = bce.BypassProxyOnLocal;
                            bhb.CloseTimeout = bce.CloseTimeout;
                            bhb.HostNameComparisonMode = bce.HostNameComparisonMode;
                            bhb.MaxBufferPoolSize = bce.MaxBufferPoolSize;
                            bhb.MaxBufferSize = bce.MaxBufferSize;
                            bhb.MaxReceivedMessageSize = bce.MaxReceivedMessageSize;
                            bhb.MessageEncoding = bce.MessageEncoding;
                            bhb.OpenTimeout = bce.OpenTimeout;
                            bhb.ProxyAddress = bce.ProxyAddress;
                            bhb.ReaderQuotas.MaxArrayLength = (bce.ReaderQuotas.MaxArrayLength != 0 ? bce.ReaderQuotas.MaxArrayLength : bhb.ReaderQuotas.MaxArrayLength);
                            bhb.ReaderQuotas.MaxDepth = (bce.ReaderQuotas.MaxDepth != 0 ? bce.ReaderQuotas.MaxDepth : bhb.ReaderQuotas.MaxDepth);
                            bhb.ReaderQuotas.MaxBytesPerRead = (bce.ReaderQuotas.MaxBytesPerRead != 0 ? bce.ReaderQuotas.MaxBytesPerRead : bhb.ReaderQuotas.MaxBytesPerRead);
                            bhb.ReaderQuotas.MaxNameTableCharCount = (bce.ReaderQuotas.MaxNameTableCharCount != 0 ? bce.ReaderQuotas.MaxNameTableCharCount : bhb.ReaderQuotas.MaxNameTableCharCount);
                            bhb.ReaderQuotas.MaxStringContentLength = (bce.ReaderQuotas.MaxStringContentLength != 0 ? bce.ReaderQuotas.MaxStringContentLength : bhb.ReaderQuotas.MaxStringContentLength);
                            bhb.ReceiveTimeout = bce.ReceiveTimeout;
                            bhb.SendTimeout = bce.SendTimeout;
                            bhb.TextEncoding = bce.TextEncoding;
                            bhb.TransferMode = bce.TransferMode;
                            bhb.UseDefaultWebProxy = bce.UseDefaultWebProxy;
                        }
                        return bhb;
                    }
                case "webhttpbinding":
                    {
                        WebHttpBinding bhb = new WebHttpBinding(WebHttpSecurityMode.None);
                        bhb.CrossDomainScriptAccessEnabled = true;
                        WebHttpBindingElement bce = bc.ConfiguredBindings.FirstOrDefault(o => o.Name == serviceEndpoint.BindingConfiguration) as WebHttpBindingElement;
                        if (bce != null)
                        {
                            bhb.AllowCookies = bce.AllowCookies;
                            bhb.BypassProxyOnLocal = bce.BypassProxyOnLocal;
                            bhb.CloseTimeout = bce.CloseTimeout;
                            bhb.HostNameComparisonMode = bce.HostNameComparisonMode;
                            bhb.MaxBufferPoolSize = bce.MaxBufferPoolSize;
                            bhb.MaxBufferSize = bce.MaxBufferSize;
                            bhb.MaxReceivedMessageSize = bce.MaxReceivedMessageSize;
                            //bhb.MessageVersion = bce.m.MessageEncoding;
                            bhb.OpenTimeout = bce.OpenTimeout;
                            bhb.ProxyAddress = bce.ProxyAddress;
                            bhb.ReaderQuotas.MaxArrayLength = (bce.ReaderQuotas.MaxArrayLength != 0 ? bce.ReaderQuotas.MaxArrayLength : bhb.ReaderQuotas.MaxArrayLength);
                            bhb.ReaderQuotas.MaxDepth = (bce.ReaderQuotas.MaxDepth != 0 ? bce.ReaderQuotas.MaxDepth : bhb.ReaderQuotas.MaxDepth);
                            bhb.ReaderQuotas.MaxBytesPerRead = (bce.ReaderQuotas.MaxBytesPerRead != 0 ? bce.ReaderQuotas.MaxBytesPerRead : bhb.ReaderQuotas.MaxBytesPerRead);
                            bhb.ReaderQuotas.MaxNameTableCharCount = (bce.ReaderQuotas.MaxNameTableCharCount != 0 ? bce.ReaderQuotas.MaxNameTableCharCount : bhb.ReaderQuotas.MaxNameTableCharCount);
                            bhb.ReaderQuotas.MaxStringContentLength = (bce.ReaderQuotas.MaxStringContentLength != 0 ? bce.ReaderQuotas.MaxStringContentLength : bhb.ReaderQuotas.MaxStringContentLength);
                            bhb.ReceiveTimeout = bce.ReceiveTimeout;
                            bhb.SendTimeout = bce.SendTimeout;
                            bhb.Name = bce.Name;
                            bhb.TransferMode = bce.TransferMode;
                            bhb.UseDefaultWebProxy = bce.UseDefaultWebProxy;
                        }
                        return bhb;
                    }
                case "wshttpbinding":
                    {
                        WSHttpBinding bhb = new WSHttpBinding(SecurityMode.None);
                        WSHttpBindingElement bce = bc.ConfiguredBindings.FirstOrDefault(o => o.Name == serviceEndpoint.BindingConfiguration) as WSHttpBindingElement;
                        if (bce != null)
                        {
                            bhb.AllowCookies = bce.AllowCookies;
                            bhb.BypassProxyOnLocal = bce.BypassProxyOnLocal;
                            bhb.CloseTimeout = bce.CloseTimeout;
                            bhb.HostNameComparisonMode = bce.HostNameComparisonMode;
                            bhb.MaxBufferPoolSize = bce.MaxBufferPoolSize;
                            bhb.MaxReceivedMessageSize = bce.MaxReceivedMessageSize;
                            bhb.MessageEncoding = bce.MessageEncoding;
                            bhb.OpenTimeout = bce.OpenTimeout;
                            bhb.ProxyAddress = bce.ProxyAddress;
                            bhb.ReaderQuotas.MaxArrayLength = (bce.ReaderQuotas.MaxArrayLength != 0 ? bce.ReaderQuotas.MaxArrayLength : bhb.ReaderQuotas.MaxArrayLength);
                            bhb.ReaderQuotas.MaxDepth = (bce.ReaderQuotas.MaxDepth != 0 ? bce.ReaderQuotas.MaxDepth : bhb.ReaderQuotas.MaxDepth);
                            bhb.ReaderQuotas.MaxBytesPerRead = (bce.ReaderQuotas.MaxBytesPerRead != 0 ? bce.ReaderQuotas.MaxBytesPerRead : bhb.ReaderQuotas.MaxBytesPerRead);
                            bhb.ReaderQuotas.MaxNameTableCharCount = (bce.ReaderQuotas.MaxNameTableCharCount != 0 ? bce.ReaderQuotas.MaxNameTableCharCount : bhb.ReaderQuotas.MaxNameTableCharCount);
                            bhb.ReaderQuotas.MaxStringContentLength = (bce.ReaderQuotas.MaxStringContentLength != 0 ? bce.ReaderQuotas.MaxStringContentLength : bhb.ReaderQuotas.MaxStringContentLength);
                            bhb.ReceiveTimeout = bce.ReceiveTimeout;
                            bhb.SendTimeout = bce.SendTimeout;
                            bhb.TextEncoding = bce.TextEncoding;
                            bhb.TransactionFlow = bce.TransactionFlow;
                            bhb.UseDefaultWebProxy = bce.UseDefaultWebProxy;
                        }
                        return bhb;
                    }
                
            }

            throw new BindingNotFoundException(Resources.BindingNotFoundException);
        }



        public static void BuildingServiceaBehavior(WCFServiceMeta wCFServiceMeta,ServiceElement serviceElement,SpringServiceHost ssh)
        {
            ServiceBehaviorElement sbe = null;
            if (serviceElement.BehaviorConfiguration!="" && wCFServiceMeta.BehaviorsConfiguration != null)
            {
                if (wCFServiceMeta.BehaviorsConfiguration.ServiceBehaviors.ContainsKey(serviceElement.BehaviorConfiguration))
                {
                    ServiceBehaviorElementCollection sbec = wCFServiceMeta.BehaviorsConfiguration.ServiceBehaviors;
                    foreach(ServiceBehaviorElement o in sbec)
                    {
                        if (o.Name == serviceElement.BehaviorConfiguration)
                        {
                            sbe = o;
                            break;
                        }
                    }
                    if(sbe!=null)
                    {
                        
                        //ServiceBehavior smb = new ServiceMetadataBehavior();
                        foreach(var metadata in sbe)
                        {
                            switch (metadata.GetType().FullName)
                            {
                                case "System.ServiceModel.Configuration.DataContractSerializerElement":
                                    {
                                        DataContractSerializerElement dse = metadata as DataContractSerializerElement;
                                        if(dse!=null)
                                        {
                                            int i = dse.MaxItemsInObjectGraph;

                                            ContractDescription cd = ssh.Description.Endpoints.FirstOrDefault(o=>o.Name!="IMetadataExchange").Contract;
                                            OperationDescriptionCollection opdc = cd.Operations;
                                            foreach(OperationDescription odp in opdc)
                                            {
                                                DataContractSerializerOperationBehavior  dsb = new DataContractSerializerOperationBehavior(odp);
                                                dsb.IgnoreExtensionDataObject = dse.IgnoreExtensionDataObject;
                                                dsb.MaxItemsInObjectGraph = dse.MaxItemsInObjectGraph;
                                                odp.Behaviors.Remove<DataContractSerializerOperationBehavior>();
                                                odp.Behaviors.Add(dsb);
                                            }
                                            
                                            return ;
                                        }
                                        break;
                                    }
                               
                            }
                        }
                        
                    }

                }
            }
           
        }
    }
}

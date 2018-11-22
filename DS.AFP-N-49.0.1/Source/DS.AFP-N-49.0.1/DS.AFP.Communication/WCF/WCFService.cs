using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.Text;
using Spring.Context;
using Spring.Context.Support;
using Spring.ServiceModel;
using DS.AFP.Common.Core;
using System.ServiceModel.Web;

namespace DS.AFP.Communication.WCF
{
    /// <summary>
    /// WCF 服务容器
    /// </summary>
    public class WCFService
    {
        private WCFServiceMeta WCFServiceMeta { get; set; }

       
        /// <summary>
        /// 根据WCF元数据创建SpringServiceHost列表
        /// <code>
        /// WCFService wcfservice = new WCFService();
        /// _container.Add(wm, wcfservice.Builder(wm));
        /// </code>
        /// </summary>
        /// <param name="serviceMeta">WCF元数据</param>
        /// <returns>SpringServiceHost列表</returns>
        public List<SpringServiceHost> Builder(WCFServiceMeta serviceMeta)
        {
            WCFServiceMeta = serviceMeta;
            List<SpringServiceHost> ssh = new List<SpringServiceHost>();
            ServiceEndpointElement metaServiceEndpoint = null;
            try
            {

                foreach (ServiceElement service in serviceMeta.ServicesConfiguration.Services)
                {
                    List<Uri> baseUris = new List<Uri>();
                    foreach (BaseAddressElement ba in service.Host.BaseAddresses)
                    {
                        baseUris.Add(new Uri(ba.BaseAddress));
                    }
                   
                    //ServiceHost sh = new System.ServiceModel.ServiceHost(CreateContactType(service.Name), baseUris.ToArray());
                    //IApplicationContext c = ContextRegistry.GetContext(serviceMeta.ContextName);
                    SpringWebServiceHost sh2 = null;
                    SpringServiceHost sh = null;// new SpringServiceHost(service.Name, serviceMeta.ContextName, baseUris.ToArray());
                  // SpringWebServiceHost sh = new WebServiceHost()

                    foreach (ServiceEndpointElement see in service.Endpoints)
                    {
                        Type contactType;
                        if (see.Contract == "IMetadataExchange")
                        {
                            //contactType = typeof(IMetadataExchange);
                            metaServiceEndpoint = see;
                            continue;
                        }
                        else
                            contactType = CreateContactType(see.Contract);
                        //ContractDescription cd = ContractDescription.GetContract(contactType);
                        Binding binding = WCFMateHelper.BindingFactory(serviceMeta, see);
                        if (binding is WebHttpBinding)
                        {
                            sh2 = new SpringWebServiceHost(service.Name, serviceMeta.ContextName, baseUris.ToArray());

                            sh2.AddServiceEndpoint(contactType, binding, see.Address);
                        }
                        else
                        {
                            try
                            {
                                sh = new SpringServiceHost(service.Name, serviceMeta.ContextName, baseUris.ToArray());

                                sh.AddServiceEndpoint(contactType, binding, see.Address);
                            }catch(Exception ex)
                            {
                                throw new Exception(string.Format("创建服务失败，WCF配置中的服务名称{0}，不能在容器中获取实例", service.Name), ex.InnerException);
                            }
                        }

                    }
                    try
                    {
                        if (sh2 == null)
                        { 
                            ServiceDebugBehavior sdb = sh.Description.Behaviors.Find<ServiceDebugBehavior>();
                            {
                                if (sdb != null)
                                    sdb.IncludeExceptionDetailInFaults = true;
                                else
                                {
                                    ServiceDebugBehavior sb = new ServiceDebugBehavior();
                                    sb.IncludeExceptionDetailInFaults = true;
                                    sh.Description.Behaviors.Add(sb);
                                }
                            }
                        
                            ServiceMetadataBehavior behavior = sh.Description.Behaviors.Find<ServiceMetadataBehavior>();
                            {

                                CreateMetadataBehavior(sh, behavior, metaServiceEndpoint);
                            }

                            WCFMateHelper.BuildingServiceaBehavior(serviceMeta, service, sh);

                            sh.Faulted += sh_Faulted;
                            sh.UnknownMessageReceived += sh_UnknownMessageReceived;
                            if (sh.State != CommunicationState.Opened)
                                sh.Open();
                            ssh.Add(sh);
                        }
                        else
                        {
                            ServiceDebugBehavior sdb = sh2.Description.Behaviors.Find<ServiceDebugBehavior>();
                            {
                                if (sdb != null)
                                    sdb.IncludeExceptionDetailInFaults = true;
                                else
                                {
                                    ServiceDebugBehavior sb = new ServiceDebugBehavior();
                                    sb.IncludeExceptionDetailInFaults = true;
                                    sh2.Description.Behaviors.Add(sb);
                                    
                                }
                            }
                            sh2.Faulted += sh_Faulted;
                            sh2.UnknownMessageReceived += sh_UnknownMessageReceived;
                            if (sh2.State != CommunicationState.Opened)
                                sh2.Open();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new WCFServiceCreateException(Resources.WCFServiceCreateException, ex);
                    }
                }
            }
            catch (Exception serException)
            {
                throw serException;
            }
            return ssh;
        }

        /// <summary>
        /// 收到位置消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void sh_UnknownMessageReceived(object sender, UnknownMessageReceivedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// 服务异常时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void sh_Faulted(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private Type CreateContactType(string contactName)
        {
            string assemblyName = "";
            string className = "";
            if (contactName.IndexOf(',') != -1)
            {
                assemblyName = contactName.Split(',')[1];
                className = contactName.Split(',')[0];
            }
            else
            {
                assemblyName = contactName.Remove(contactName.LastIndexOf('.'));
                className = contactName;
            }
            //IEnumerable<Assembly> ass = AppDomain.CurrentDomain.GetAssemblies().Where(o => o.FullName.IndexOf(assemblyName) != -1);
            IEnumerable<Assembly> ass = GetAssembly(ref assemblyName);
            if (ass.Count() == 0)
                throw new ServiceContractNotFoundException(string.Format("WCF 服务契约接口是：{0}.按逐层递减的去找程序集，没有找到。", className));
            foreach (Assembly a in ass)
            {
                Type t1 = a.GetType(className);
                if(t1!=null)
                    return t1;
            }
            throw new ServiceContractNotFoundException(string.Format("服务契约接口是：{0},在程序集{1}里没有找到。",contactName, assemblyName));
        }

        private IEnumerable<Assembly> GetAssembly(ref string assemblyName)
        {
            string t_assemblyName = assemblyName;
            IEnumerable<Assembly> ass = AppDomain.CurrentDomain.GetAssemblies().Where(o => o.FullName.IndexOf(t_assemblyName) != -1);
            
            if (ass.Count() > 0)
                return ass;
            else
            {
                if (assemblyName.IndexOf('.') == -1)
                    return new List<Assembly>();
                assemblyName = assemblyName.Remove(assemblyName.LastIndexOf('.'));
                return  GetAssembly(ref assemblyName);
            }
        }

        private void CreateMetadataBehavior(SpringServiceHost ssh, ServiceMetadataBehavior smb,ServiceEndpointElement metadataEle)
        {
            if (smb == null)
            {
                smb = new ServiceMetadataBehavior();
                if (ssh.BaseAddresses.Any(o => o.Scheme.ToLower() == Uri.UriSchemeHttp))
                {
                    smb.HttpGetEnabled = true;
                }
                ssh.Description.Behaviors.Add(smb);
            }
            
            foreach (var baseAddress in ssh.BaseAddresses)
            {
                //BindingElement bindingElement = null;
                Binding bindingElement = null;
                switch (baseAddress.Scheme)
                {
                    case  "net.tcp":
                        {
                            bindingElement = MetadataExchangeBindings.CreateMexTcpBinding();
                            //bindingElement = new TcpTransportBindingElement();
                            break;
                        }
                    case "net.pipe":
                        {
                            bindingElement = MetadataExchangeBindings.CreateMexNamedPipeBinding();
                            //bindingElement = new NamedPipeTransportBindingElement();
                            break;
                        }
                    case "http":
                        {
                            bindingElement = MetadataExchangeBindings.CreateMexHttpBinding();
                            //bindingElement = new HttpTransportBindingElement();
                            break;
                        }
                    case "https":
                        {
                            bindingElement = MetadataExchangeBindings.CreateMexHttpsBinding();
                            //bindingElement = new HttpsTransportBindingElement();
                            break;
                        }
                    default:
                        throw new ProtocolException("The base address {0} Unable to identify".FormatString(baseAddress.ToString()));
                }
                if (bindingElement != null)
                {
                    //Binding binding = new CustomBinding(bindingElement);
                    ssh.AddServiceEndpoint(typeof(IMetadataExchange), bindingElement, "MEX");
                }
            }
        }
    }
}

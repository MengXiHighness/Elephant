using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Configuration;
using System.Text;
using Spring.ServiceModel;

namespace DS.AFP.Communication.WCF
{
    /// <summary>
    /// WCF服务容器
    /// </summary>
    public class WCFServiceContainer
    {
        private static Dictionary<object, List<SpringServiceHost>> _container = new Dictionary<object, List<SpringServiceHost>>();

        /// <summary>
        /// 按指定的容器环境名以及配置创建WCF服务，并添加到容器
        /// <code>
        /// WCFServiceContainer.Create(contextName, CurrentAddinConfiguration);//初始化WCF服务
        /// </code>
        /// </summary>
        /// <param name="contextName"></param>
        /// <param name="configuration"></param>
        public static void Create(string contextName, System.Configuration.Configuration configuration)
        {
            WCFServiceMeta wm = InitializeWCFServiceMeta(contextName,configuration);

            if (wm.IsExistService)
            {
                lock (_container)
                {
                    WCFService wcfservice = new WCFService();
                    _container.Add(wm, wcfservice.Builder(wm));
                }
            }
        }

        /// <summary>
        /// 打开容器内所有状态非Opened与Opening的SpringServiceHost对象
        /// </summary>
        public static void Start()
        {
            foreach (var sshl in _container.Values)
            {
                foreach (SpringServiceHost ssh in sshl)
                {
                    if (ssh.State != CommunicationState.Opened && ssh.State != CommunicationState.Opening)
                        ssh.Open();
                }
            }
        }

        /// <summary>
        /// 关闭容器内所有SpringServiceHost对象
        /// </summary>
        public static void Close()
        {
            foreach (List<SpringServiceHost> sshl in _container.Values)
            {
                foreach (SpringServiceHost ssh in sshl)
                {
                    if (ssh.State == CommunicationState.Opened)
                        ssh.Close();
                }
            }
        }

        /// <summary>
        /// 初始化WCF元数据
        /// </summary>
        /// <param name="config"></param>
        private static WCFServiceMeta InitializeWCFServiceMeta(string contextName,System.Configuration.Configuration config)
        {
            WCFServiceMeta wm = new WCFServiceMeta();
            ServicesSection sconfig = config.GetSection(GlobalParams.ServiceHostSession) as ServicesSection;
            if (sconfig != null)
            {
                if (sconfig.Services.Count > 0)
                {
                    wm.IsExistService = true;
                    wm.ServicesConfiguration = sconfig;
                    wm.ContextName = contextName.ToLower();
                    wm.ChildConfiguration = config;
                }
                else
                {
                    wm.IsExistService = false;
                }
            }
            BehaviorsSection bconfig = config.GetSection(GlobalParams.BehaviorSession) as BehaviorsSection;
            if (bconfig != null)
            {
                wm.BehaviorsConfiguration = bconfig;
            }
            return wm;

        }
    }
}

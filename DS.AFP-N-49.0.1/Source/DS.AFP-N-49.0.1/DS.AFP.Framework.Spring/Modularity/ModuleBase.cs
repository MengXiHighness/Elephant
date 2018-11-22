using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Reflection;
using System.Resources;
using Spring.Context.Support;
using Spring.Context;
using Spring.Objects.Factory;
using Spring.Objects.Factory.Xml;
using Spring.Core.IO;
using Spring.Util;
using System.IO;
using DS.AFP.Framework;
using System.Diagnostics;
using DS.AFP.Common.Core.ConfigurationNameSpace;
using DS.AFP.Communication.WCF;
using DS.AFP.Common.Core;
using Spring.Objects.Factory.Config;
using DS.AFP.Framework.SocketNameSpace;
using DS.AFP.Common.Core.Utility;
using DS.AFP.Framework.Spring;


namespace DS.AFP.Framework.Modularity
{
    /// <summary>
    /// 插件基类
    /// </summary>
    public abstract class ModuleBase : IModule
    {
        /// <summary>
        /// 平台配置
        /// </summary>
        public IDsConfigurationSection DsConfigurationSection { get; private set; }

        /// <summary>
        /// 插件配置
        /// </summary>
        public System.Configuration.Configuration CurrentAddinConfiguration { get; private set; }


        static System.Collections.Concurrent.ConcurrentDictionary<string, Configuration> addinConfigurationList = new System.Collections.Concurrent.ConcurrentDictionary<string, Configuration>();
        /// <summary>
        /// 所有插件配置集合
        /// </summary>
        public System.Collections.Concurrent.ConcurrentDictionary<string, Configuration> AddinConfigurationList
        {
            get
            {
                return addinConfigurationList;
            }
        }

        /// <summary>
        /// //插件对象容器
        /// </summary>
        public IApplicationContext Container { get; private set; }

        /// <summary>
        /// 平台对象容器
        /// </summary>
        public static IApplicationContext ParentContaioner { get; private set; }

        /// <summary>
        /// 插件名称
        /// </summary>
        public AddinInfo AddinInfo { get; private set; }

        /// <summary>
        /// 日志
        /// </summary>
        private ILoggerFacade logger { get; set; }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="container"></param>
        /// <param name="dsconfig"></param>
        protected ModuleBase(IApplicationContext container, IDsConfigurationSection dsconfig)
        {

//#if DEBUG

//            Debugger.Launch();

//#endif
            logger = new LoggerFacade(this.GetType());
            DsConfigurationManager dcm = new DsConfigurationManager();            
            Assembly a = this.GetType().Assembly;
            string contextName = a.GetName().Name.ToLower();
            
            ParentContaioner = container;
            //初始化环境信息
            this.InitializationDsEnvironment(dsconfig, a.FullName);
            logger.Debug("{0} Plug-ins start loading".FormatString(AddinInfo.AddinName));
            //SyncInfo(dcm);
            if (File.Exists(AddinInfo.ConfigurationFilePath))
            {
                System.Configuration.Configuration ecs = dcm.Get<System.Configuration.Configuration>(AddinInfo.ConfigurationFilePath);
                if (ecs == null)
                {
                    logger.Error("{0} Configuration file loading exception, configuration file does not exist!".FormatString(AddinInfo.AddinName));
                }
                CurrentAddinConfiguration = ecs;
                this.DsConfigurationSection = dsconfig;
                AddinConfigurationList.TryAdd(AddinInfo.AddinName, ecs);

                ParentContaioner.RegisterInstance(this);
                Container = ChildContainer.Create(container, contextName, ecs);

                GlobalObject.SetAddinContanier(AddinInfo.AddinName, Container);
                //初始化WCF服务
                WCFServiceContainer.Create(contextName, CurrentAddinConfiguration);
                //初始化Socket服务
                SocketServiceContainer.Create(CurrentAddinConfiguration);
            }
            logger.Debug("{0} Plug-in loaded".FormatString(AddinInfo.AddinName));
        }

      

        /// <summary>
        /// 初始化环境参数
        /// </summary>
        /// <param name="dsConfig"></param>
        /// <param name="assemblyFullName"></param>
        private void InitializationDsEnvironment(IDsConfigurationSection dsConfig, string assemblyFullName)
        {

            //#if DEBUG

            //            Debugger.Launch();    

            //#endif
            Console.WriteLine("InitializationDsEnvironment:{0},{1}".FormatString(dsConfig.ToString(), assemblyFullName));
            try
            {
                //var server;
                IDsEnvironment de = ParentContaioner.GetObject<IDsEnvironment>();
                IList<ModuleConfigurationElement> server = (from ModuleConfigurationElement me in dsConfig.Modules
                                                            where me.ModuleType.EndsWith(assemblyFullName)
                                                            select me).ToList<ModuleConfigurationElement>();
                Console.WriteLine("Modules：{0},server count:{0}".FormatString(dsConfig.Modules.Count,server.Count));
                ModuleConfigurationElement mce = server.FirstOrDefault();
                this.AddinInfo = new AddinInfo()
                {
                    AddinAssemblyName = assemblyFullName,
                    AddinNameSpace = mce.AssemblyFile.Substring(mce.AssemblyFile.LastIndexOf('/') + 1).Replace(".dll", ""),
                    AddinName = mce.ModuleName,
                    ConfigurationFilePath = ConfigurationFileHelper.GetAddinConfigurationFilePath(mce.AssemblyFile),
                    AddinConfigurationEle = mce,
                    AddinPath = PathHelper.GetFullPath(mce.AssemblyFile)
                };
                if (de.AddinInfos.ContainsKey(AddinInfo.AddinName))
                {
                    AddinInfo addin = de.AddinInfos[AddinInfo.AddinName];
                    addin.ConfigurationFilePath = this.AddinInfo.ConfigurationFilePath;
                    addin.AddinConfigurationEle = this.AddinInfo.AddinConfigurationEle;
                    addin.AddinPath = this.AddinInfo.AddinPath;
                    //throw new Exception("DsEnvironment中注册AddinName失败，原因是AddinName作为键值重复!");
                }
                else
                {
                    de.AddinInfos.Add(mce.ModuleName, AddinInfo);

                }
            }catch(Exception ex)
            {
                Console.WriteLine("InitializationDsEnvironment异常:{0}".FormatString(ex.StackTrace.ToString()));

                throw new Exception("InitializationDsEnvironment异常。", ex);
            }

        }

    

        public abstract void Initialize();

        /// <summary>
        /// 通过指定类型注册Region
        /// </summary>
        /// <param name="formObjectId"></param>
        /// <param name="registerType"></param>
        /// <param name="registerSingleton"></param>
        public void RegisterRegionFormType(string formObjectId, Type registerType, bool registerSingleton,bool lazyInit = false)
        {
            ParentContaioner.RegisterTypeIfMissing(formObjectId, registerType, registerSingleton, AutoWiringMode.Constructor,lazyInit);
        }
    }




}

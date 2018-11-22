using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring.Context;
using Spring.Context.Support;
using System.Reflection;
using Spring.Util;
using Spring.Core.TypeResolution;
using System.Configuration;
using System.Xml;
using System.Configuration.Internal;

namespace DS.AFP.Framework
{
    /// <summary>
    /// 子容器类
    /// </summary>
    public class ChildContainer
    {
        /// <summary>
        /// 创建容器
        /// <code>
        /// IApplicationContext Container = ChildContainer.Create(container, contextName, ecs);
        /// </code>
        /// </summary>
        /// <param name="container">IApplicationContext容器</param>
        /// <param name="contextName">容器名</param>
        /// <param name="configuration">Configuration配置对象</param>
        /// <returns></returns>
        public static IApplicationContext Create(IApplicationContext container,string contextName,Configuration configuration)
        {
            string _contextName = contextName.ToLower();
            ExeConfigurationSystem config = new ExeConfigurationSystem(configuration);
            IApplicationContext ctx = config.GetSection(_contextName, container, AbstractApplicationContext.ContextSectionName) as IApplicationContext;
            if (!ContextRegistry.IsContextRegistered(_contextName) && ctx.Name != "spring.root")
            {
                try
                {
                    ContextRegistry.RegisterContext(ctx);
                }
                catch(Exception e)
                {
                    throw new Exception("Registered ContextName does not exist!Could be a plug-in ContextName for plug-in assembly name (not fullName)", e);
                }
            }
            return ctx;
        }


    }

    /// <summary>
    /// 系统配置管理类
    /// </summary>
    public class ExeConfigurationSystem : IChainableConfigSystem
    {
        private string _configPath;
        private System.Configuration.Configuration _configuration;
        private IInternalConfigSystem _next;

        /// <summary>
        /// initializes this instance with a path to be passed into <see cref="ConfigurationManager.OpenExeConfiguration(string)"/>
        /// </summary>
        /// <param name="configPath"></param>
        public ExeConfigurationSystem(Configuration config)
        {
            this._configPath = (config.HasFile) ? config.FilePath : "";
            this._configuration = config;
        }

        /// <summary>
        /// Purges cached configuration
        /// </summary>
        public void RefreshConfig(string sectionName)
        {
            if (_next != null)
            {
                _next.RefreshConfig(sectionName);
            }
            _configuration = null;
        }

        ///<summary>
        /// Only true if the underlying config system supports this.
        ///</summary>
        public bool SupportsUserConfig
        {
            get
            {
                EnsureInit();
                if (_next != null)
                {
                    return _next.SupportsUserConfig;
                }
                return false;
            }
        }

        /// <summary>
        /// Set the nested configuration system to delegate calls in case we can't resolve a config section ourselves
        /// </summary>
        public void SetInnerConfigurationSystem(IInternalConfigSystem innerConfigSystem)
        {
            _next = innerConfigSystem;
        }

        private void EnsureInit()
        {
            if (_configuration == null)
            {
                lock (this)
                {
                    if (_configuration == null)
                    {
                        _configuration = ConfigurationManager.OpenExeConfiguration(_configPath);
                    }
                }
            }
        }

        private delegate object ResolveSectionRuntimeObject(ConfigurationSection section);

        private static ResolveSectionRuntimeObject resolveSectionRuntimeObject =
            (ResolveSectionRuntimeObject)Delegate.CreateDelegate(typeof(ResolveSectionRuntimeObject),
                                                                  typeof(ConfigurationSection).GetMethod("GetRuntimeObject",
                                                                                                          BindingFlags.Instance |
                                                                                                          BindingFlags.NonPublic));

        /// <summary>
        /// Get the specified section
        /// </summary>
        /// <param name="sectionName"></param>
        /// <returns></returns>
        public object GetSection(string sectionName)
        {
            return null;
            
        }

        /// <summary>
        /// Get the specified section
        /// </summary>
        /// <param name="contextName"></param>
        /// <param name="parent"></param>
        /// <param name="sectionName"></param>
        /// <returns></returns>
        public object GetSection(string contextName,object parent, string sectionName)
        {
            EnsureInit();
            ConfigurationSection thisSection = _configuration.GetSection(sectionName);

            //object parent = null;
            if (_next != null)
            {
                parent = _next.GetSection(sectionName);
            }
            if (thisSection == null)
            {
                return parent;
            }

            object result = resolveSectionRuntimeObject(thisSection);
            if (result is DefaultSection)
            {
                string rawXml = thisSection.SectionInformation.GetRawXml();
                if (string.IsNullOrEmpty(rawXml))
                {
                    return null;
                }

                Type t = TypeResolutionUtils.ResolveType(thisSection.SectionInformation.Type);
                //ContextHandler ch = new ContextHandler();
                if (typeof(IConfigurationSectionHandler).IsAssignableFrom(t))
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(thisSection.SectionInformation.GetRawXml());
                    IConfigurationSectionHandler handler = (IConfigurationSectionHandler)Activator.CreateInstance(t);
                    return handler.Create(parent, null, SetContextName(contextName, xmlDoc.DocumentElement));
                }
                throw new ConfigurationErrorsException(string.Format(" <section>配置节点没有声明'{0}'", sectionName));
            }
            return result;
        }

        private XmlElement SetContextName(string contextName,XmlElement contextElement)
        {
			contextElement.SetAttribute("name",contextName);
            return contextElement ;
        }
    }
}

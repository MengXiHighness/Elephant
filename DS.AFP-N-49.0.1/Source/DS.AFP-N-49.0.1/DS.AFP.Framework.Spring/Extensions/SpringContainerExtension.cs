using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring.Context;
using Spring.Objects.Factory.Config;
using Spring.Objects.Factory.Support;

namespace DS.AFP.Framework
{
    /// <summary>
    /// Spring容器扩展
    /// </summary>
    public static class SpringContainerExtension
    {
        /// <summary>
        /// 检查容器内某个type类型是否已注册
        /// </summary>
        /// <param name="container"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsTypeRegistered(this IApplicationContext container, Type type)
        {
            return (container.ContainsObjectDefinition(type.Name) || container.ContainsObjectDefinition(type.FullName));
        }

        /// <summary>
        /// 为容器注册实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="container"></param>
        /// <param name="instance"></param>
        public static void RegisterInstance<T>(this IApplicationContext container, T instance) where T:class
        {
            string alias = typeof(T).Name;
            RegisterInstance<T>(container, alias, instance);
        }

        /// <summary>
        /// 为容器注册实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="container"></param>
        /// <param name="alias"></param>
        /// <param name="instance"></param>
        public static void RegisterInstance<T>(this IApplicationContext container,string alias, T instance) where T : class
        {
            IConfigurableApplicationContext configurableContext = container as IConfigurableApplicationContext;
            if (configurableContext != null && !container.ContainsObjectDefinition(alias) && !container.ContainsObject(alias))
            {
                configurableContext.ObjectFactory.RegisterSingleton(alias, instance);
            }
            else if (configurableContext != null && !container.ContainsObjectDefinition(typeof(T).FullName) && !container.ContainsObject(typeof(T).FullName))
            {
                configurableContext.ObjectFactory.RegisterSingleton(typeof(T).FullName, instance);
            }
        }

        /// <summary>
        /// 如果某个类型未在容器注册，则注册该类型
        /// </summary>
        /// <param name="container"></param>
        /// <param name="alias"></param>
        /// <param name="registerType"></param>
        /// <param name="registerAsSingleton"></param>
        /// <param name="autoWiringMode"></param>
        public static void RegisterTypeIfMissing(this IApplicationContext container, string alias,Type registerType, bool registerAsSingleton,AutoWiringMode autoWiringMode,bool lazyInit = false) 
        {
            Type typeToRegister = registerType;
            if (!container.ContainsObjectDefinition(alias))
            {
                DefaultObjectDefinitionFactory definitionFactory = new DefaultObjectDefinitionFactory();
                ObjectDefinitionBuilder builder = ObjectDefinitionBuilder.RootObjectDefinition(definitionFactory, typeToRegister);
                builder.SetSingleton(registerAsSingleton);
                builder.SetAutowireMode(autoWiringMode);
                builder.SetLazyInit(lazyInit);
                IConfigurableApplicationContext configurableContext = container as IConfigurableApplicationContext;
                IObjectDefinitionRegistry definitionRegistry = container as IObjectDefinitionRegistry;

                if (definitionRegistry != null)
                {
                    definitionRegistry.RegisterObjectDefinition(alias, builder.ObjectDefinition);
                    //if (configurableContext != null)
                    //{
                    //    configurableContext.Refresh();
                    //}
                }
            }
        }

        /// <summary>
        /// 如果某个类型未在容器注册，则注册该类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="container"></param>
        /// <param name="alias"></param>
        /// <param name="registerAsSingleton"></param>
        public static void RegisterTypeIfMissing<T>(this IApplicationContext container,string alias,bool registerAsSingleton) where T:class
        {
            RegisterTypeIfMissing<T>(container, alias, registerAsSingleton, AutoWiringMode.Constructor);
        }

        /// <summary>
        /// 如果某个类型未在容器注册，则注册该类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="container"></param>
        /// <param name="alias"></param>
        /// <param name="registerAsSingleton"></param>
        /// <param name="autoWiringMode"></param>
        public static void RegisterTypeIfMissing<T>(this IApplicationContext container, string alias, bool registerAsSingleton,AutoWiringMode autoWiringMode) where T : class
        {
            Type typeToRegister = typeof(T);
            RegisterTypeIfMissing(container, alias, typeToRegister, registerAsSingleton, autoWiringMode);
        }

        /// <summary>
        /// 获取容器内某类型的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="container"></param>
        /// <returns></returns>
        public static T GetObject<T>(this IApplicationContext container) where T:class
        {
            Type t = typeof(T);
            return container.GetObject(t.Name) as T;
        }
    }
}

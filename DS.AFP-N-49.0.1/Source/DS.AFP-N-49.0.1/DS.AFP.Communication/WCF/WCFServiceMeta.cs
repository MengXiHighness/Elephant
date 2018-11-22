using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.AFP.Communication.WCF
{
    /// <summary>
    /// WCF 元数据
    /// </summary>
    public class WCFServiceMeta
    {
        /// <summary>
        /// 容器环境名称
        /// </summary>
        public string ContextName { get; set; }

        /// <summary>
        /// 插件配置
        /// </summary>
        public System.Configuration.Configuration ChildConfiguration { get; set; }

        /// <summary>
        /// 平台配置
        /// </summary>
        public System.Configuration.Configuration ParentConfiguration { get; set; }

        /// <summary>
        /// WCF配置
        /// </summary>
        public System.ServiceModel.Configuration.ServicesSection ServicesConfiguration { get; set; }

        /// <summary>
        /// 行为配置
        /// </summary>
        public System.ServiceModel.Configuration.BehaviorsSection BehaviorsConfiguration { get; set; }

        /// <summary>
        /// 是否存在服务
        /// </summary>
        public bool IsExistService { get; set; }
    }
}

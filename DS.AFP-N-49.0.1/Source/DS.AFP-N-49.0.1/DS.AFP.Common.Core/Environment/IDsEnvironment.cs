using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace DS.AFP.Common.Core
{
    /// <summary>
    /// 环境接口（DsEnvironment的基类）
    /// </summary>
    public interface IDsEnvironment
    {
        /// <summary>
        /// 环境类型
        /// </summary>
        EnvironmentType EnvironmentType { get; set; }

        /// <summary>
        /// 宿主名称
        /// </summary>
        string HostName { get; set; }

        /// <summary>
        /// 插件信息集合
        /// </summary>
        ThreadSafeDictionary<string, AddinInfo> AddinInfos { get; }

        /// <summary>
        /// 共享数据
        /// </summary>
        ThreadSafeDictionary<string, object> ShareData { get; set; }

        /// <summary>
        /// 当前使用的Theme Culture.默认是zh-cn
        /// </summary>
        CultureInfo Culture { get; set; }
    }
}

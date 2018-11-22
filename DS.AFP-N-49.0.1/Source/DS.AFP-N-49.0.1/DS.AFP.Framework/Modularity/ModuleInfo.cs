
using System;
using System.Collections.ObjectModel;

namespace DS.AFP.Framework.Modularity
{
    /// <summary>
    /// 插件信息
    /// </summary>
    public partial class ModuleInfo : IModuleCatalogItem
    {
        public ModuleInfo()
            : this(null, null, 1000, new string[0])
        {
        }

        public ModuleInfo(string name, string type)
            : this(name, type, 1000, new string[0])
        {
        }

        public ModuleInfo(string name, string type, int index, params string[] dependsOn)
        {
            if (dependsOn == null) throw new System.ArgumentNullException("dependsOn");

            this.ModuleName = name;
            this.ModuleType = type;
            this.ModuleIndex = index;
            this.DependsOn = new Collection<string>();
            foreach (string dependency in dependsOn)
            {
                this.DependsOn.Add(dependency);
            }
        }

        /// <summary>
        /// 插件名称
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        /// 插件标题
        /// </summary>
        public string ModuleTitle { get; set; }

        /// <summary>
        /// 插件描述
        /// </summary>
        public string ModuleDescription { get; set; }

        /// <summary>
        /// 插件类型
        /// </summary>
        public string ModuleType { get; set; }

        /// <summary>
        /// Splash key
        /// </summary>
        public Type SplashType { get; set; }

        /// <summary>
        /// 插件索引
        /// </summary>
        public int ModuleIndex { get; set; }


        //如果是继承ModuleBase，作为模块入口的组件则为true，否则为false
        public bool IsModuleEntrance { get; set; }

        /// <summary>
        /// 插件状态
        /// </summary>
        public ModuleState State { get; set; }

        /// <summary>
        /// 插件依赖
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "The setter is here to work around a Silverlight issue with setting properties from within Xaml.")]
        public Collection<string> DependsOn { get; set; }

        public InitializationMode InitializationMode { get; set; }

        /// <summary>
        /// 返回插件路径
        /// http://myDomain/ClientBin/MyModules.xap 远程
        /// file://c:/MyProject/Modules/MyModule.dll 本地
        /// </example>
        /// </summary>
        public string Ref { get; set; }


    }
}


using System;
using System.Collections.ObjectModel;

namespace DS.AFP.Framework.Modularity
{
    /// <summary>
    /// �����Ϣ
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
        /// �������
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        /// �������
        /// </summary>
        public string ModuleTitle { get; set; }

        /// <summary>
        /// �������
        /// </summary>
        public string ModuleDescription { get; set; }

        /// <summary>
        /// �������
        /// </summary>
        public string ModuleType { get; set; }

        /// <summary>
        /// Splash key
        /// </summary>
        public Type SplashType { get; set; }

        /// <summary>
        /// �������
        /// </summary>
        public int ModuleIndex { get; set; }


        //����Ǽ̳�ModuleBase����Ϊģ����ڵ������Ϊtrue������Ϊfalse
        public bool IsModuleEntrance { get; set; }

        /// <summary>
        /// ���״̬
        /// </summary>
        public ModuleState State { get; set; }

        /// <summary>
        /// �������
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "The setter is here to work around a Silverlight issue with setting properties from within Xaml.")]
        public Collection<string> DependsOn { get; set; }

        public InitializationMode InitializationMode { get; set; }

        /// <summary>
        /// ���ز��·��
        /// http://myDomain/ClientBin/MyModules.xap Զ��
        /// file://c:/MyProject/Modules/MyModule.dll ����
        /// </example>
        /// </summary>
        public string Ref { get; set; }


    }
}

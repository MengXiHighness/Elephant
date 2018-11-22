
using System.Configuration;
using DS.AFP.Common.Core.ConfigurationNameSpace;

namespace DS.AFP.Framework.Modularity
{
    public class ConfigurationStore : IConfigurationStore
    {
        /// <summary>
        /// ��ȡƽ̨������Ϣ
        /// </summary>
        public IDsConfigurationSection RetrieveModuleConfigurationSection()
        {
            IDsConfigurationManager dm = new DsConfigurationManager();
            return dm.DsRootConfigurationSection;
        }
    }
}
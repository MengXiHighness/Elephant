
using System.Configuration;
using DS.AFP.Common.Core.ConfigurationNameSpace;

namespace DS.AFP.Framework.Modularity
{
    public class ConfigurationStore : IConfigurationStore
    {
        /// <summary>
        /// 获取平台配置信息
        /// </summary>
        public IDsConfigurationSection RetrieveModuleConfigurationSection()
        {
            IDsConfigurationManager dm = new DsConfigurationManager();
            return dm.DsRootConfigurationSection;
        }
    }
}
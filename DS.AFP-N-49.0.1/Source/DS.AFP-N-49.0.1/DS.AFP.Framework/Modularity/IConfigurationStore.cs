
using DS.AFP.Common.Core.ConfigurationNameSpace;
namespace DS.AFP.Framework.Modularity
{
    /// <summary>
    /// 配置信息接口
    /// </summary>
    public interface IConfigurationStore
    {
        /// <summary>
        /// 得到配置信息
        /// </summary>
        /// <returns>A <see cref="ModulesConfigurationSection"/> </returns>
        IDsConfigurationSection RetrieveModuleConfigurationSection();
    }
}
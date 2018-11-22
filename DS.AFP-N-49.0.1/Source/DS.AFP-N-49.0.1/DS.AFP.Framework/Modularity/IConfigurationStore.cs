
using DS.AFP.Common.Core.ConfigurationNameSpace;
namespace DS.AFP.Framework.Modularity
{
    /// <summary>
    /// ������Ϣ�ӿ�
    /// </summary>
    public interface IConfigurationStore
    {
        /// <summary>
        /// �õ�������Ϣ
        /// </summary>
        /// <returns>A <see cref="ModulesConfigurationSection"/> </returns>
        IDsConfigurationSection RetrieveModuleConfigurationSection();
    }
}
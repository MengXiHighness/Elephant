

namespace DS.AFP.Framework.Modularity
{
    /// <summary>
    /// ���򼯽ӿ�
    /// </summary>
    public interface IAssemblyResolver
    {
        /// <summary>
        /// ��Ӧ�ó�����Ҫʱ���س���
        /// </summary>
        /// <param name="assemblyFilePath"></param>
        void LoadAssemblyFrom(string assemblyFilePath);
    }
}

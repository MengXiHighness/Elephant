

namespace DS.AFP.Framework.Modularity
{
    /// <summary>
    /// 程序集接口
    /// </summary>
    public interface IAssemblyResolver
    {
        /// <summary>
        /// 当应用程序需要时加载程序集
        /// </summary>
        /// <param name="assemblyFilePath"></param>
        void LoadAssemblyFrom(string assemblyFilePath);
    }
}

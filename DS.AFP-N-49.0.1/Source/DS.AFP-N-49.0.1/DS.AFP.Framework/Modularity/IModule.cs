//===================================================================================
// Microsoft patterns & practices
// Composite Application Guidance for Windows Presentation Foundation and Silverlight

using System;
namespace DS.AFP.Framework.Modularity
{
    /// <summary>
    /// 插件接口
    /// </summary>
    public interface IModule
    {
        /// <summary>
        /// 初始化.
        /// </summary>
        void Initialize();

        void RegisterRegionFormType(string formObjectId, Type registerType, bool registerSingleton,bool lazyInit);


    }
}
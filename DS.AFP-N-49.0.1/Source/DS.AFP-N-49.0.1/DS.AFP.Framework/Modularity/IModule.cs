//===================================================================================
// Microsoft patterns & practices
// Composite Application Guidance for Windows Presentation Foundation and Silverlight

using System;
namespace DS.AFP.Framework.Modularity
{
    /// <summary>
    /// ����ӿ�
    /// </summary>
    public interface IModule
    {
        /// <summary>
        /// ��ʼ��.
        /// </summary>
        void Initialize();

        void RegisterRegionFormType(string formObjectId, Type registerType, bool registerSingleton,bool lazyInit);


    }
}
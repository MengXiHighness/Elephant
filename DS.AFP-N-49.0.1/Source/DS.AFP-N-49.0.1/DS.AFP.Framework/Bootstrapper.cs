
using System;
using System.Windows;
using DS.AFP.Common.Core;
using DS.AFP.Framework.Modularity;
using Microsoft.Practices.ServiceLocation;

namespace DS.AFP.Framework
{
    /// <summary>
    /// ��������
    /// </summary>
    public abstract class Bootstrapper
    {
        /// <summary>
        /// ��־�ӿ�
        /// </summary>
        protected ILoggerFacade Logger { get; set; }

        /// <summary>
        /// ģ����
        /// </summary>
        protected IModuleCatalog ModuleCatalog { get; set; }

        /// <summary>
        /// ƽ̨����
        /// </summary>
        protected IDsEnvironment Environment { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "The Logger is added to the container which will dispose it when the container goes out of scope.")]
        protected virtual ILoggerFacade CreateLogger()
        {
            return new LoggerFacade(this.GetType());
        }

        /// <summary>
        /// ���п�����
        /// </summary>
        public void Run()
        {
            this.Run(true);
        }

        protected virtual IModuleCatalog CreateModuleCatalog()
        {
            return new ModuleCatalog();
        }
        
        protected virtual void ConfigureModuleCatalog()
        {
        }

        /// <summary>
        /// ��ʼ������������Ĭ��������WPF������
        /// </summary>
        protected virtual void ConfigureEnvironment()
        {
            Environment = new DsEnvironment() { EnvironmentType = EnvironmentType.WPF };
        }
        /// <summary>
        /// ����������Ϣ���ظ�������ĳ���
        /// </summary>
        protected virtual void PreInitializeModules()
        {

        }

        protected virtual void RegisterFrameworkExceptionTypes()
        {
            ExceptionExtensions.RegisterFrameworkExceptionType(
                typeof(ActivationException));
        }
        
        /// <summary>
        /// ��ʼ��ģ����Ϣ
        /// </summary>
        protected virtual void InitializeModules()
        {
            IModuleManager manager = ServiceLocator.Current.GetInstance<IModuleManager>();
            manager.Run();
        }

        /// <summary>
        /// ���п�����
        /// </summary>
        /// <param name="runWithDefaultConfiguration"></param>
        public abstract void Run(bool runWithDefaultConfiguration);

        protected abstract void ConfigureServiceLocator();
    }
}

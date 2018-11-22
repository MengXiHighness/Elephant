
using System;
using System.Windows;
using DS.AFP.Common.Core;
using DS.AFP.Framework.Modularity;
using Microsoft.Practices.ServiceLocation;

namespace DS.AFP.Framework
{
    /// <summary>
    /// 框架入口类
    /// </summary>
    public abstract class Bootstrapper
    {
        /// <summary>
        /// 日志接口
        /// </summary>
        protected ILoggerFacade Logger { get; set; }

        /// <summary>
        /// 模块组
        /// </summary>
        protected IModuleCatalog ModuleCatalog { get; set; }

        /// <summary>
        /// 平台环境
        /// </summary>
        protected IDsEnvironment Environment { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "The Logger is added to the container which will dispose it when the container goes out of scope.")]
        protected virtual ILoggerFacade CreateLogger()
        {
            return new LoggerFacade(this.GetType());
        }

        /// <summary>
        /// 运行框架入口
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
        /// 初始化环境参数，默认宿主在WPF上运行
        /// </summary>
        protected virtual void ConfigureEnvironment()
        {
            Environment = new DsEnvironment() { EnvironmentType = EnvironmentType.WPF };
        }
        /// <summary>
        /// 根据配置信息加载各个插件的程序集
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
        /// 初始化模块信息
        /// </summary>
        protected virtual void InitializeModules()
        {
            IModuleManager manager = ServiceLocator.Current.GetInstance<IModuleManager>();
            manager.Run();
        }

        /// <summary>
        /// 运行框架入口
        /// </summary>
        /// <param name="runWithDefaultConfiguration"></param>
        public abstract void Run(bool runWithDefaultConfiguration);

        protected abstract void ConfigureServiceLocator();
    }
}

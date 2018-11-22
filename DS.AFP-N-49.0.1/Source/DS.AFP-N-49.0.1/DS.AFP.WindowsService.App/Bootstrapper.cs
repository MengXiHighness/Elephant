
namespace DS.AFP.WindowsService.App
{
    using System;
    using System.Windows;
    using DS.AFP.Common.Core;
    using DS.AFP.Framework;
    using System.Windows.Threading;
    using System.Windows.Controls;
    using DS.AFP.Framework.WindowService;

    /// <summary>
    /// 系统入口
    /// </summary>
    public class Bootstrapper : WindowServiceBootstrapper
    {
        private ILoggerFacade Logger = null;
        protected override void ConfigureEnvironment()
        {
            this.Environment = new DsEnvironment()
            {
                EnvironmentType = EnvironmentType.WindowsService,
                HostName =Program.HOSTNAME// EnvironmentType.WindowsService.ToDescription()
            };
        }

        public Bootstrapper(ILoggerFacade logger)
        {
            Logger = logger;
        }

        /// <summary>
        /// 创建日志实例
        /// </summary>
        /// <returns></returns>
        protected override ILoggerFacade CreateLogger()
        {
            return Logger;// new LoggerFacade();
        }

        public void Stop()
        {
            //DS21.DS21Moudle mb = Container.GetObject("DS21Moudle") as DS21.DS21Moudle;
            //DS21Manager ds21Server = mb.Container.GetObject("DS21Manager") as DS21Manager;
            //ds21Server.Dispose();
        }
    }
}

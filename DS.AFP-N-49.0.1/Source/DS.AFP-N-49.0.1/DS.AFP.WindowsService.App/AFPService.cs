using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using DS.AFP.Common.Core;


namespace DS.AFP.WindowsService.App
{
    public partial class AFPService : ServiceBase
    {
        ILoggerFacade LoggerFacade = null;
        Bootstrapper bootstrapper;
        public AFPService(ILoggerFacade logger)
        {
            InitializeComponent();

            LoggerFacade = logger;// new LoggerFacade("DS.AFP.WindowsService.App");
            if (!EventLog.SourceExists("DS.AFP.WindowsService.App"))
            {
                EventLog.CreateEventSource("DS.AFP.WindowsService.App", "Application");
            }

            EventLog.WriteEntry("DS.AFP.WindowsService.App", "AFPService构造开始", EventLogEntryType.Information);
        }

        protected override void OnStart(string[] args)
        {
//#if DEBUG

//            Debugger.Launch();    //Launches and attaches a debugger to the process.

//#endif
            if (!EventLog.SourceExists("DS.AFP.WindowsService.App"))
            {
                EventLog.CreateEventSource("DS.AFP.WindowsService.App", "Application");
            }

            EventLog.WriteEntry("DS.AFP.WindowsService.App", "AFPService OnStart开始", EventLogEntryType.Information);
            LoggerFacade.Info( "服务“{0}”启动开始".FormatString(GlobalParams.ServiceName));
            bootstrapper = new Bootstrapper(LoggerFacade);
            bootstrapper.Run();
            base.OnStart(args);
            LoggerFacade.Info( "服务“{0}”启动结束".FormatString(GlobalParams.ServiceName));
        }


        protected override void OnStop()
        {
            LoggerFacade.Info( "服务“{0}”停止开始".FormatString(GlobalParams.ServiceName));
            bootstrapper.Stop();
            base.OnStop();
            LoggerFacade.Info( "服务“{0}”停止结束".FormatString(GlobalParams.ServiceName));
        }

        protected override void OnContinue()
        {
            LoggerFacade.Info( "服务“{0}”继续开始".FormatString(GlobalParams.ServiceName));
            base.OnContinue();
            LoggerFacade.Info( "服务“{0}”继续结束".FormatString(GlobalParams.ServiceName));
        }

        protected override void OnPause()
        {
            LoggerFacade.Info( "服务“{0}”暂停开始".FormatString(GlobalParams.ServiceName));
            base.OnPause();
            LoggerFacade.Info( "服务“{0}”暂停结束".FormatString(GlobalParams.ServiceName));
        }

        protected override void OnShutdown()
        {
            LoggerFacade.Info( "服务“{0}”关闭开始".FormatString(GlobalParams.ServiceName));
            base.OnShutdown();
            LoggerFacade.Info( "服务“{0}”关闭开始".FormatString(GlobalParams.ServiceName));
        }

        protected override void OnSessionChange(SessionChangeDescription changeDescription)
        {
            base.OnSessionChange(changeDescription);
        }
    }
}

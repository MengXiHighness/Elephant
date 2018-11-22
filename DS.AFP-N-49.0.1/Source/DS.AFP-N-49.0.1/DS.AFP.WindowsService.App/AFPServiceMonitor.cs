using DS.AFP.Common.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;

namespace DS.AFP.WindowsService.App
{
    partial class AFPServiceMonitor : ServiceBase
    {
       private  static Timer timer = null;
       private ServiceInfo serviceInfo = null;
       private ILoggerFacade logger;
       public AFPServiceMonitor( ServiceInfo serviceInfo)
        {
            InitializeComponent();
            this.serviceInfo = serviceInfo;            
          
        }

        protected override void OnStart(string[] args)
        {
//#if DEBUG

//            Debugger.Launch();    //Launches and attaches a debugger to the process.

//#endif
            EventLog log = new EventLog("DSLOG");
            if (!EventLog.SourceExists("DS.AFP.WindowsService"))
                EventLog.CreateEventSource("DS.AFP.WindowsService", "DSLOG");
            log.Source = "DS.AFP.WindowsService";
            log.WriteEntry("监控开始 OnStart", EventLogEntryType.Information);
            // TODO:  在此处添加代码以启动服务。
            if (timer == null)
            {
                timer = new Timer(new TimerCallback((o) =>
                {
                    IServiceManage sm = new ServiceManage();
                    //ServiceInfo si = o as ServiceInfo;
                    //if (sm.IsExistService(serviceInfo))
                    //{
                    //    sm.CreateService(serviceInfo);
                    //}
                    ServiceState ss = sm.QueryService(serviceInfo);
                    if (ss != null)
                    {
                        //如果状态停止则需要启动该服务
                        if (ss.State.IndexOf("1  STOPPED") != -1)
                        {
                            sm.StartService(serviceInfo);
                        }
                    }
                    else
                    {
                        sm.CreateService(serviceInfo);
                    }
                }), null,100000,12000);
            }
            base.OnStart(args);
        }

        protected override void OnStop()
        {
            // TODO:  在此处添加代码以执行停止服务所需的关闭操作。
            EventLog log = new EventLog("DSLOG");
            if (!EventLog.SourceExists("DS.AFP.WindowsService"))
                EventLog.CreateEventSource("DS.AFP.WindowsService", "DSLOG");
            log.Source = "DS.AFP.WindowsService";
            log.WriteEntry("监控开始 OnStop", EventLogEntryType.Information);

            timer = null;
            base.OnStop();
        }
    }
}

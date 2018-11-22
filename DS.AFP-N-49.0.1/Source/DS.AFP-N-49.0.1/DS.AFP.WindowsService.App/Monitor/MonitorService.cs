using DS.AFP.Common.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace DS.AFP.WindowsService.App
{
    partial class MonitorService : ServiceBase
    {
        public MonitorService( )
        {
            InitializeComponent();
        }


        protected override void OnStart(string[] args)
        {
            //#if DEBUG
            //            Debugger.Launch();    //Launches and attaches a debugger to the process.
            //#endif
            //LoggerFacade.Info("监控服务“{0}”启动开始".FormatString(GlobalParams.ServiceName + "Monitor"));
            ////MonitorManage mm = new MonitorManage();
            ////mm.Execute();
            base.OnStart(args);
            //LoggerFacade.Info("监控服务“{0}”启动结束".FormatString(GlobalParams.ServiceName + "Monitor"));
        }


        protected override void OnStop()
        {
            //LoggerFacade.Info("监控服务“{0}”停止开始".FormatString(GlobalParams.ServiceName + "Monitor"));
            base.OnStop();
            //LoggerFacade.Info("监控服务“{0}”停止结束".FormatString(GlobalParams.ServiceName + "Monitor"));
        }

        protected override void OnContinue()
        {
            //LoggerFacade.Info("监控服务“{0}”继续开始".FormatString(GlobalParams.ServiceName + "Monitor"));
            base.OnContinue();
            //LoggerFacade.Info("监控服务“{0}”继续结束".FormatString(GlobalParams.ServiceName + "Monitor"));
        }

     
    }
}

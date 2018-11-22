using System;
using System.Collections.Generic;
using System.Configuration.Install;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Windows.Forms;
using DS.AFP.Common.Core;
using System.Configuration;
using DS.AFP.Common.Core.ConfigurationNameSpace;
using System.Diagnostics;
//using System.Windows;

namespace DS.AFP.WindowsService.App
{
    static class Program
    {

        public const string HOSTNAME = "DS.AFP.WindowsService.App";

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            EventLog log = new EventLog("DSLOG");
            if (!EventLog.SourceExists("DS.AFP.WindowsService"))
                EventLog.CreateEventSource("DS.AFP.WindowsService", "DSLOG");
            log.Source = "DS.AFP.WindowsService";
            log.WriteEntry("应用程序主入口 参数长度:" + args.Length, EventLogEntryType.Information);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //if (args != null && args.Length > 0)
            //    Application.Run(new ServiceConsole("monitor"));
            //else
                Application.Run(new ServiceConsole("main"));

           
        }
    }
}

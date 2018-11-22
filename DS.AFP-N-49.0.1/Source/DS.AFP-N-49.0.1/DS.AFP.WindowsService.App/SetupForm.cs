using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Reflection;
using System.Configuration.Install;
using System.ServiceProcess;
using DS.AFP.Common.Core;
using DS.AFP.Common.Core.ConfigurationNameSpace;
using System.Windows;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DS.AFP.WindowsService.App
{
    public delegate void LogDelegate(string logInfo);
    public partial class ServiceConsole : Form
    {
        bool IsExsitService = false;
        ServiceController currentService = null;
        private LogDelegate logDeleage;
        private ILoggerFacade LoggerFacade;

        private ServiceInfo serviceInfo = null;
       

        bool IsMonitor = false;

       

        public ServiceConsole(string isMonitor)
        {
            InitializeComponent();

            try
            {
                InitConfiguration(isMonitor);
            }
            catch (Exception ex)
            {

            }
            switch(isMonitor)
            {
                case "monitor":
                    Setup(isMonitor);
                    
                break;
                case "main":
                LoggerFacade = new LoggerFacade(Program.HOSTNAME);
                    //logDeleage = new LogDelegate((o) =>
                    //{
                    //    txtLogBox.Text += txtLogBox.Text.IsNullOrEmpty() ? o : "\r\n" + o;
                    //});

                    //LogTraceClient.LogEvent += LogTraceClient_LogEvent;
                    Setup(isMonitor);
                break;
            }

           
        }

        //public void AutoStart()
        //{
        //    if ((new SelfInstaller(LoggerFacade)).InstallMe())
        //    {
        //        currentService = new System.ServiceProcess.ServiceController(GlobalParams.ServiceName);
        //    }
        //    /*启动服务*/
        //    if (currentService != null)
        //    {
        //        if (currentService.Status != System.ServiceProcess.ServiceControllerStatus.Running && currentService.Status != System.ServiceProcess.ServiceControllerStatus.StartPending)
        //        {
        //            currentService.Start();
        //            currentService.Refresh();
        //        }
        //    }
        //}

        void LogTraceClient_LogEvent(object sender, LogTraceInfo e)
        {
            if (txtLogBox != null)
            {
                txtLogBox.BeginInvoke(logDeleage, e.Message);
            }
        }

        private void SetupForm_Load(object sender, EventArgs e)
        {
            new Thread(new ThreadStart(Init)).Start();
        }

        private void Init()
        {
            this.currentService = new ServiceController(GlobalParams.ServiceName);
            IServiceManage sm = new ServiceManage();
            ServiceState ss = sm.QueryService(new ServiceInfo() { ServiceName = GlobalParams.ServiceName });
            if (ss == null)
            {
                this.currentService = null;
            }
           


            ChangeButtonStatus();
        }

        private void InitConfiguration(string isMonitor)
        {
            DsConfigurationManager dscm = new DsConfigurationManager();
            var config = dscm.Get<DsConfigurationSection>(AppDomain.CurrentDomain.BaseDirectory + "DS.AFP.WindowsService.App.exe.config", "ds/base");
            GlobalParams.WindowsService = ConfigurationManager.GetSection("ds/windowsService") as WindowsServiceConfigurationSection;
            if (GlobalParams.WindowsService.Service.ServiceName.IsNullOrEmpty())
            {
                if (LoggerFacade.IsErrorEnabled)
                {
                    LoggerFacade.Error("DS服务名称配置节点“serviceName”为空,请配置该节点");
                }
                throw new Exception("DS服务名称配置节点“serviceName”为空");
                //    LogTraceClient.WriteLog(LogTraceType.Info, "{0} DS服务名称配置节点“serviceName”为空，\r\n将采用默认服务名：DSWindowService，默认服务显示名：DSWindows服务".FormatString(DateTime.Now.ToString("HH:mm:ss fff")));
            }
            else
            {
                GlobalParams.ServiceName = GlobalParams.WindowsService.Service.ServiceName;
                GlobalParams.DisplayName = GlobalParams.WindowsService.Service.DisplayName;

            }
        }

        private void Setup(string isMonitor)
        {
            //IDsEnvironment Environment = new DsEnvironment()
            //{
            //    EnvironmentType = EnvironmentType.WindowsService,
            //    HostName = EnvironmentType.WindowsService.ToDescription()
            //};

            serviceInfo = new ServiceInfo()
            {
                ServiceName = GlobalParams.ServiceName,
                MapPath = Assembly.GetExecutingAssembly().Location
            };

           

            System.ServiceProcess.ServiceBase[] ServicesToRun;
           
            ServicesToRun = new System.ServiceProcess.ServiceBase[] { new AFPService(LoggerFacade) };
            EventLog log = new EventLog("DSLOG");
            if (!EventLog.SourceExists("DS.AFP.WindowsService"))
                EventLog.CreateEventSource("DS.AFP.WindowsService", "DSLOG");
            log.Source = "DS.AFP.WindowsService";
            log.WriteEntry("主体服务正在初始化 new AFPService() " + isMonitor, EventLogEntryType.Information);

            if (LoggerFacade.IsDebugEnabled)
            {
                LoggerFacade.Debug("{0} 服务已经创建并开始Run，该服务路径：{1}".FormatString(serviceInfo.ServiceName,serviceInfo.MapPath));
            }
            ServiceBase.Run(ServicesToRun);

        }

        /// <summary>
        /// 注册、卸载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAction_Click(object sender, EventArgs e)
        {
            new Thread(new ThreadStart(RegisterService)).Start();
        }

        /// <summary>
        /// 启动、停止
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            switch (btnStart.Text)
            {
                case "启 动":
                    {
                        new Thread(new ThreadStart(StartService)).Start();
                        //StartService();
                    } break;
                case "停 止":
                    {
                        new Thread(new ThreadStart(StopService)).Start();
                        //StopService();

                    } break;
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DisableButtonStatus()
        {
            //btnAction.Enabled = false;
            //btnStart.Enabled = false;
            //btnCancel.Enabled = false;

            btnAction.BeginInvoke(new EventHandler((o, e) =>
            {
                var btn = (Button)o;

                btn.Enabled = false;
            }));

            btnStart.BeginInvoke(new EventHandler((o, e) =>
            {
                var btn = (Button)o;

                btn.Enabled = false;
            }));// Enabled = true;
            btnCancel.BeginInvoke(new EventHandler((o, e) =>
            {
                var btn = (Button)o;

                btn.Enabled = false;
            }));
        }

        private void ChangeButtonStatus()
        {
            if (this.currentService != null)
            {
                btnAction.BeginInvoke(new EventHandler((o, e) =>
                {
                    var btn = (Button)o;

                    btn.Text = "卸 载";
                }));// = "卸 载";

                switch (this.currentService.Status)
                {
                    case ServiceControllerStatus.Running:
                        {
                            btnStart.BeginInvoke(new EventHandler((o, e) =>
                            {
                                var btn = (Button)o;

                                btn.Text = "停 止";
                                btn.Enabled = true;
                            }));

                            btnAction.BeginInvoke(new EventHandler((o, e) =>
                            {
                                var btn = (Button)o;

                                btn.Enabled = false;
                            }));
                        } break;
                    default:
                        {
                            btnStart.BeginInvoke(new EventHandler((o, e) =>
                            {
                                var btn = (Button)o;

                                btn.Text = "启 动";
                                btn.Enabled = true;
                            }));

                            btnAction.BeginInvoke(new EventHandler((o, e) =>
                            {
                                var btn = (Button)o;

                                btn.Enabled = true;
                            })); //Enabled = false;

                            //btnStart.Text = "启 动";
                            //btnStart.Enabled = true;
                            //btnAction.Enabled = true;
                        } break;
                }
            }
            else
            {
                //btnAction.Text = "安 装";

                //btnStart.Enabled = false;
                //btnAction.Enabled = true;

                btnAction.BeginInvoke(new EventHandler((o, e) =>
                {
                    var btn = (Button)o;

                    btn.Text = "安 装";
                    btn.Enabled = true;
                }));

                btnStart.BeginInvoke(new EventHandler((o, e) =>
                {
                    var btn = (Button)o;

                    btn.Enabled = false; ;
                }));

            }

            //btnCancel.Enabled = true;

            btnCancel.BeginInvoke(new EventHandler((o, e) =>
            {
                var btn = (Button)o;

                btn.Enabled = true; ;
            }));
        }

        public void RegisterService()
        {
            DisableButtonStatus();

            IServiceManage sm = new ServiceManage();

            //this.progressBar1.Minimum = 0;
            //this.progressBar1.Value = 0;
            //this.progressBar1.Maximum = 1000;
            progressBar1.BeginInvoke(new EventHandler((o, e) =>
            {
                var progbar = (ProgressBar)o;

                progbar.Minimum = 0;
                progbar.Value = 0;
                progbar.Maximum = 1000;
            }));

            if (currentService != null)
            {
                // Thanks to PIEBALDconsult's Concern V2.0

                LogTraceClient.WriteLog(LogTraceType.Info, "{0} 卸载服务“{1}”开始".FormatString(DateTime.Now.ToString("HH:mm:ss fff"), GlobalParams.ServiceName));
                              
                if (sm.DeleteService(serviceInfo))
                {
                    progressBar1.BeginInvoke(new EventHandler((o, e) =>
                    {
                        var progbar = (ProgressBar)o;

                        for (int i = 0; i < 1000; i++)
                        {
                            progbar.Value++;
                        }
                    }));

                    currentService = null;
                    IsExsitService = false;
                    ChangeButtonStatus();

                    LogTraceClient.WriteLog(LogTraceType.Info, "{0} 服务“{1}”已经成功卸载".FormatString(DateTime.Now.ToString("HH:mm:ss fff"), GlobalParams.ServiceName));
                }
                else
                {
                    LogTraceClient.WriteLog(LogTraceType.Info, "{0} 服务“{1}”已经卸载失败".FormatString(DateTime.Now.ToString("HH:mm:ss fff"), GlobalParams.ServiceName));
                }

                LogTraceClient.WriteLog(LogTraceType.Info, "{0} 卸载服务“{1}”结束".FormatString(DateTime.Now.ToString("HH:mm:ss fff"), GlobalParams.ServiceName));
            }
            else
            {
                LogTraceClient.WriteLog(LogTraceType.Info, "{0} 安装服务“{1}”开始".FormatString(DateTime.Now.ToString("HH:mm:ss fff"), GlobalParams.ServiceName));
               

               if (sm.CreateService(serviceInfo) )
                {
                    progressBar1.BeginInvoke(new EventHandler((o, e) =>
                    {
                        var progbar = (ProgressBar)o;

                        for (int i = 0; i < 1000; i++)
                        {
                            progbar.Value++;
                        }
                    }));

                    currentService = new System.ServiceProcess.ServiceController(GlobalParams.ServiceName);
                    IsExsitService = true;
                    ChangeButtonStatus();

                    LogTraceClient.WriteLog(LogTraceType.Info, "{0} 服务“{1}”已经注册成功".FormatString(DateTime.Now.ToString("HH:mm:ss fff"), GlobalParams.ServiceName));
                }
                else
                {
                    LogTraceClient.WriteLog(LogTraceType.Info, "{0} 服务“{1}”注册失败".FormatString(DateTime.Now.ToString("HH:mm:ss fff"), GlobalParams.ServiceName));
                }

                LogTraceClient.WriteLog(LogTraceType.Info, "{0} 安装服务“{1}”结束".FormatString(DateTime.Now.ToString("HH:mm:ss fff"), GlobalParams.ServiceName));
            }
        }

        /// <summary>  
        /// 启动服务（启动存在的服务，60秒后启动失败报错）  
        /// </summary>
        public void StartService()
        {
           
            DisableButtonStatus();
            int currentProgress = 0;
            LogTraceClient.WriteLog(LogTraceType.Info, "{0} 服务“{1}”启动开始".FormatString(DateTime.Now.ToString("HH:mm:ss fff"), GlobalParams.ServiceName));
            progressBar1.BeginInvoke(new EventHandler((o, e) =>
                       {
                           var progbar = (ProgressBar)o;

                           progbar.Minimum = 0;
                           progbar.Value = 10;
                           progbar.Maximum = 1000;
                           currentProgress = progbar.Value;
                       }));

            if (currentService != null)
            {
                if (currentService.Status != System.ServiceProcess.ServiceControllerStatus.Running && currentService.Status != System.ServiceProcess.ServiceControllerStatus.StartPending)
                {
                    try
                    {
                        //currentService.Start();
                       
                        IServiceManage sm = new ServiceManage();
                       if( sm.StartService(serviceInfo))
                           LoggerFacade.Error("{0}服务启动异常".FormatString( serviceInfo.ServiceName));
                       
                    }
                    catch (Exception ex)
                    {
                        LoggerFacade.Error("服务启动异常",ex);
                        return;
                    }
                    for (int i = 1; i <= 100; i++)
                    {
                        //this.progressBar1.Value = i * 10;
                        progressBar1.BeginInvoke(new EventHandler((o, e) =>
                        {
                            var progbar = (ProgressBar)o;
                            progbar.Value = i * 10;
                            currentProgress = progbar.Value;
                        }));

                        currentService.Refresh();
                        System.Threading.Thread.Sleep(100);
                        if (currentService.Status == System.ServiceProcess.ServiceControllerStatus.Running)
                        {
                            progressBar1.BeginInvoke(new EventHandler((o, e) =>
                            {
                                var progbar = (ProgressBar)o;
                                progbar.Value = 1000;
                                currentProgress = progbar.Value;
                            }));
                            break;
                        }
                        if (i == 100)
                        {
                            System.Threading.Thread.Sleep(1000);
                            for (int k = currentProgress; k >= 0; k -= 20)
                            {
                                System.Threading.Thread.Sleep(100);
                                progressBar1.BeginInvoke(new EventHandler((o, e) =>
                                {

                                    var progbar = (ProgressBar)o;
                                    if (k < 0)
                                        k = 0;
                                    progbar.Value = k;

                                }));

                            }
                            currentProgress = 0;
                            // throw new Exception("服务" + serviceName + "启动失败！");
                            LogTraceClient.WriteLog(LogTraceType.Info, "{0} 服务“{1}”启动已超时".FormatString(DateTime.Now.ToString("HH:mm:ss fff"), GlobalParams.ServiceName));
                            LogTraceClient.WriteLog(LogTraceType.Info, "{0} 服务“{1}”启动异常，详细请看日志描述".FormatString(DateTime.Now.ToString("HH:mm:ss fff"), GlobalParams.ServiceName));
                            ChangeButtonStatus();
                            return;
                        }
                    }
                }
            }
            LogTraceClient.WriteLog(LogTraceType.Info, "{0} 服务“{1}”启动结束".FormatString(DateTime.Now.ToString("HH:mm:ss fff"), GlobalParams.ServiceName));

            ChangeButtonStatus();
        }

        /// <summary>  
        /// 停止服务
        /// </summary>  
        /// <param name="serviceName">服务名</param>  
        public void StopService()
        {
            DisableButtonStatus();

            LogTraceClient.WriteLog(LogTraceType.Info, "{0} 停止服务“{1}”开始".FormatString(DateTime.Now.ToString("HH:mm:ss fff"), GlobalParams.ServiceName));

            progressBar1.BeginInvoke(new EventHandler((o, e) =>
            {
                var progbar = (ProgressBar)o;

                progbar.Minimum = 0;
                progbar.Value = 0;
                progbar.Maximum = 1000;
            }));

            if (currentService != null)
            {
                if (currentService.Status == System.ServiceProcess.ServiceControllerStatus.Running)
                {
                    System.DateTime startTime = System.DateTime.Now;
                   
                    IServiceManage sm = new ServiceManage();
            
                    if (sm.StopService(serviceInfo))
                        LoggerFacade.Error("{0}服务停止".FormatString(serviceInfo.ServiceName));
                    //bool isExecute = true;
                    //while (isExecute)
                    //{
                    //    if ((System.DateTime.Now - startTime).TotalSeconds < 120)
                    //    {
                    //        if (currentService.CanStop)
                    //        {
                    //            currentService.Stop();
                    //            isExecute = false;
                    //        }
                    //        currentService.Refresh();

                    //    }
                    //    else
                    //    {
                    //        LogTraceClient.WriteLog(LogTraceType.Info, "{0} 服务“{1}”停止失败，已经超时".FormatString(DateTime.Now.ToString("HH:mm:ss fff"), GlobalParams.ServiceName));

                    //    }
                    //}

                    bool isExecute2 = true;
                    while (isExecute2)
                    {
                        if ((System.DateTime.Now - startTime).TotalSeconds < 120)
                        {

                            if (currentService.Status == System.ServiceProcess.ServiceControllerStatus.Stopped)
                            {
                                progressBar1.BeginInvoke(new EventHandler((o, e) =>
                                {
                                    var progbar = (ProgressBar)o;
                                    progbar.Value = 970;
                                }));
                                
                                Process[] serviceProcess = System.Diagnostics.Process.GetProcesses();
                                int count = 0;
                                foreach(var p in serviceProcess)
                                {
                                    if(p.Id!=0)
                                    {
                                        if (p.ProcessName == Program.HOSTNAME)
                                        {
                                            count++;
                                        }
                                    }
                                }
                                if(count==1)
                                    isExecute2 = false;
                            }
                            else
                            {
                                sm.StopService(serviceInfo);
                                Thread.Sleep(1000);
                                progressBar1.BeginInvoke(new EventHandler((o, e) =>
                                {
                                    var progbar = (ProgressBar)o;
                                    if (progbar.Value<=940)
                                        progbar.Value += 30;
                                }));
                                

                            }

                            currentService.Refresh();

                        }
                        else
                        {
                            isExecute2 = false;
                            LogTraceClient.WriteLog(LogTraceType.Info, "{0} 服务“{1}”服务已经停止，但进程卸载失败".FormatString(DateTime.Now.ToString("HH:mm:ss fff"), GlobalParams.ServiceName));

                        }
                    }          
                    
                }
                
            }
            LogTraceClient.WriteLog(LogTraceType.Info, "{0} 停止服务“{1}”结束".FormatString(DateTime.Now.ToString("HH:mm:ss fff"), GlobalParams.ServiceName));
            progressBar1.BeginInvoke(new EventHandler((o, e) =>
            {
                var progbar = (ProgressBar)o;
                progbar.Value = 1000;
            }));
            ChangeButtonStatus();
        }

        public  class SelfInstaller
        {
            private ILoggerFacade Logger = null;
            public SelfInstaller(ILoggerFacade Logger)
            {
                this.Logger = Logger;
            }
            private static readonly string _exePath = Assembly.GetExecutingAssembly().Location;
            public bool InstallMe()
            {
                try
                {
                    ManagedInstallerClass.InstallHelper(
                        new string[] { _exePath });
                }
                catch (Exception e)
                {
                    Logger.Debug("安装服务失败",e);
                    System.Windows.MessageBox.Show("安装服务失败！{0}".FormatString(e.InnerException.StackTrace));
                    return false;
                }
                return true;
            }

            public bool UninstallMe()
            {
                try
                {
                    ManagedInstallerClass.InstallHelper(
                        new string[] { "/u", _exePath });
                }
                catch(Exception e)
                {
                    Logger.Debug("卸载安装服务失败", e);
                    return false;
                }
                return true;
            }
        }
    }
}

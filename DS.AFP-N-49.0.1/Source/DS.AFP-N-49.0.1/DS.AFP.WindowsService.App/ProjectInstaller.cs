using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using DS.AFP.Common.Core.ConfigurationNameSpace;
using System.Configuration;

namespace DS.AFP.WindowsService.App
{
    [RunInstaller(true)]
    public class ProjectInstaller : System.Configuration.Install.Installer
    {
        private ServiceProcessInstaller process;
        public ServiceInstaller serviceInstaller1;
    
        public ProjectInstaller()
        {
            InitializeComponent();

        }

        private void InitializeComponent()
        {
            this.process = new System.ServiceProcess.ServiceProcessInstaller();
            this.serviceInstaller1 = new System.ServiceProcess.ServiceInstaller();
            // 
            // process
            // 
            this.process.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.process.Password = null;
            this.process.Username = null;
            // 
            // serviceInstaller1
            // 
            this.serviceInstaller1.DisplayName = "DSWindows服务";
            this.serviceInstaller1.ServiceName = "DSWindowService";
            this.serviceInstaller1.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.process,
            this.serviceInstaller1});

        }
    }
}

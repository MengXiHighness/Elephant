using DS.AFP.Common.Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.AFP.Common.Core;
using System.ServiceProcess;

namespace DS.AFP.WindowsService.App
{
    public class ServiceManage:IServiceManage
    {
        private CmdHelper cmdHelper = new CmdHelper();
        public bool CreateService(ServiceInfo serviceInfo)
        {
            if (IsExistService(serviceInfo))
            {
                DeleteService(serviceInfo);
            }
            StringBuilder cmdStr = new StringBuilder();
            cmdStr.Append("sc create {0} binpath= {1} type= {2} start= auto".FormatString(serviceInfo.ServiceName, serviceInfo.MapPath, "own"));

            string result = cmdHelper.Input(cmdStr.ToString());
            if (result.IndexOf("[SC] CreateService") != -1)
                return true;
            else
                return false;
        }

        public bool DeleteService(ServiceInfo serviceInfo)
        {
            StopService(serviceInfo);
            StringBuilder cmdStr = new StringBuilder();
            cmdStr.Append("sc delete {0} ".FormatString(serviceInfo.ServiceName));

            string result = cmdHelper.Input(cmdStr.ToString());
            if (result.IndexOf("[SC] DeleteService") != -1)
                return true;
            else
                return false;
        }

        public bool StartService(ServiceInfo serviceInfo)
        {
            //StringBuilder cmdStr = new StringBuilder();
            //cmdStr.Append("sc start {0} ".FormatString(serviceInfo.ServiceName));

            //string result = cmdHelper.Input(cmdStr.ToString());
            //if (result.IndexOf("[SC] StartService") != -1)
            //    return true;
            //else
            //    return false;
            ServiceController sc = new ServiceController(serviceInfo.ServiceName);
            if (sc != null)
            {
                sc.Start();
                return true;
            }

            return false;
        }

        public bool StopService(ServiceInfo serviceInfo)
        {
            StringBuilder cmdStr = new StringBuilder();
            cmdStr.Append("sc stop {0} ".FormatString(serviceInfo.ServiceName));

            string result = cmdHelper.Input(cmdStr.ToString());
            if (result.IndexOf("[SC] StopService") != -1)
                return true;
            else
                return false;
        }

        public void PauseService(ServiceInfo serviceInfo)
        {
            StringBuilder cmdStr = new StringBuilder();
            cmdStr.Append("sc pause {0} ".FormatString(serviceInfo.ServiceName));

            string result = cmdHelper.Input(cmdStr.ToString());
        }


        public bool IsExistService(ServiceInfo serviceInfo)
        {
            StringBuilder cmdStr = new StringBuilder();
            cmdStr.Append("sc query {0} ".FormatString(serviceInfo.ServiceName));
            string result = cmdHelper.Input(cmdStr.ToString());
            if (result.IndexOf("1060") != -1)
                return false;
            else
                return true;
        }




        public ServiceState QueryService(ServiceInfo serviceInfo)
        {
            StringBuilder cmdStr = new StringBuilder();
            cmdStr.Append("sc query {0} ".FormatString(serviceInfo.ServiceName));
            string result = cmdHelper.Input(cmdStr.ToString());
            if (result.IndexOf("1060") != -1)
                return null;
            else
            {
                string[] state = result.Split("\r\n");
                if(state!=null && state.Length>0)
                {
                    ServiceState ss = new ServiceState()
                    {
                        Name = state[5],
                        Type = state[6],
                        State = state[7],
                        Win32_Exit_Code = state[8]
                    };
                    return ss;
                }
                return null;
            }
        }
    }

    public interface IServiceManage
    {
        bool CreateService(ServiceInfo serviceInfo);

        bool DeleteService(ServiceInfo serviceInfo);

        bool StartService(ServiceInfo serviceInfo);

        bool StopService(ServiceInfo serviceInfo);

        void PauseService(ServiceInfo serviceInfo);

        bool IsExistService(ServiceInfo serviceInfo);

        ServiceState QueryService(ServiceInfo serviceInfo);

    }

    public class ServiceState
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string State { get; set; }
        public string Win32_Exit_Code { get; set; }
        
    }

    public class ServiceInfo
    {
        public string ServiceName
        {
            get;
            set;
        }
        public string MapPath
        {
            get;
            set;
        }

        
    }
}

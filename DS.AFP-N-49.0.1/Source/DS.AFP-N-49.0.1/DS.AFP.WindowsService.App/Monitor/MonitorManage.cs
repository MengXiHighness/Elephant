using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;

namespace DS.AFP.WindowsService.App.Monitor
{
    public class MonitorManage
    {
        private static Timer timer = null;
        public void Execute()
        {
            if (timer != null)
            {
                timer = new Timer(new TimerCallback((o) =>
                {

                }), null, 10000, 10);
            }
        }

        private void StartService()
        {
            ServiceController[] services = ServiceController.GetServices();

           // this.currentService = services.Where(s => s.ServiceName == GlobalParams.ServiceName).FirstOrDefault();
        }
    }
}

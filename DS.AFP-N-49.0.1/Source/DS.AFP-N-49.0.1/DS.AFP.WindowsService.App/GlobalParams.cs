using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.AFP.Common.Core.ConfigurationNameSpace;

namespace DS.AFP.WindowsService.App
{
    public class GlobalParams
    {
        public static WindowsServiceConfigurationSection WindowsService { get; set; }
        public static string ServiceName = "DSWindowService";
        public static string DisplayName = "DSWindows服务";
    }
}

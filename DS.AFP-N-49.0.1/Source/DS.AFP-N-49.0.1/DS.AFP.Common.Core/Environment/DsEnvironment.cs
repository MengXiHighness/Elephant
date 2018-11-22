using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Globalization;
using System.Threading;

namespace DS.AFP.Common.Core
{
    /// <summary>
    /// 平台环境参数（主属性有环境类型、宿主名称、插件信息集合、共享数据）
    /// <code>
    ///  this.Environment = new DsEnvironment()
    /// {
    /// EnvironmentType = EnvironmentType.WPF,
    /// HostName = EnvironmentType.WPF.ToDescription()
    /// };
    /// </code>
    /// </summary>
    public class DsEnvironment : IDsEnvironment
    {
        private static EnvironmentType environmentType ;

        static DsEnvironment()
        {
            
        }

        /// <summary>
        /// 环境类型
        /// </summary>
        public EnvironmentType EnvironmentType
        {
            get
            {
                return environmentType;
            }
            set
            {
                environmentType = value;
            }
        }

        private static string hostName = "";

        /// <summary>
        /// 宿主名称
        /// </summary>
        public string HostName
        {
            get
            {
                return hostName;
            }
            set
            {
                hostName = value;
            }
        }

        private static ThreadSafeDictionary<string, AddinInfo> addinInfos = new ThreadSafeDictionary<string,AddinInfo>(new Dictionary<string, AddinInfo>());

        /// <summary>
        /// 插件信息集合
        /// </summary>
        public ThreadSafeDictionary<string, AddinInfo> AddinInfos
        {
            get
            {
                return addinInfos;
            }           
        }
        private static ThreadSafeDictionary<string, object> shareData = new ThreadSafeDictionary<string, object>(new Dictionary<string, object>());

        /// <summary>
        /// 共享数据
        /// </summary>
        public ThreadSafeDictionary<string, object> ShareData
        {
            get
            {
                return shareData;
            }
            set
            {
                shareData = value;
            }
        }



        private CultureInfo culture;
        public System.Globalization.CultureInfo Culture
        {
            get
            {
                return culture;
            }
            set
            {
                if (value != culture)
                {
                    Thread.CurrentThread.CurrentUICulture = value;
                    SetThreadCulture(value);
                    culture = value;

                }
            }
        }

        private void SetThreadCulture(CultureInfo value)
        {
            if (value.IsNeutralCulture)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("zh-cn");
            }
            else
            {
                Thread.CurrentThread.CurrentCulture = value;
            }
        }
    }
   
    
    /// <summary>
    /// 宿主应用程序类型（WindowsService、WebForm、WebMvc、WPF）
    /// </summary>
    public enum EnvironmentType
    {
        [DescriptionAttribute("DS.AFP.WindowsService.App")]
        WindowsService = 0,
        [DescriptionAttribute("DS.AFP.WebForm.App")]
        WebForm = 1,
        [DescriptionAttribute("DS.AFP.WebMvc.App")]
        WebMvc = 2,
        [DescriptionAttribute("DS.AFP.WPF.App")]
        WPF = 3
    }


   
}

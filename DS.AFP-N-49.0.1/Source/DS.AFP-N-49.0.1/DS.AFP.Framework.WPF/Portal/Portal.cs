using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.AFP.Framework.Regions;
using DS.AFP.Framework.ViewModel;
using DS.AFP.Common.Core.ConfigurationNameSpace;

namespace DS.AFP.Framework.WPF.Portal
{
    /// <summary>
    /// 系统主界面入口
    /// </summary>
    public class MainPortal : ViewModelBase, IPortal
    {
        public static IRegionManager PortalRegionManager
        {
            get;
            private set;
        }

        public MainPortal(IRegionManager regionManager)
        {
            PortalRegionManager = regionManager;
        }

        private string logo;
        /// <summary>
        /// logo图片路径（）
        /// </summary>
        public string Logo
        {
            get { return logo; }
            set
            {
                if (logo != value)
                {
                    logo = value; base.RaisePropertyChanged("Logo");
                }
            }
        }


        private string imgBackground;
        /// <summary>
        /// 背景图片路径
        /// </summary>
        public string ImgBackground
        {
            get { return imgBackground; }
            set
            {
                if (imgBackground != value)
                {
                    imgBackground = value; base.RaisePropertyChanged("ImgBackground");
                }
            }
        }

        private string comapnyInfo;
        /// <summary>
        ///公司信息
        /// </summary>
        public string ComapnyInfo
        {
            get { return comapnyInfo; }
            set
            {
                if (comapnyInfo != value)
                {
                    comapnyInfo = value; base.RaisePropertyChanged("ComapnyInfo");
                }
            }
        }

        private string comapnyLogo;
        /// <summary>
        /// 公司Logo
        /// </summary>
        public string ComapnyLogo
        {
            get { return comapnyLogo; }
            set
            {
                if (comapnyLogo != value)
                {
                    comapnyLogo = value; base.RaisePropertyChanged("ComapnyLogo");
                }
            }
        }

        private string systemVersion;
        /// <summary>
        /// 系统版本
        /// </summary>
        public string SystemVersion
        {
            get { return systemVersion; }
            set
            {
                if (systemVersion != value)
                {
                    systemVersion = value;
                    base.RaisePropertyChanged("SystemVersion");
                }
            }
        }

        private string licenseMsg;
        /// <summary>
        /// 系统版本
        /// </summary>
        public string LicenseMsg
        {
            get { return licenseMsg; }
            set
            {
                if (licenseMsg != value)
                {
                    licenseMsg = value;
                    base.RaisePropertyChanged("LicenseMsg");
                }
            }
        }

        private string title;
        /// <summary>
        /// 门户标题
        /// </summary>
        public string Title
        {
            get { return title; }
            set
            {
                if (title != value)
                {
                    title = value;
                    base.RaisePropertyChanged("Title");
                }
            }
        }

        private Navigation navigation = new Navigation();
        /// <summary>
        /// 导航菜单
        /// </summary>
        public Navigation Navigation
        {
            get { return navigation; }
            set
            {
                if (navigation != value)
                {
                    navigation = value;
                    base.RaisePropertyChanged("Navigation");
                }
            }
        }

        private Navigation bottomNavigation = new Navigation();
        /// <summary>
        /// 底部导航菜单
        /// </summary>
        public Navigation BottomNavigation
        {
            get { return bottomNavigation; }
            set
            {
                if (bottomNavigation != value)
                {
                    bottomNavigation = value;
                    base.RaisePropertyChanged("BottomNavigation");
                }
            }
        }

        private ToolBar toolbar = new ToolBar();

        /// <summary>
        /// 工具栏
        /// </summary>
        public ToolBar ToolBar
        {
            get
            {
                return toolbar;
            }
            set
            {
                if (toolbar != value)
                {
                    toolbar = value;
                    base.RaisePropertyChanged("ToolBar");
                }
            }
        }

        private StatusBar statusBar = new StatusBar();
        /// <summary>
        /// 状态栏
        /// </summary>
        public StatusBar StatusBar
        {
            get
            {
                return statusBar;
            }
            set
            {
                if (statusBar != value)
                {
                    statusBar = value;
                    base.RaisePropertyChanged("StatusBar");
                }
            }
        }

        private Object body;
        /// <summary>
        /// 窗体展示区（容器类）
        /// </summary>
        public Object Body
        {
            get
            {
                return body;
            }
            set
            {
                if (body != value)
                {
                    body = value;
                    base.RaisePropertyChanged("Body");
                }
            }
        }
    }
}

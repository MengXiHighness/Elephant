using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using DS.AFP.Framework.ViewModel;
using System.Windows.Input;
using DS.AFP.Framework.Commands;
using DS.AFP.Framework.Regions;
using System.Windows;

namespace DS.AFP.Framework.WPF.Portal
{
    /// <summary>
    /// 导航项
    /// </summary>
    public class NavigationItem : ViewModelBase
    {
        
        private ICommand navigationClick;

        private event EventHandler<NavigationItemArg> navigationItemEvent = null;

        public event EventHandler<NavigationItemArg> NavigationItemClick
        {
            //显式实现'add'方法
            add
            {
                navigationItemEvent += value; 
            }
            //显式实现'remove'方法
            remove
            {
                navigationItemEvent -= value; 
            }
        }

        public ICommand NavigationClick
        {
            get { return navigationClick; }
            set
            {
                if (this.navigationClick != value)
                {
                    navigationClick = value;
                    base.RaisePropertyChanged("NavigationClick");
                }
            }
        }

        public NavigationItem()
        {
            NavigationClick = new DelegateCommand<NavigationCommandParam>(new Action<NavigationCommandParam>((o) =>
            {
                try
                {
                    if (string.Equals(o.OpenStyle, "_blank"))
                    {
                        if (navigationItemEvent != null)
                            navigationItemEvent(this, new NavigationItemArg() { Index = this.index, OpenStyle = this.openStyle, OpenUri = this.openUri, Remark = this.remark, Tag = this.tag, Title = this.title });
                        if (o.OpenUri != "")
                            MainPortal.PortalRegionManager.RequestNavigate("PopupWindow", new Uri(o.OpenUri, UriKind.Relative));
                    }
                    else
                    {
                        if (navigationItemEvent != null)
                            navigationItemEvent(this, new NavigationItemArg() { Index = this.index, OpenStyle = this.openStyle, OpenUri = this.openUri, Remark = this.remark, Tag = this.tag, Title = this.title });

                        if (o.OpenUri != "")
                            MainPortal.PortalRegionManager.RequestNavigate("WindowAreaRoot", new Uri(o.OpenUri, UriKind.RelativeOrAbsolute));
                    }
                }
                catch (Exception ex)
                {
                    LoggerFacade.Error("Navigation exception!May be due to the configuration of the interface type not in the container.");
                }

            }), (o) => { return true; });

        }


        private float index;
        /// <summary>
        ///菜单索引
        /// </summary>
        public float Index
        {
            get { return index; }
            set
            {
                if (this.index != value)
                {
                    index = value;
                    base.RaisePropertyChanged("Index");
                }
            }
        }

        private string name;
        /// <summary>
        /// 主键标示
        /// </summary>
        public string Name
        {
            get { return name; }
            set
            {
                if (this.name != value)
                {
                    name = value;
                    base.RaisePropertyChanged("Name");
                }
            }
        }

        private string title;
        /// <summary>
        /// 数据标题
        /// </summary>
        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                if (this.title != value)
                {
                    this.title = value;
                    base.RaisePropertyChanged("Title");
                }
            }
        }


        private string iconUri;
        /// <summary>
        /// 图标
        /// </summary>
        public string IconUri
        {
            get
            {
                return this.iconUri;
            }
            set
            {
                if (this.iconUri != value)
                {
                    this.iconUri = value;
                    base.RaisePropertyChanged("IconUri");
                }
            }
        }

        private string openUri = "";
        /// <summary>
        /// 导航链接
        /// </summary>
        public string OpenUri
        {
            get
            {
                return this.openUri;
            }
            set
            {
                if (this.openUri != value)
                {
                    this.openUri = value;
                    base.RaisePropertyChanged("OpenUri");
                }
            }
        }

        public string openTargetType = "";

        public string OpenTargetType
        {
            get
            {
                return this.openTargetType;
            }
            set
            {
                if (this.openTargetType != value)
                {
                    this.openTargetType = value;
                    base.RaisePropertyChanged("OpenTargetType");
                }
            }
        }

        private string remark;
        /// <summary>
        /// Remark
        /// </summary>
        public string Remark
        {
            get
            {
                return this.remark;
            }
            set
            {
                if (this.remark != value)
                {
                    this.remark = value;
                    base.RaisePropertyChanged("Remark");
                }
            }
        }

        private string openStyle="";
        /// <summary>
        /// OpenStyle
        /// </summary>
        public string OpenStyle
        {
            get
            {
                return this.openStyle;
            }
            set
            {
                if (this.openStyle != value)
                {
                    this.openStyle = value;
                    base.RaisePropertyChanged("OpenStyle");
                }
            }
        }

        private Visibility isVisibility =  Visibility.Visible;
        /// <summary>
        /// OpenStyle
        /// </summary>
        public Visibility IsVisibility
        {
            get
            {
                return this.isVisibility;
            }
            set
            {
                if (this.isVisibility != value)
                {
                    this.isVisibility = value;
                    base.RaisePropertyChanged("IsVisibility");
                }
            }
        }

        private object tag;
        /// <summary>
        /// Tag
        /// </summary>
        public object Tag
        {
            get { return this.tag; }
            set
            {
                if (this.tag != value)
                {
                    this.tag = value;
                    base.RaisePropertyChanged("Tag");
                }
            }
        }
    }

    public class NavigationItemArg : EventArgs
    {
        public float Index { get; set; }

        public string OpenUri{ get;set;}

        public object Tag { get; set; }

        public string OpenStyle { get; set; }

        public string Remark { get; set; }

        public string Title { get; set; }
    }

    public class NavigationCommandParam
    {
        public string OpenUri { get; set; }
        public string OpenStyle { get; set; }
    }
}

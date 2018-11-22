using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.AFP.Framework.ViewModel;
using System.Collections.ObjectModel;
using System.Windows.Input;
using DS.AFP.Framework.Commands;
using DS.AFP.Framework.Regions;
using System.Windows;

namespace DS.AFP.Framework.WPF.Portal
{
    /// <summary>
    /// 工具栏栏项
    /// </summary>
    public class ToolBarItem : ViewModelBase
    {
        private ICommand toolbarClick;
        public ICommand ToolbarClick
        {
            get { return toolbarClick; }
            set
            {
                if (this.toolbarClick != value)
                {
                    this.toolbarClick = value;
                    base.RaisePropertyChanged("ToolbarClick");
                }
            }
        }

        private event EventHandler<ToolBarItemArg> toolBarItemEvent = null;
        public event EventHandler<ToolBarItemArg> ToolBarItemClick
        {
            //显式实现'add'方法
            add
            {
                toolBarItemEvent += value;
            }
            //显式实现'remove'方法
            remove
            {
                toolBarItemEvent -= value;
            }
        }

        public ToolBarItem()
        {
            ToolbarClick = new DelegateCommand<ToolBarCommandParam>(new Action<ToolBarCommandParam>((o) =>
            {
                try
                {
                    if (toolBarItemEvent != null)
                        toolBarItemEvent(this, new ToolBarItemArg() { Index = this.index, OpenStyle = this.openStyle, OpenUri = this.openUri, IconUri = this.iconUri, Title = this.title });
                   
                    if (string.Equals(o.OpenStyle, "_blank"))
                    {
                       
                        if (o.OpenUri != "")
                            MainPortal.PortalRegionManager.RequestNavigate("PopupWindow", new Uri(o.OpenUri, UriKind.Relative));
                    }
                    else
                    {
                        if (o.OpenUri != "")
                            MainPortal.PortalRegionManager.RequestNavigate("WindowAreaRoot", new Uri(o.OpenUri, UriKind.Relative));
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
        ///索引
        /// </summary>
        public float Index
        {
            get { return index; }
            set
            {
                if (this.index != value)
                {
                    this.index = value;
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

        private Visibility isVisibility = Visibility.Visible;
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

        private string openStyle = "";
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

        private ToolBar children = new ToolBar();
        /// <summary>
        /// 下拉项
        /// </summary>
        public ToolBar Children
        {
            get { return children; }
            set
            {
                if (this.children != value)
                {
                    this.children = value;
                    base.RaisePropertyChanged("Children");
                }
            }
        }
    }

    public class ToolBarCommandParam
    {
        public string OpenUri { get; set; }
        public string OpenStyle { get; set; }
    }

    public class ToolBarItemArg : EventArgs
    {
        public float Index { get; set; }

        public string OpenUri { get; set; }

        public string OpenStyle { get; set; }

        public string IconUri { get; set; }

        public string Title { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using DS.AFP.Framework.ViewModel;

namespace DS.AFP.Framework.WPF.Portal
{
    /// <summary>
    /// 状态数据项
    /// </summary>
    public class StatusDataItem : ViewModelBase
    {
        private float index;
        /// <summary>
        /// 索引
        /// </summary>
        public float Index
        {
            get { return index; }
            set
            {
                if (this.index != value)
                {
                    index = value; base.RaisePropertyChanged("Index");
                }
            }
        }
        private string title;
        /// <summary>
        /// 数据标题
        /// </summary>
        public string Title
        {
            get { return title; }
            set
            {
                if (this.title != value)
                {
                    title = value; base.RaisePropertyChanged("Title");
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

        private string description;
        /// <summary>
        /// 数据描述
        /// </summary>
        public string Description
        {
            get { return description; }
            set
            {
                if (this.description != value)
                {
                    description = value; base.RaisePropertyChanged("Description");
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
    }
}

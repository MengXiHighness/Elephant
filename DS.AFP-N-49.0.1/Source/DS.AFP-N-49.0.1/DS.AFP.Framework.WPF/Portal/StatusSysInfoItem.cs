using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using DS.AFP.Framework.ViewModel;

namespace DS.AFP.Framework.WPF.Portal
{
    /// <summary>
    /// 系统信息项
    /// </summary>
    public class StatusSysInfoItem : ViewModelBase
    {
        private int index;
        /// <summary>
        /// 数据序号
        /// </summary>
        public int Index
        {
            get { return index; }
            set { index = value; base.RaisePropertyChanged("Index"); }
        }

        private string dataTitle;
        /// <summary>
        /// 数据标题
        /// </summary>
        public string DataTitle
        {
            get { return dataTitle; }
            set { dataTitle = value; base.RaisePropertyChanged("DataTitle"); }
        }
        private string dataContent;
        /// <summary>
        /// 数据标题内容
        /// </summary>
        public string DataContent
        {
            get { return dataContent; }
            set { dataContent = value; base.RaisePropertyChanged("DataContent"); }
        }
    }
}

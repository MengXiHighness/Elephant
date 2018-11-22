using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using DS.AFP.Framework.ViewModel;

namespace DS.AFP.Framework.WPF.Portal
{
    /// <summary>
    /// 消息提示项
    /// </summary>
    public class StatusMsgTipItem : ViewModelBase
    {
        private int index;

        /// <summary>
        /// 索引
        /// </summary>
        public int Index
        {
            get { return index; }
            set { index = value; base.RaisePropertyChanged("Index"); }
        }

        private string dataContent;

        /// <summary>
        /// 内容
        /// </summary>
        public string DataContent
        {
            get { return dataContent; }
            set { dataContent = value; base.RaisePropertyChanged("DataContent"); }
        }
    }
}

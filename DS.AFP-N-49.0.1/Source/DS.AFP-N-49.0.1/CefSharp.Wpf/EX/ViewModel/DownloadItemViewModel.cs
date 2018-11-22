
using System;
using System.ComponentModel;
using System.IO;
using System.Linq.Expressions;

namespace CefSharp.Wpf.ViewModel
{
    public class DownloadItemViewModel : ViewModelBase
    {

        private string _FileName;

        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName
        {
            get { return _FileName; }
            set
            {
                _FileName = value;
                RaisePropertyChanged(() => FileName);
            }
        }


        private string _Percentage="0%";

        /// <summary>
        /// 百分比
        /// </summary>
        public string Percentage
        {
            get { return _Percentage; }
            set
            {
                _Percentage = value;
                RaisePropertyChanged(() => Percentage);
            }
        }



        private string _FullPath;

        /// <summary>
        /// 完整保存路径
        /// </summary>
        public string FullPath
        {
            get { return _FullPath; }
            set
            {
                _FullPath = value;
                RaisePropertyChanged(() => FullPath);

                FileName = Path.GetFileName(_FullPath);
            }
        }

        private long _ReceivedBytes = 0;
        /// <summary>
        /// 已经下载字节
        /// </summary>
        public long ReceivedBytes
        {
            get { return _ReceivedBytes; }
            set
            {
                _ReceivedBytes = value;
                RaisePropertyChanged(() => ReceivedBytes);
            }
        }


        private long _TotalBytes = 0;
        /// <summary>
        /// 总字节数量
        /// </summary>
        public long TotalBytes
        {
            get { return _TotalBytes; }
            set
            {
                _TotalBytes = value;
                RaisePropertyChanged(() => TotalBytes);
                if (_TotalBytes > 0 && ReceivedBytes > 0)
                {
                    decimal tempvalue = Convert.ToDecimal(ReceivedBytes) / Convert.ToDecimal(_TotalBytes) * 100;
                    NewValue = Convert.ToInt32(tempvalue);
                    Percentage = string.Format("{0}%", decimal.Round(tempvalue, 2));
                }
            }
        }

        int _NewValue = 0;
        public int NewValue
        {
            get { return _NewValue; }
            set
            {
                _NewValue = value;
                RaisePropertyChanged(() => NewValue);
            }
        }

        private int _ID = 0;
        /// <summary>
        /// ID标识
        /// </summary>
        public int ID
        {
            get { return _ID; }
            set
            {
                _ID = value;
                RaisePropertyChanged(() => ID);
            }
        }

       private string _Url = "";
        /// <summary>
        /// 文件下载地址
        /// </summary>
        public string Url 
        {
            get { return _Url; }
            set
            {
                _Url = value;
                RaisePropertyChanged(() => Url);
            }
        }


    }


    public class ViewModelBase : INotifyPropertyChanged
    {
        protected void RaisePropertyChanged<T>(Expression<Func<T>> action)
        {
            var propertyName = GetPropertyName(action);
            RaisePropertyChanged(propertyName);
        }

        private static string GetPropertyName<T>(Expression<Func<T>> action)
        {
            var expression = (MemberExpression)action.Body;
            var propertyName = expression.Member.Name;
            return propertyName;
        }

        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
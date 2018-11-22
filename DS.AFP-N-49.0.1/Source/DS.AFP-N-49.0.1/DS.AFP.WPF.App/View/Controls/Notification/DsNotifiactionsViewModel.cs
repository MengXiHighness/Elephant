using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.AFP.Framework.ViewModel;
using DS.AFP.Framework.WPF;

namespace DS.AFP.WPF.App
{
    public class DsNotifiactionsViewModel:ViewModelBase
    {
        private string fromMoveStoryboard = "0,100,0,0";
        public string FromMoveStoryboard
        {
            get
            {
                return this.fromMoveStoryboard;
            }
            set
            {
                if (this.fromMoveStoryboard != value)
                {
                    this.fromMoveStoryboard = value;
                    base.RaisePropertyChanged("FromMoveStoryboard");
                }
            }
        }

        public Notifications Notifications
        {
            get;
            set;
        }

    }
}

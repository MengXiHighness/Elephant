using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using DS.AFP.Framework.ViewModel;

namespace DS.AFP.Framework.WPF.Portal
{
    /// <summary>
    /// 导航菜单（NavigationItem类型的ObservableCollection集合）
    /// </summary>
    public class Navigation : ObservableCollection<NavigationItem>
    {
        public event EventHandler<NavigationItemArg> NavigationEvent;
        public new void Add(NavigationItem item)
        {
            int setoff = 0;
            int count = base.Count;
            bool isset = false;
            for (int i = 0; i < count; i++)
            {
                if (base[i].Index >= item.Index)
                {
                    isset = true;
                    setoff = i;
                    break;
                }
            }

            item.NavigationItemClick += item_NavigationItemClick;

            if (isset)
            {
                base.InsertItem(setoff, item);
            }
            else
            {
                base.InsertItem(count, item);
            }
        }

        private void item_NavigationItemClick(object sender, NavigationItemArg e)
        {
            if (null != NavigationEvent)
                this.NavigationEvent(sender, e);
        }
    }
}

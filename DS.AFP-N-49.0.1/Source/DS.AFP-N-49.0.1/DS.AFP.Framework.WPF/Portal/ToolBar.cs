using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using DS.AFP.Framework.ViewModel;

namespace DS.AFP.Framework.WPF.Portal
{
    /// <summary>
    /// 工具栏（ToolBarItem型的ObservableCollection集合）
    /// </summary>
    public class ToolBar : ObservableCollection<ToolBarItem>
    {
        public event EventHandler<ToolBarItemArg> ToolBarEvent;
        public new void Add(ToolBarItem item)
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
            item.ToolBarItemClick += item_ToolBarItemClick;
            if (isset)
            {
                base.InsertItem(setoff, item);
            }
            else
            {
                base.InsertItem(count, item);
            }
        }

        private void item_ToolBarItemClick(object sender, ToolBarItemArg e)
        {
            if (null != ToolBarEvent)
                ToolBarEvent(sender, e);
        }
    }
}

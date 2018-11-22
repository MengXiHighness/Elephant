using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using DS.AFP.Framework.ViewModel;

namespace DS.AFP.Framework.WPF.Portal
{
    /// <summary>
    /// 数据状态数据（StatusDataItem型的ObservableCollection集合）
    /// </summary>
    public class StatusBar : ObservableCollection<StatusDataItem>
    {
        public new void Add(StatusDataItem item)
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
            if (isset)
            {
                base.InsertItem(setoff, item);
            }
            else
            {
                base.InsertItem(count, item);
            }
        }
    }
}

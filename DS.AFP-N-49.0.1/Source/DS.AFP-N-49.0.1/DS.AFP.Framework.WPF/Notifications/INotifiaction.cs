using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.AFP.Framework.WPF
{
    /// <summary>
    /// 通知接口
    /// </summary>
    public interface INotifiaction
    {
        void AddNotification(Notification notification);

        void RemoveNotification(Notification notification);
    }
}

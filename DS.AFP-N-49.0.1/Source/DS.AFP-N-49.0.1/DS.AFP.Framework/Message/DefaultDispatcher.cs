
using System;
using System.Windows;
using System.Windows.Threading;

namespace DS.AFP.Framework.Message
{
    /// <summary>
    /// WPF����Dispatcher
    /// </summary>
    public class DefaultDispatcher : IDispatcherFacade
    {
        /// <summary>
        /// ��UI�߳�ִ��ָ��ί��
        /// </summary>
        /// <param name="method">ָ��ί��</param>
        /// <param name="arg1">MessageSender</param>
        /// <param name="arg2">TMessage</param>
        public void BeginInvoke(Delegate method, MessageSender arg1,object arg2)
        {
            if (Application.Current != null)
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, method,arg1, arg2);
            }
        }
    }
}
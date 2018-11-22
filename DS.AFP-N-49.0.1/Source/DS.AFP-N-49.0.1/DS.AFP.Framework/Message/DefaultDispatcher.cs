
using System;
using System.Windows;
using System.Windows.Threading;

namespace DS.AFP.Framework.Message
{
    /// <summary>
    /// WPF窗体Dispatcher
    /// </summary>
    public class DefaultDispatcher : IDispatcherFacade
    {
        /// <summary>
        /// 用UI线程执行指定委托
        /// </summary>
        /// <param name="method">指定委托</param>
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
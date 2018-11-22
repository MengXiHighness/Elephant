using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using System.Threading;

namespace DS.AFP.Common.Core.Utility
{
    /// <summary>
    /// 使用线程来优化UI性能的帮助类（提供将后台线程要执行的动作转移至UI线程执行等方法）
    /// </summary>
    public class UIThreadHelper
    {
        public static void DoAction(Action backgroundAction, Action mainThreadAction)
        {
            DoAction(backgroundAction, mainThreadAction, 200);
        }
       
        /// <summary>
        /// 让action动作在UI线程执行
        /// </summary>
        public static void DoWithDispatcher(Dispatcher dispatcher, Action action)
        {
            if (dispatcher.CheckAccess())
            {
                action();
            }
            else
            {
                AutoResetEvent done = new AutoResetEvent(false);
                dispatcher.BeginInvoke((Action)delegate()
                {
                    action();
                    done.Set();
                });
                done.WaitOne();
            }
        }

        /// <summary>
        /// 在backgroundAction（后台）动作执行后回调UI线程执行mainThreadAction
        /// </summary>
        /// <param name="backgroundAction">后台动作</param>
        /// <param name="mainThreadAction">UI界面线程操作</param>
        /// <param name="milliseconds">延时时间</param>
        public static void DoAction(Action backgroundAction, Action mainThreadAction, int milliseconds)
        {
#if SL
			Dispatcher dispatcher = Dispatcher;
#else
            Dispatcher dispatcher = Dispatcher.CurrentDispatcher;
#endif
            Thread thread = new Thread(delegate()
            {
                Thread.Sleep(milliseconds);
                if (backgroundAction != null)
                    backgroundAction();
                if (mainThreadAction != null)
                    dispatcher.BeginInvoke(mainThreadAction);
            });
            thread.IsBackground = true;
#if !SL
            thread.Priority = ThreadPriority.Lowest;
#endif
            thread.Start();
        }

        /// <summary>
        /// UI性能方面提供的方法
        /// </summary>
        /// <typeparam name="T">前后线程传递的数据结构</typeparam>
        /// <param name="backgroundAction">后台线程执行的操作</param>
        /// <param name="mainThreadAction">前台线程执行的操作</param>
        /// <param name="issync"></param>
        public static void DoAction<T>(Func<T> backgroundAction, Action<T> mainThreadAction, bool issync) where T:new()
        {
            Dispatcher dispatcher = Dispatcher.CurrentDispatcher;
            AutoResetEvent done = new AutoResetEvent(false);
            Thread thread = new Thread(delegate()
            {
                var param = new T();
                if (backgroundAction != null)
                {
                    param = backgroundAction();
                    done.Set();
                }
                if (mainThreadAction != null)
                {
                    done.WaitOne();
                    dispatcher.BeginInvoke(mainThreadAction, param);
                }
            });
            thread.IsBackground = true;
#if !SL
            thread.Priority = ThreadPriority.Normal;
#endif
            thread.Start();
        }
    }
}

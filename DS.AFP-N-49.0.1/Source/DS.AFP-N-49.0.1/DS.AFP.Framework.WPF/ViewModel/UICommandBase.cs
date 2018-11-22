using DS.AFP.Framework.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace DS.AFP.Framework.WPF.ViewModel
{
    public class UICommandBase : ICommand, IActiveAware
    {
        private bool _isActive;
        private List<WeakReference> _canExecuteChangedHandlers;
        private readonly Func<object, bool> canExecuteMethod;
        public UICommandBase(ExecutedRoutedEventHandler executeHandler, Func<object, bool> canExecuteMethod)
        {
            if (executeHandler == null || canExecuteMethod == null)
                throw new ArgumentNullException("UICommandBase的参数为null");

            ExecutedRoutedEvent += executeHandler;
            this.canExecuteMethod = canExecuteMethod;
        }

       

        static readonly object m_eventLock = new object();
        static ExecutedRoutedEventHandler executedRoutedEvent;
        public event ExecutedRoutedEventHandler ExecutedRoutedEvent
        {
            //显式实现'add'方法
            add
            {
                //加私有锁，并向委托链表增加一个处理程序(以'value'为参数)
                lock (m_eventLock) { executedRoutedEvent += value; }
            }
            //显式实现'remove'方法
            remove
            {
                //加私有锁，并从委托链表从中移除处理程序(以'value'为参数)
                lock (m_eventLock) { executedRoutedEvent -= value; }
            }
        }

        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                if (_isActive != value)
                {
                    _isActive = value;
                    OnIsActiveChanged();
                }
            }
        }

        protected virtual void OnCanExecuteChanged()
        {
            WeakEventHandlerManager.CallWeakReferenceHandlers(this, _canExecuteChangedHandlers);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate")]
        public void RaiseCanExecuteChanged()
        {
            OnCanExecuteChanged();
        }

        public virtual event EventHandler IsActiveChanged;

        protected virtual void OnIsActiveChanged()
        {
            EventHandler isActiveChangedHandler = IsActiveChanged;
            if (isActiveChangedHandler != null) isActiveChangedHandler(this, EventArgs.Empty);
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                WeakEventHandlerManager.AddWeakReferenceHandler(ref _canExecuteChangedHandlers, value, 2);
            }
            remove
            {
                WeakEventHandlerManager.RemoveWeakReferenceHandler(_canExecuteChangedHandlers, value);
            }
        }

      public bool CanExecute(object parameter, IInputElement target)
      {
          return true;
      }
        //
        // 摘要: 
        //     对当前命令目标执行 System.Windows.Input.RoutedCommand。
        //
        // 参数: 
        //   parameter:
        //     要传递到处理程序的用户定义的参数。
        //
        //   target:
        //     要在其中查找命令处理程序的元素。
        //
        // 异常: 
        //   System.InvalidOperationException:
        //     target 不是 System.Windows.UIElement 或 System.Windows.ContentElement。
        [SecurityCritical]
        public void Execute(object parameter, IInputElement target)
        { 

        }
    }
}

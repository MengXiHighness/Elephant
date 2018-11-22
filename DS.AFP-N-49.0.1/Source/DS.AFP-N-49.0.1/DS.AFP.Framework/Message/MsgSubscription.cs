using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.AFP.Framework;
using System.Globalization;

namespace DS.AFP.Framework.Message
{
    /// <summary>
    /// 消息订阅类
    /// </summary>
    /// <typeparam name="TMessage"></typeparam>
    public class MsgSubscription<TMessage> : IMsgSubscription
    {
        private readonly IDelegateReference _actionReference;
        private readonly IDelegateReference _filterReference;

        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <param name="actionReference">订阅的委托</param>
        /// <param name="filterReference">过滤器委托</param>
        public MsgSubscription(IDelegateReference actionReference, IDelegateReference filterReference)
        {
            if (actionReference == null)
                throw new ArgumentNullException("actionReference");
            if (!(actionReference.Target is Action<MessageSender,TMessage>))
                throw new ArgumentException(String.Format(CultureInfo.CurrentCulture, "", typeof(Action<TMessage>).FullName), "actionReference");

            if (filterReference == null)
                throw new ArgumentNullException("filterReference");
            if (!(filterReference.Target is Predicate<TMessage>))
                throw new ArgumentException(String.Format(CultureInfo.CurrentCulture,"", typeof(Predicate<TMessage>).FullName), "filterReference");

            _actionReference = actionReference;
            _filterReference = filterReference;
        }


        public Action<MessageSender,TMessage> Action
        {
            get { return (Action<MessageSender,TMessage>)_actionReference.Target; }
        }

        
        public Predicate<TMessage> Filter
        {
            get { return (Predicate<TMessage>)_filterReference.Target; }
        }

       
        public SubscriptionToken SubscriptionToken { get; set; }

        /// <summary>
        /// 发布消息
        /// </summary>
        public virtual Action<object[]> GetExecutionStrategy()
        {
            Action<MessageSender,TMessage> action = this.Action;
            Predicate<TMessage> filter = this.Filter;
            
            if (action != null && filter != null)
            {
                return arguments =>
                {
                    MessageSender argument1 = null;
                    TMessage argument2 = default(TMessage);
                    if (arguments != null && arguments.Length > 0 && arguments[0] != null && arguments[1] != null)
                    {
                        argument1 = (MessageSender)arguments[0];
                        argument2 = (TMessage)arguments[1];
                    }
                    if (filter(argument2))
                    {
                        InvokeAction(action,argument1, argument2);
                    }
                };
            }
            return null;
        }

        /// <summary>
        /// 执行action
        /// </summary>
        /// <param name="action"></param>
        /// <param name="argument1"></param>
        /// <param name="argument2"></param>
        public virtual void InvokeAction(Action<MessageSender,TMessage> action,MessageSender argument1, TMessage argument2)
        {
            if (action == null) throw new System.ArgumentNullException("action");

            action(argument1,argument2);
        }
    }
}

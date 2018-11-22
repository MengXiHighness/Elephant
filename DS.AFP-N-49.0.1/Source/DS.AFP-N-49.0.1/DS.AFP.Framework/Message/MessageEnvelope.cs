using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.AFP.Framework.Message
{
    /// <summary>
    /// 消息信封类，是用来消息订阅和发布的基类
    /// </summary>
    /// <typeparam name="TMessage"></typeparam>
    public class MessageEnvelope<TMessage> : MsgBase
    {
        private IDispatcherFacade uiDispatcher;

        private IDispatcherFacade UIDispatcher
        {
            get
            {
                if (uiDispatcher == null)
                {
                    this.uiDispatcher = new DefaultDispatcher();
                }

                return uiDispatcher;
            }
        }

        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="action">订阅的动作</param>
        /// <returns>订阅标识</returns>
        public SubscriptionToken Subscribe(Action<MessageSender,TMessage> action)
        {
            return Subscribe(action, ThreadOption.PublisherThread);
        }

        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="action">订阅的动作</param>
        /// <param name="threadOption">定义在什么类型的线程上执行动作</param>
        /// <returns>订阅标识</returns>
        public SubscriptionToken Subscribe(Action<MessageSender,TMessage> action, ThreadOption threadOption)
        {
            return Subscribe(action, threadOption, false);
        }

        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="action">订阅的动作</param>
        /// <param name="keepSubscriberReferenceAlive">为ture则执行action，否则执行action的副本</param>
        /// <returns>订阅标识</returns>
        public SubscriptionToken Subscribe(Action<MessageSender,TMessage> action, bool keepSubscriberReferenceAlive)
        {
            return Subscribe(action, ThreadOption.PublisherThread, keepSubscriberReferenceAlive);
        }

        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="action">订阅的动作</param>
        /// <param name="threadOption">定义在什么类型的线程上执行动作</param>
        /// <param name="keepSubscriberReferenceAlive">为ture则执行action，否则执行action的副本</param>
        /// <returns>订阅标识</returns>
        public SubscriptionToken Subscribe(Action<MessageSender,TMessage> action, ThreadOption threadOption, bool keepSubscriberReferenceAlive)
        {
            return Subscribe(action, threadOption, keepSubscriberReferenceAlive, null);
        }

        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="action">订阅的动作</param>
        /// <param name="threadOption">定义在什么类型的线程上执行动作</param>
        /// <param name="keepSubscriberReferenceAlive">为ture则执行action，否则执行action的副本</param>
        /// <param name="filter">过滤器</param>
        /// <returns>订阅标识</returns>
        public virtual SubscriptionToken Subscribe(Action<MessageSender,TMessage> action, ThreadOption threadOption, bool keepSubscriberReferenceAlive, Predicate<TMessage> filter)
        {
            IDelegateReference actionReference = new DelegateReference(action, keepSubscriberReferenceAlive);
            IDelegateReference filterReference;
            if (filter != null)
            {
                filterReference = new DelegateReference(filter, keepSubscriberReferenceAlive);
            }
            else
            {
                filterReference = new DelegateReference(new Predicate<TMessage>(delegate { return true; }), true);
            }
            MsgSubscription<TMessage> subscription;
            switch (threadOption)
            {
                case ThreadOption.PublisherThread:
                    subscription = new MsgSubscription<TMessage>(actionReference, filterReference);
                    break;
                case ThreadOption.BackgroundThread:
                    subscription = new BackgroundMsgSubscription<TMessage>(actionReference, filterReference);
                    break;
                case ThreadOption.UIThread:
                    subscription = new DispatcherMsgSubscription<TMessage>(actionReference, filterReference, UIDispatcher);
                    break;
                default:
                    subscription = new MsgSubscription<TMessage>(actionReference, filterReference);
                    break;
            }


            return base.InternalSubscribe(subscription);
        }



        /// <summary>
        /// 发布MessageEnvelope<TMessage>
        /// </summary>
        /// <param name="payload">传递给订阅者的消息对象</param>
        public virtual void Publish(MessageSender sender,TMessage message)
        {
            base.InternalPublish(sender,message);
        }

        /// <summary>
        /// 取消匹配的第一个订阅
        /// </summary>
        /// <param name="subscriber"></param>
        public virtual void Unsubscribe(Action<MessageSender,TMessage> subscriber)
        {
            lock (Subscriptions)
            {
                IMsgSubscription eventSubscription = Subscriptions.Cast<MsgSubscription<TMessage>>().FirstOrDefault(evt => evt.Action == subscriber);
                if (eventSubscription != null)
                {
                    Subscriptions.Remove(eventSubscription);
                }
            }
        }

        public virtual bool Contains(Action<MessageSender,TMessage> subscriber)
        {
            IMsgSubscription msgSubscription;
            lock (Subscriptions)
            {
                msgSubscription = Subscriptions.Cast<MsgSubscription<TMessage>>().FirstOrDefault(evt => evt.Action == subscriber);
            }
            return msgSubscription != null;
        }

    }
}

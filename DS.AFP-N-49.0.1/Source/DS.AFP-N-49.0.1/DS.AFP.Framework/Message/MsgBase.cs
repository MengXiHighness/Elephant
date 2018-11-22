using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.AFP.Framework.Message
{
    /// <summary>
    /// 消息基类
    /// </summary>
    public abstract class MsgBase
    {
        private readonly List<IMsgSubscription> _subscriptions = new List<IMsgSubscription>();

       
        protected ICollection<IMsgSubscription> Subscriptions
        {
            get { return _subscriptions; }
        }

        
        protected virtual SubscriptionToken InternalSubscribe(IMsgSubscription msgSubscription)
        {
            if (msgSubscription == null) throw new System.ArgumentNullException("msgSubscription");

            msgSubscription.SubscriptionToken = new SubscriptionToken(Unsubscribe);

            lock (Subscriptions)
            {
                Subscriptions.Add(msgSubscription);
            }
            return msgSubscription.SubscriptionToken;
        }

       
        protected virtual void InternalPublish(params object[] arguments)
        {
            List<Action<object[]>> executionStrategies = PruneAndReturnStrategies();
            foreach (var executionStrategy in executionStrategies)
            {
                executionStrategy(arguments);
            }
        }

        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="token">订阅标识</param>
        public virtual void Unsubscribe(SubscriptionToken token)
        {
            lock (Subscriptions)
            {
                IMsgSubscription subscription = Subscriptions.FirstOrDefault(evt => evt.SubscriptionToken == token);
                if (subscription != null)
                {
                    Subscriptions.Remove(subscription);
                }
            }
        }

        /// <summary>
        /// 是否包含了指定订阅标识
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public virtual bool Contains(SubscriptionToken token)
        {
            lock (Subscriptions)
            {
                IMsgSubscription subscription = Subscriptions.FirstOrDefault(evt => evt.SubscriptionToken == token);
                return subscription != null;
            }
        }

        private List<Action<object[]>> PruneAndReturnStrategies()
        {
            List<Action<object[]>> returnList = new List<Action<object[]>>();

            lock (Subscriptions)
            {
                for (var i = Subscriptions.Count - 1; i >= 0; i--)
                {
                    Action<object[]> listItem =
                        _subscriptions[i].GetExecutionStrategy();

                    if (listItem == null)
                    {
                        _subscriptions.RemoveAt(i);
                    }
                    else
                    {
                        returnList.Add(listItem);
                    }
                }
            }

            return returnList;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.AFP.Framework.Message
{
    /// <summary>
    /// 订阅标识，组合一个SubscriptionToken型Action和一个Guid
    /// <code>
    /// //IMsgSubscription msgSubscription
    /// msgSubscription.SubscriptionToken = new SubscriptionToken(Unsubscribe);
    /// </code>
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Justification = "Should never have a need for a finalizer, hence no need for Dispole(bool)")]
    public class SubscriptionToken : IEquatable<SubscriptionToken>, IDisposable
    {
        private readonly Guid _token;
        private Action<SubscriptionToken> _unsubscribeAction;

        /// <summary>
        /// 构造一个SubscriptionToken实例
        /// </summary>
        public SubscriptionToken(Action<SubscriptionToken> unsubscribeAction)
        {
            _unsubscribeAction = unsubscribeAction;
            _token = Guid.NewGuid();
        }

        /// <summary>
        /// 比较1个SubscriptionToken与本对象内部SubscriptionToken当前是否一致
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(SubscriptionToken other)
        {
            if (other == null) return false;
            return Equals(_token, other._token);
        }

        /// <summary>
        /// 比较1个object与本对象内部SubscriptionToken当前是否一致
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as SubscriptionToken);
        }

        /// <summary>
        /// 返回本对象内部Guid对象的GetHashCode方法
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return _token.GetHashCode();
        }

       
        /// <summary>
        /// 释放资源并垃圾回收
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Justification = "Should never have need for a finalizer, hence no need for Dispose(bool).")]
        public virtual void Dispose()
        {
           
            if (this._unsubscribeAction != null)
            {
                this._unsubscribeAction(this);
                this._unsubscribeAction = null;
            }

            GC.SuppressFinalize(this);
        }
    }
}

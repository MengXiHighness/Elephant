using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.AFP.Framework.Message
{
    /// <summary>
    /// 消息订阅接口
    /// </summary>
    public interface IMsgSubscription
    {
        /// <summary>
        /// 得到订阅标识
        /// </summary>
        /// <value></value>
        SubscriptionToken SubscriptionToken { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        Action<object[]> GetExecutionStrategy();
    }
}
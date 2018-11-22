
using System;

namespace DS.AFP.Framework.Message
{
    /// <summary>
    /// 分发器接口
    /// </summary>
    public interface IDispatcherFacade
    {
        void BeginInvoke(Delegate method, MessageSender arg1,object arg2);
    }
}
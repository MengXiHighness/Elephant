
using System;

namespace DS.AFP.Framework.Message
{
    /// <summary>
    /// �ַ����ӿ�
    /// </summary>
    public interface IDispatcherFacade
    {
        void BeginInvoke(Delegate method, MessageSender arg1,object arg2);
    }
}
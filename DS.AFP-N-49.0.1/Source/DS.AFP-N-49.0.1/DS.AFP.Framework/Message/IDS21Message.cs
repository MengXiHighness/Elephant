

using DS.AFP.Communication;
using System;
using DS.AFP.Communication.DS21;
using DS.AFP.Framework.Message;
namespace DS.AFP.Framework
{
    /// <summary>
    /// 21消息接口
    /// </summary>
    public interface IDS21Message
    {

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        Msgenvelope<TMsgType> GetMessage<TMsgType>() where TMsgType : class, new();

        object GetMessage(Type msgType);

        int Send(DSMsg dsmsg);

        int Send(DSMsg dsmsg, bool isTransferRoute);

        bool Register(IDS21 ds21);

    }
}

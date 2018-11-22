using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.AFP.Communication.DS21
{
    /// <summary>
    /// DS21接口
    /// </summary>
    public interface IDS21
    {
        int Send(DSMsg dsMsg);
        int Send(DSMsg dsMsg, bool isTransferRoute = false);
        event EventHandler<DSMsg> MessageReceived;
        bool Register(ushort nodeType, ushort nodeID);
    }
}

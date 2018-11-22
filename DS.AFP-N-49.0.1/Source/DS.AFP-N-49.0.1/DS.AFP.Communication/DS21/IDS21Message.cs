using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.AFP.Communication.DS21
{
    public interface IDS21Message
    {
        object Receive(Type msgType);
        int Send(DSMsg dsmsg);
        bool Register(IDS21 ds21);
    }
}

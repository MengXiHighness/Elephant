using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.AFP.Communication.DS21
{
    public class DS21Message:IDS21Message
    {
        public object Receive(Type msgType)
        {
            throw new NotImplementedException();
        }

        public int Send(DSMsg dsmsg)
        {
            throw new NotImplementedException();
        }

        public bool Register(IDS21 ds21)
        {
            throw new NotImplementedException();
        }
    }
}

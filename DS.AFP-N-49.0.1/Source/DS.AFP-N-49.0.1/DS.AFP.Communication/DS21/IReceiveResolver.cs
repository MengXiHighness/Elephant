using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.AFP.Communication.DS21
{
    public interface IReceiveResolver
    {
        void Handler(DSMsg dsmsg);
    }
}

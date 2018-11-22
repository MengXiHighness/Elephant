using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.AFP.Common.Core
{
    public class SessionHandlerCollection:System.Collections.Concurrent.ConcurrentDictionary<string,IHandler>
    {

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.AFP.Communication.Common;

namespace DS.AFP.Communication.SocketEngine
{
    interface IExceptionSource
    {
        event EventHandler<ErrorEventArgs> ExceptionThrown;
    }
}

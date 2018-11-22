using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.AFP.Common.Core;

namespace DS.AFP.Communication.DS21
{
    public class DS21InitializeException : AddinException 
    {
        public DS21InitializeException(string message,Exception innerException) : base("DS21", message, innerException) { }
    }
}

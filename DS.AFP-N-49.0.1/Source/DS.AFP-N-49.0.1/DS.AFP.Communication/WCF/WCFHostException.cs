using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.AFP.Common.Core;

namespace DS.AFP.Communication.WCF
{
    /// <summary>
    /// WCF宿主异常
    /// </summary>
    public class WCFHostException : AddinException
    {
        public WCFHostException() : base(DS.AFP.Common.Core.Resources.WCFHostException) { }
    }
}

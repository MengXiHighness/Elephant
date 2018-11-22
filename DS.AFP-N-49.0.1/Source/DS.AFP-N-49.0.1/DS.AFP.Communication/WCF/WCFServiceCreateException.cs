using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.AFP.Common.Core;

namespace DS.AFP.Communication.WCF
{
    /// <summary>
    /// WCF服务创建异常
    /// </summary>
    public class WCFServiceCreateException:AddinException
    {
        public WCFServiceCreateException(string message, Exception innerException) : base(message, innerException) { }
    }
}

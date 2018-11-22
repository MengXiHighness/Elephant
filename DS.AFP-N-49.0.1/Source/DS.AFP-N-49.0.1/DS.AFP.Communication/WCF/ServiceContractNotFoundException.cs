using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.AFP.Common.Core;

namespace DS.AFP.Communication.WCF
{
    /// <summary>
    /// 未找到服务契约异常
    /// </summary>
    public class ServiceContractNotFoundException:AddinException
    {
        public ServiceContractNotFoundException(string message):base(message)
        {
        }
    }
}

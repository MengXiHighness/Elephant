using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.AFP.Common.Core;

namespace DS.AFP.Communication.WCF
{
    /// <summary>
    /// 未找到绑定异常
    /// </summary>
    public class BindingNotFoundException:AddinException
    {
        public BindingNotFoundException(string message)
            : base(message)
        {
        }
    }
}

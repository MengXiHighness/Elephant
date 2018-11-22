using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.AFP.Common.Core;

namespace DS.AFP.Framework
{
    /// <summary>
    /// 容器初始化异常
    /// </summary>
    public class ContainerInitializeException :AFPException
    {
        public ContainerInitializeException(string message, Exception innerException) : base(message, innerException) { }
    }
}

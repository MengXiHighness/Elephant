using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.AFP.Framework.Message
{
    /// <summary>
    /// 委托引用接口
    /// </summary>
    public interface IDelegateReference
    {
        /// <summary>
        /// 得到一个Delegate 类型实例
        /// </summary>
        /// <value>如果存在则返回Delegate否则null</value>
        Delegate Target { get; }
    }
}

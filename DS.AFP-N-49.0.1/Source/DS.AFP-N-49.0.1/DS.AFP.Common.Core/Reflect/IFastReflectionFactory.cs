using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.AFP.Common.Core.Reflect
{
    /// <summary>
    /// 快速反射工厂接口（ConstructorInvokerFactory、FieldAccessorFactory、MethodInvokerFactory、PropertyAccessorFactory基类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public interface IFastReflectionFactory<TKey, TValue>
    {
        TValue Create(TKey key);
    }
}

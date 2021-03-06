﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.AFP.Common.Core.Reflect
{
    /// <summary>
    /// 快速反射缓存接口（FastReflectionCache基类）
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public interface IFastReflectionCache<TKey, TValue>
    {
        TValue Get(TKey key);
    }
}

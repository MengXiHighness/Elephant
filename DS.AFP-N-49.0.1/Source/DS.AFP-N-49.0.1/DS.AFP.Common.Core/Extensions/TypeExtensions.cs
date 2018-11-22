using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.AFP.Common.Core.Extensions
{
    /// <summary>
    /// 类型扩展（用于获取指定对象的类型）
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// 获取指定对象的类型
        /// </summary>
        public static Type GetType(this object obj)
        {
            return typeof(object);
        }
    }
}

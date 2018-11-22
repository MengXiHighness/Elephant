using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.AFP.Common.Core.Utility
{
    /// <summary>
    /// 数据库类型转换工具（提供object到decimal?与DateTime?的转换）
    /// </summary>
    public static class DBConvertHelper
    {
        /// <summary>
        /// object转成Decimal?
        /// </summary>
        public static decimal? ToNullableDecimal(object value)
        {
            if (DBNull.Value.Equals(value))
            {
                return null;
            }

            return Convert.ToDecimal(value);
        }

        /// <summary>
        /// object转成Datetime?
        /// </summary>
        public static DateTime? ToNullableDatetime(object value)
        {
            if (DBNull.Value.Equals(value))
            {
                return null;
            }

            return Convert.ToDateTime(value);
        }
    }
}

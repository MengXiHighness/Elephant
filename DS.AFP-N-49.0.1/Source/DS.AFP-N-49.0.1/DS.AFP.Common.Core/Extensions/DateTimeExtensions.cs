using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.AFP.Common.Core
{
    /// <summary>
    /// 时间类型转换（主要负责将DateTime转换成制定格式的string）
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// 把dt转成string格式(yyyy-MM-dd,如：2014-01-01)
        /// </summary>
        /// <param name="dt">要转换的DateTime</param>
        /// <param name="separator">日期分隔符，默认为-</param>
        /// <returns>转换后的字符串</returns>
        public static string DateFormat(this DateTime dt, char separator='-')
        {
            string dateString = string.Format("{1}{0}{2}{0}{3}",
                separator,
                dt.Year,
                dt.Month.ToString().PadLeft(2, '0'),
                dt.Day.ToString().PadLeft(2, '0'));

            return dateString;
        }

        /// <summary>
        /// 把dt转成string格式(hh-mm-ss,如：08:25:06)
        /// </summary>
        /// <param name="dt">要转换的DateTime</param>
        /// <param name="separator">时间分隔符，默认为:</param>
        /// <returns>转换后的字符串</returns>
        public static string TimeFormat(this DateTime dt, char separator = ':')
        {
            string dateString = string.Format("{1}{0}{2}{0}{3}",
                separator,
                dt.Hour.ToString().PadLeft(2, '0'),
                dt.Minute.ToString().PadLeft(2, '0'),
                dt.Second.ToString().PadLeft(2, '0'));

            return dateString;
        }

        /// <summary>
        /// 把dt转成string格式(yyyy-MM-dd hh-mm-ss,如：2014-01-01 08:25:06)
        /// </summary>
        /// <param name="dt">要转换的DateTime</param>
        /// <param name="separator">日期时间分隔符 日期默认为- 时间默认为:</param>
        /// <returns>转换后的字符串</returns>
        public static string DateTimeFormat(this DateTime dt, char separator1 = '-', char separator2 = ':')
        {
            string dateString = string.Format("{2}{0}{3}{0}{4} {5}{1}{6}{1}{7}",
                separator1,separator2,
                dt.Year,
                dt.Month.ToString().PadLeft(2, '0'),
                dt.Day.ToString().PadLeft(2, '0'),
                dt.Hour.ToString().PadLeft(2, '0'),
                dt.Minute.ToString().PadLeft(2, '0'),
                dt.Second.ToString().PadLeft(2, '0'));

            return dateString;
        }
       
    }
}


using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DS.AFP.Common.Core
{
    /// <summary>
    /// 字符串扩展（对字符串的各种操作，如字符串替换、字符串搜索、大小写转换等）
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// 字符串是否为null或空字符串
        /// </summary>
        /// <returns>null或空字符串返回true，反之为flase</returns>
        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        /// <summary>
        /// 字符串是否为不是null与空字符串
        /// </summary>
        /// <returns>null或空字符串返回false，反之为true</returns>
        public static bool NotEmpty(this string s)
        {
            return !string.IsNullOrEmpty(s);
        }

        /// <summary>
        /// 字符串首字母小写其余不变
        /// </summary>
        public static string FirstLower(this string s)
        {
            if (s.IsNullOrEmpty()) return s;
            return s[0].ToString().ToLower() + s.Substring(1);
        }


        /// <summary>
        /// 字符串首字母大写其余不变
        /// </summary>
        public static string FirstUpper(this string s)
        {
            if (s.IsNullOrEmpty()) return s;

            return s[0].ToString().ToUpper() + s.Substring(1);
        }

        /// <summary>
        /// 在输入字符串中搜索正则表达式的匹配项，并将精确结果作为单个 Match 对象返回
        /// </summary>
        /// <param name="s">指定的字符串</param>
        /// <param name="pattern">指定的正则表达式</param>
        /// <returns>正则表达式在输入字符串中找到匹配项返回true，反之为flase</returns>
        public static bool IsMatch(this string s, string pattern)
        {
            if (s == null) return false;
            else return Regex.IsMatch(s, pattern);
        }

        /// <summary>
        /// 寻找输入字符串中搜索正则表达式的子字符串
        /// </summary>
        /// <param name="s">指定的字符串</param>
        /// <param name="pattern">指定的正则表达式</param>
        /// <returns>在输入字符串中搜索正则表达式的子字符串</returns>
        public static string Match(this string s, string pattern)
        {
            if (s == null) return "";
            return Regex.Match(s, pattern).Value;
        }

        /// <summary>
        /// 将指定的 String 中的每个格式项替换为相应对象的值的文本等效项
        /// </summary>
        /// <param name="stringFormat">复合格式字符串</param>
        /// <param name="args">包含零个或多个要格式化的对象的 Object 数组</param>
        /// <returns>将指定 String 中的格式项替换为指定数组中相应 Object 实例的值的文本等效项</returns>
        public static string FormatString(this string stringFormat, params object[] args)
        {
            return string.Format(stringFormat, args);
        }

        /// <summary>
        /// 从当前 String 对象移除数组中指定的前导匹配项
        /// </summary>
        /// <param name="sourceString">原始字符串</param>
        /// <param name="prefix">前导匹配项</param>
        /// <param name="ignoreCase">若要在对此字符串与 value 进行比较时忽略大小写，则为 true；否则为 false。默认为true</param>
        /// <returns>String 对象移除数组中指定的前导匹配项后的副本</returns>
        public static string TrimStartString(this string sourceString, string prefix, bool ignoreCase = true)
        {
            prefix = prefix ?? string.Empty;
            if (!sourceString.StartsWith(prefix, ignoreCase, CultureInfo.CurrentCulture))
            {
                return sourceString;
            }
            return sourceString.Remove(0, prefix.Length);
        }

        /// <summary>
        /// 从当前 String 对象移除数组中指定的尾部匹配项
        /// </summary>
        /// <param name="sourceString">原始字符串</param>
        /// <param name="prefix">前导匹配项</param>
        /// <param name="ignoreCase">若要在对此字符串与 value 进行比较时忽略大小写，则为 true；否则为 false。默认为true</param>
        /// <returns>String 对象移除数组中指定的尾部匹配项后的副本</returns>
        public static string TrimEndString(this string sourceString, string suffix, bool ignoreCase = true)
        {
            suffix = suffix ?? string.Empty;
            if (!sourceString.EndsWith(suffix, ignoreCase, CultureInfo.CurrentCulture))
            {
                return sourceString;
            }
            return sourceString.Substring(0, sourceString.Length - suffix.Length);
        }

        /// <summary>
        /// 将List按指定格式转换成string[]
        /// </summary>
        /// <param name="list">待转换的List</param>
        /// <param name="formatStr">转换格式格式，格式如同string.Format中的格式</param>
        /// <returns>转换后的string[]</returns>
        public static string[] ToArray(this List<string> list, string formatStr)
        {
            string[] arrayStr = new string[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                arrayStr[i] = string.Format(formatStr, list[i]);
            }

            return arrayStr;
        }

        /// <summary>
        /// 将指定string转换为utf-8的副本
        /// </summary>
        public static string ToUTF8(this string sourceString)
        {
            byte[] buff = Encoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(sourceString));
            return Encoding.UTF8.GetString(buff);
        }


        public static string HtmlEncode(this string sourceString)
        {
            return System.Net.WebUtility.HtmlEncode(sourceString);
        }

        public static string HtmlDecode(this string sourceString)
        {
            return System.Net.WebUtility.HtmlDecode(sourceString);
        }

        public static string UrlEncode(this string sourceString)
        {
           return System.Web.HttpUtility.UrlEncode(sourceString);
        }

        public static string UrlDecode(this string sourceString)
        {
            return System.Web.HttpUtility.UrlDecode(sourceString);
        }


        /// <summary>
        /// 在由指定的正则表达式模式定义的位置拆分输入字符串。指定的选项将修改匹配操作。
        /// </summary>
        /// <param name="sourceString">要拆分的字符串</param>
        /// <param name="splitStr">要匹配的正则表达式模式</param>
        /// <param name="regexOption">提供用于设置正则表达式选项的枚举值,默认为IgnoreCase</param>
        /// <returns>字符串数组</returns>
        public static string[] Split(this string sourceString, string splitStr,RegexOptions regexOption = RegexOptions.IgnoreCase)
        {
            return Regex.Split(sourceString.TrimEndString(splitStr), splitStr, regexOption);  
        }

        /// <summary>
        /// 是否是Emai地址
        /// </summary>
        /// <returns>是为true，否则false</returns>
        public static bool IsEmail(this string value)
        {
            return Regex.IsMatch(value, @"^(\w)+(\.\w+)*@(\w)+((\.\w{2,3}){1,3})$");
        }

        /// <summary>
        /// 是否是手机号码
        /// </summary>
        /// <returns>是为true，否则false</returns>
        public static bool IsMobilePhone(this string value)
        {
            return Regex.IsMatch(value, @"^((\+86)|(86))?(1)\d{10}$");
        }

        /// <summary>
        /// 是否是IP地址
        /// </summary>
        /// <returns>是为true，否则false</returns>
        public static bool IsIPAdress(this string value)
        {
            return Regex.IsMatch(value, @"^(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[0-9]{1,2})(\.(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[0-9]{1,2})){3}$");
        }

        /// <summary>
        /// 是否是ZipCode
        /// </summary>
        /// <returns>是为true，否则false</returns>
        public static bool IsZipCode(this string value)
        {
            return Regex.IsMatch(value, @"^[1-9][0-9]{5}$");
        }

        /// <summary>
        /// 是否是IDCard
        /// </summary>
        /// <returns>是为true，否则false</returns>
        public static bool IsIDCard(this string value)
        {
            return Regex.IsMatch(value, @"^(\d{6})(18|19|20)?(\d{2})([01]\d)([0123]\d)(\d{3})(\d|X)?$");
        }

        /// <summary>
        /// 是否是URL
        /// </summary>
        /// <returns>是为true，否则false</returns>
        public static bool IsURL(this string value)
        {
            return Regex.IsMatch(value, @"^(http|https|ftp)\://([a-zA-Z0-9\.\-]+(\:[a-zA-Z0-9\.&amp;%\$\-]+)*@)?((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|([a-zA-Z0-9\-]+\.)*[a-zA-Z0-9\-]+\.[a-zA-Z]{2,4})(\:[0-9]+)?(/[^/][a-zA-Z0-9\.\,\?\'\\/\+&amp;%\$#\=~_\-@]*)*$");
        }

        /// <summary>
        /// 去掉Xml的头协议及命名空间
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string InnerXML(this string value)
        {
            if (value == null) return "";
            value = value.Replace(" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"", "");
            value = value.Replace(" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "");
            value = value.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");
            return value;
        }

    

    }
}

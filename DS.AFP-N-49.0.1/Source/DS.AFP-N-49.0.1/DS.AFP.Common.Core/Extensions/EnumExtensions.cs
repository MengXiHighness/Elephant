using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Reflection;

namespace DS.AFP.Common.Core
{
    /// <summary>
    /// 枚举扩展（负责获取枚举属性描述并输出string）
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// 获取枚举属性描述
        /// </summary>
        /// <param name="en">枚举</param>
        /// <returns>属性描述，如没有则返回枚举的ToString</returns>
        public static string ToDescription(this Enum en)
        {
            Type type = en.GetType();
            MemberInfo[] memInfo = type.GetMember(en.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }
            return en.ToString();
        }
    }
}

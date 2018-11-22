using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.AFP.Common.Core
{
    /// <summary>
    /// 进制转换类（数字10进制与64进制的互转）
    /// </summary>
    public static class IntConvertExtensions
    {
        private static char[] rDigits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

        /// <summary>
        /// 数字10进制转64进制
        /// </summary>
        /// <param name="i">数字10进制</param>
        /// <returns>64进制字符串</returns>
        public static string To64(this Int32 i)
        {
            int digitIndex = 0;
            int longPositive = Math.Abs(i);
            int radix = 62;//62进制
            char[] outDigits = new char[63];
            for (digitIndex = 0; digitIndex <= 64; digitIndex++)
            {
                if (longPositive == 0) { break; }
                outDigits[outDigits.Length - digitIndex - 1] = rDigits[longPositive % radix];
                longPositive /= radix;
            }
            return new string(outDigits, outDigits.Length - digitIndex, digitIndex);
        }

        /// <summary>
        /// 64进制转数字10进制
        /// </summary>
        /// <param name="value">64进制字符串</param>
        /// <returns>数字10进制</returns>
        public static long Form64To10(this string value)
        {
            int fromBase = 62;
            value = value.Trim();
            if (string.IsNullOrEmpty(value))
            {
                return 0L;
            }
            string sDigits = new string(rDigits, 0, fromBase);
            long result = 0;
            for (int i = 0; i < value.Length; i++)
            {
                if (!sDigits.Contains(value[i].ToString()))
                {
                    throw new ArgumentException(string.Format("The argument \"{0}\" is not in {1} system", value[i], fromBase));
                }
                else
                {
                    try
                    {
                        int index = 0;
                        for (int xx = 0; xx < rDigits.Length; xx++)
                        {
                            if (rDigits[xx] == value[value.Length - i - 1])
                            {
                                index = xx;
                            }
                        }
                        result += (long)Math.Pow(fromBase, i) * index;//   2
                    }
                    catch
                    {
                        throw new OverflowException("Arithmetic overflow");
                    }
                }
            }
            return result;
        }
    }
}

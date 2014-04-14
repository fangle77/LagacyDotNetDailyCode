using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace yltl.Common
{
    /// <summary>
    /// 数据类型转换扩展方法
    /// </summary>
    public static class ConverterExtendFunc
    {
        /// <summary>
        /// 带判断null值的ToString方法，如果为null返回String.Empty
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public static string ToStringNull(this object temp)
        {
            if (temp == null) return string.Empty;
            else return temp.ToString();
        }

        /// <summary>
        /// 转换为int，如果无法转换，返回0
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public static int ToInt(this string temp)
        {
            return temp.ToInt(0);
        }

        /// <summary>
        /// 转换为int，如果无法转换，返回指定的默认值
        /// </summary>
        /// <param name="temp"></param>
        /// <param name="defaultValue">如果转换失败，则返回该值</param>
        /// <returns></returns>
        public static int ToInt(this string temp, int defaultValue)
        {
            int i = 0;
            if (!int.TryParse(temp, out i)) i = defaultValue;
            return i;
        }

        /// <summary>
        /// 转换为long，如果无法转换，返回0
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public static long ToLong(this string temp)
        {
            return temp.ToLong(0);
        }

        /// <summary>
        /// 转换为long，如果无法转换，返回指定的默认值
        /// </summary>
        /// <param name="temp"></param>
        /// <param name="defaultValue">如果转换失败，则返回该值</param>
        /// <returns></returns>
        public static long ToLong(this string temp, long defaultValue)
        {
            long i = 0;
            if (!long.TryParse(temp, out i)) i = defaultValue;
            return i;
        }

        /// <summary>
        /// 转换为double，如果无法转换，返回0
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public static double ToDouble(this string temp)
        {
            return temp.ToDouble(0);
        }

        /// <summary>
        /// 转换为double，如果无法转换，返回指定的默认值
        /// </summary>
        /// <param name="temp"></param>
        /// <param name="defaultValue">如果转换失败，则返回该值</param>
        /// <returns></returns>
        public static double ToDouble(this string temp, double defaultValue)
        {
            double i = 0;
            if (!double.TryParse(temp, out i)) i = defaultValue;
            return i;
        }

        /// <summary>
        /// 转换为decimal，如果无法转换，返回0
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this string temp)
        {
            return temp.ToDecimal(0);
        }

        /// <summary>
        /// 转换为decimal，如果无法转换，返回指定的默认值
        /// </summary>
        /// <param name="temp"></param>
        /// <param name="defaultValue">如果转换失败，则返回该值</param>
        /// <returns></returns>
        public static decimal ToDecimal(this string temp, decimal defaultValue)
        {
            decimal i = 0;
            if (!decimal.TryParse(temp, out i)) i = defaultValue;
            return i;
        }
    }
}

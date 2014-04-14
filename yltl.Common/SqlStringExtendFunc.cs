using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace yltl.Common
{
    /// <summary>
    /// 扩展方法
    /// </summary>
    public static class SqlStringExtendFunc
    {
        /// <summary>
        /// 将string 列表连接成一个串，例如：Join(",") 结果 a,b
        /// </summary>
        public static string Join(this IEnumerable<string> stringList, string link)
        {
            if (string.IsNullOrEmpty(link)) link = " ";
            if (stringList == null)
                return link + link;

            StringBuilder sb = new StringBuilder();
            //','      ',     ,'
            foreach (string block in stringList)
            {
                sb.Append(block + link);
            }
            if (sb.Length == 0) return link + link;

            var result = link;
            sb.Insert(0, result);
            result = sb.ToString();

            return result;
        }

        /// <summary>
        /// 将string 列表连接成一个串,例如Join("'","','","'")结果 'a','b','c'
        /// </summary>
        public static string Join(this IEnumerable<string> stringList, string head, string link, string tail)
        {
            if (string.IsNullOrEmpty(link)) link = " ";
            if (stringList == null)
                return link + link;

            StringBuilder sb = new StringBuilder();

            //','      ',     ,'
            foreach (string block in stringList)
            {
                sb.Append(block + link);
            }
            if (sb.Length == 0) return link + link;

            var result = link;
            sb.Insert(0, result);
            result = sb.ToString();

            result = result.TrimStart(link.ToCharArray());
            result = result.TrimEnd(link.ToCharArray());
            result = head + result + tail;

            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace yltl.DBUtility
{
    /// <summary>
    /// 加密解密类
    /// </summary>
    internal class Encrypt
    {
        //定义密匙
        /// <summary>
        /// 定义密匙
        /// </summary>
        private const string mstr = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";

        /// <summary>
        /// 字符串加密，自定义的加密
        /// </summary>
        /// <param name="str">待加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string EnCode(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }

            byte[] buff = Encoding.Default.GetBytes(str);
            int j, k, m;
            int len = mstr.Length;
            StringBuilder sb = new StringBuilder();
            Random r = new Random();

            for (int i = 0; i < buff.Length; i++)
            {
                j = (byte)r.Next(6);
                buff[i] = (byte)((int)buff[i] ^ j);
                k = (int)buff[i] % len;
                m = (int)buff[i] / len;
                m = m * 8 + j;
                sb.Append(mstr.Substring(k, 1) + mstr.Substring(m, 1));
            }

            return sb.ToString();
        }

        /// <summary>
        /// 字符串解密，自定义解密
        /// </summary>
        /// <param name="str">待解密的字符串</param>
        /// <returns>解密后的字符串</returns>
        public static string DeCode(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }

            try
            {
                int j, k, m, n = 0;
                int len = mstr.Length;
                byte[] buff = new byte[str.Length / 2];

                for (int i = 0; i < str.Length; i += 2)
                {
                    k = mstr.IndexOf(str[i]);
                    m = mstr.IndexOf(str[i + 1]);
                    j = m / 8;
                    m = m - j * 8;
                    buff[n] = (byte)(j * len + k);
                    buff[n] = (byte)((int)buff[n] ^ m);
                    n++;
                }
                return Encoding.Default.GetString(buff);
            }
            catch
            {
                return "";
            }
        }
    }
}

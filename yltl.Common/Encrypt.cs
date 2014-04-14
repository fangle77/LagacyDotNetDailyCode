using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace yltl.Common
{
    /// <summary>
    /// 加密解密类
    /// </summary>
    public class Encrypt
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
        public static string EnCodeToCustom(string str)
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
        public static string DeCodeCustom(string str)
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

        /// <summary>
        /// 转换成MD5 加密串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string EnCodeToMD5(string str)
        {
            var md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] enMd5 = md5.ComputeHash(Encoding.Unicode.GetBytes(str));
            System.Text.StringBuilder sb = new StringBuilder();
            for (int i = 0; i < enMd5.Length; i++)
            {
                sb.AppendFormat("{0:x2}", enMd5[i]);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 转换成SHA1加密串
        /// </summary>
        /// <param name="temp">待加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string EnCodeToSHA1(string temp)
        {
            var shai = System.Security.Cryptography.SHA1.Create();
            byte[] enMd5 = shai.ComputeHash(Encoding.Default.GetBytes(temp));
            System.Text.StringBuilder sb = new StringBuilder();
            for (int i = 0; i < enMd5.Length; i++)
            {
                sb.AppendFormat("{0:x2}", enMd5[i]);
            }
            return sb.ToString().ToUpper();
            //3D4F2BF07DC1BE38B20CD6E46949A1071F9D0E3D  "1111111"
        }
    }
}

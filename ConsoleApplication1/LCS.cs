using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    /*
     * http://blog.sina.com.cn/s/blog_455b20c10100929m.html
     */

    /// <summary>
    /// 最长公共子串算法(Longest Common Subsequence)
    /// </summary>
    class LCS
    {
        internal static string MatchLongestMatrix(string strA, string strB)
        {
            if (strA == strB) return strA;
            if (string.IsNullOrEmpty(strA) || string.IsNullOrEmpty(strB)) return string.Empty;

            int row = strA.Length;
            int colum = strB.Length;
            int[,] matrix = new int[row, colum];

            int longestLength = 0;
            int positionRow = 0;
            int positionColumn = 0;

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < colum; j++)
                {
                    if (strA[i] != strB[j])
                    {
                        matrix[i, j] = 0;
                    }
                    else if (i == 0 || j == 0)
                    {
                        matrix[i, j] = 1;
                    }
                    else
                    {
                        int length = matrix[i, j] = matrix[i - 1, j - 1] + 1;
                        if (length > longestLength)
                        {
                            longestLength = length;
                            positionRow = i;
                            positionColumn = j;
                        }
                    }
                }
            }

            return strA.Substring(positionRow + 1 - longestLength, longestLength);
        }

        /// <summary>
        /// Hirschberg 算法
        /// </summary>
        /// <param name="strA"></param>
        /// <param name="strB"></param>
        /// <returns></returns>
        public static string MatchLongest(string strA, string strB)
        {
            if (strA == strB) return strA;
            if (string.IsNullOrEmpty(strA) || string.IsNullOrEmpty(strB)) return string.Empty;

            int row = strA.Length;
            int colum = strB.Length;

            if (row > colum)
            {
                row = strB.Length;
                colum = strA.Length;
                string tempStr = strB;
                strB = strA;
                strA = tempStr;
            }

            int[] vecotorA = new int[row];
            int[] vecotorB = new int[row];

            int longestLength = 0;
            int positionRow = 0;
            int positionColumn = 0;

            for (int j = 0; j < colum; j++)
            {
                int[] temp = vecotorB;
                vecotorB = vecotorA;
                vecotorA = temp;

                for (int i = 0; i < row; i++)
                {
                    if (strA[i] != strB[j])
                    {
                        vecotorB[i] = 0;
                    }
                    else if (i == 0)
                    {
                        vecotorB[i] = 1;
                    }
                    else
                    {
                        int length = vecotorB[i] = vecotorA[i - 1] + 1;
                        if (length > longestLength)
                        {
                            longestLength = length;
                            positionRow = i;
                            positionColumn = j;
                        }
                    }
                }
            }
            return strA.Substring(positionRow + 1 - longestLength, longestLength);
        }

        public static double CalcSimilar(string strA, string strB)
        {
            string lcs = MatchLongest(strA, strB);
            return 0;
        }
    }

    class LCSTest
    {
        public static void RunTest()
        {
            while (true)
            {
                Console.Write("string A:");
                string strA = Console.ReadLine().Trim();
                if (strA.Equals("q", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }
                Console.Write("string B:");
                string strB = Console.ReadLine().Trim();
                if (strB.Equals("q", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }
                int times = 10000;
                string dis = null;
                string disV = null;
                var dt1 = DateTime.Now;
                for (int i = 0; i < times; i++)
                {
                    dis = LCS.MatchLongestMatrix(strA, strB);
                }
                var dt2 = DateTime.Now;
                for (int i = 0; i < times; i++)
                {
                    disV = LCS.MatchLongest(strA, strB);
                }
                var dt3 = DateTime.Now;

                Console.WriteLine("length:{2}-{3},The longest is:{0},time:{1}", dis, (dt2 - dt1).TotalMilliseconds, strA.Length, strB.Length);
                Console.WriteLine("length:{2}-{3},The longest is:{0},time:{1}", disV, (dt3 - dt2).TotalMilliseconds, strA.Length, strB.Length);

                Console.WriteLine();
            }
        }
    }
}

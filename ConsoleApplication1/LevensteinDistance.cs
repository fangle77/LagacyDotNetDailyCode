using System;

namespace ConsoleApplication1
{
    /*
     * http://www.cnblogs.com/ymind/archive/2012/03/27/fast-memory-efficient-Levenshtein-algorithm.html
     * http://blog.csdn.net/yangzhongwei1031/article/details/5898330
     */

    /// <summary>
    /// Levenshtein Distance计算字符串编辑距离和相似度
    /// </summary>
    class LevenshteinDistance
    {
        internal static int CalcDistanceMatrix(string strA, string strB)
        {
            if (strA == strB) return 0;
            if (strA == null) return strB.Length;
            if (strB == null) return strA.Length;
            if (strA == string.Empty) return strB.Length;
            if (strB == string.Empty) return strA.Length;

            int row = strA.Length + 1;
            int colum = strB.Length + 1;

            int[,] matrix = InitMatrix(row, colum);

            for (int i = 1; i < row; i++)
            {
                for (int j = 1; j < colum; j++)
                {
                    matrix[i, j] = Minimum(matrix[i, j - 1] + 1, matrix[i - 1, j] + 1, matrix[i - 1, j - 1] + (strA[i - 1] == strB[j - 1] ? 0 : 1));
                }
            }
            return matrix[row - 1, colum - 1];
        }

        public static int CalcDistance(string strA, string strB)
        {
            if (strA == strB) return 0;
            if (strA == null) return strB.Length;
            if (strB == null) return strA.Length;
            if (strA == string.Empty) return strB.Length;
            if (strB == string.Empty) return strA.Length;

            int row = strA.Length + 1;
            int colum = strB.Length + 1;

            //用较短的字符串作为行，减少空间分配，增加数组交换次数。
            if (row > colum)
            {
                int temp = colum;
                row = colum;
                colum = temp;

                string tempString = strB;
                strB = strA;
                strA = tempString;
            }

            int[] vectorA = new int[row];
            int[] vectorB = InitVector(row);

            for (int j = 1; j < colum; j++)
            {
                int[] vectorTmp = vectorB;
                vectorB = vectorA;
                vectorA = vectorTmp;

                vectorB[0] = j;
                for (int i = 1; i < row; i++)
                {
                    vectorB[i] = Minimum(vectorA[i] + 1, vectorB[i - 1] + 1, vectorA[i - 1] + (strA[i - 1] == strB[j - 1] ? 0 : 1));
                }
            }
            return vectorB[row - 1];
        }

        private static int[,] InitMatrix(int row, int colum)
        {
            int[,] matrix = new int[row, colum];
            for (int i = 0; i < row; i++) matrix[i, 0] = i;
            for (int j = 0; j < colum; j++) matrix[0, j] = j;
            return matrix;
        }

        private static int[] InitVector(int length)
        {
            int[] vecotor = new int[length];
            for (int i = 0; i < length; i++) vecotor[i] = i;
            return vecotor;
        }

        private static int Minimum(int x, int y, int z)
        {
            return Math.Min(z, Math.Min(x, y));
        }

        public static double CalcSimilar(string strA, string strB)
        {
            int distance = CalcDistance(strA, strB);
            int length = Math.Max(strA == null ? 0 : strA.Length, strB == null ? 0 : strB.Length);
            if (length == 0) return 1;
            return 1.0 - (double)distance / length;
        }


    }

    class LevenshteinDistanceTest
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

                int times = 1;
                int dis = 0;
                int disV = 0;
                var dt1 = DateTime.Now;
                for (int i = 0; i < times; i++)
                {
                    dis = LevenshteinDistance.CalcDistanceMatrix(strA, strB);
                }
                var dt2 = DateTime.Now;
                for (int i = 0; i < times; i++)
                {
                    disV = LevenshteinDistance.CalcDistance(strA, strB);
                }
                var dt3 = DateTime.Now;

                Console.WriteLine("length:{2}-{3},The distance is:{0},time:{1}", dis, (dt2 - dt1).TotalMilliseconds, strA.Length, strB.Length);
                Console.WriteLine("length:{2}-{3},The distanceV is:{0},time:{1}", disV, (dt3 - dt2).TotalMilliseconds, strA.Length, strB.Length);

                Console.WriteLine();
            }
        }
    }
}

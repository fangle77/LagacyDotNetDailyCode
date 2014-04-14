using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    /// <summary>
    /// 相似度
    /// </summary>
    class Similar
    {
        /// <summary>
        /// S(A,B)=LCS(A,B)/(LD(A,B)+LCS(A,B))
        /// </summary>
        /// <param name="strA"></param>
        /// <param name="strB"></param>
        /// <returns></returns>
        public static double SimilarityBy_LD_LCS(string strA, string strB)
        {
            int distance = LevenshteinDistance.CalcDistance(strA, strB);
            string lcs = LCS.MatchLongest(strA, strB);
            if (distance == 0) return 1;
            int lcsLength = lcs == null ? 0 : lcs.Length;
            if (lcsLength == 0) return 0;
            return (double)lcsLength / (lcsLength + distance);
        }

        public static double SimilarityBy_LD_LCSIgonorSpace(string strA, string strB)
        {
            return SimilarityBy_LD_LCS(strA.RemoveSpace(), strB.RemoveSpace());
        }

        public static List<string> FileSimilar(List<string> filePaths)
        {
            if (filePaths == null || filePaths.Count == 0) return new List<string>();
            string baseFile = filePaths.Find(File.Exists);
            if (baseFile == null) return new List<string>() { "all files don't exist" };

            List<string> listMsg = new List<string>(filePaths.Count);
            listMsg.Add(string.Format("{0},\t{1}", new FileInfo(baseFile).Name, 1));

            string baseContent = File.ReadAllText(baseFile);
            foreach (string f in filePaths)
            {
                if (f == baseFile) continue;

                if (File.Exists(f) == false)
                {
                    listMsg.Add(string.Format("{0},\t{1}", new FileInfo(baseFile).Name, "not exist"));
                    continue;
                }

                double d = Similar.SimilarityBy_LD_LCSIgonorSpace(baseContent, File.ReadAllText(f));
                listMsg.Add(string.Format("{0},\t{1}", new FileInfo(f).FullName, d));
            }
            return listMsg;
        }
    }

    class FileSimilar
    {
        private static readonly List<string> Sites = new List<string>() { "casa", "diapers", "beautybar", "soap", "casa", "green", "wag", "book", "look", "jump", "yoyo" };

        public static void WWWCompareFiles()
        {
            List<string> list = new List<string>();
            string siteFormat = @"E:\Code\WWW\DEV\QuidsiWebSite\{0}\Views\Product\ProductCell.svm";

            Sites.ForEach(s =>
            {
                list.Add(string.Format(siteFormat, s));
            });

            var results = Similar.FileSimilar(list);
            results.ForEach(Console.WriteLine);
        }
    }

    class SimilarTest
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

                double similar = 0;
                var dt1 = DateTime.Now;
                for (int i = 0; i < times; i++)
                {
                    similar = Similar.SimilarityBy_LD_LCS(strA, strB);
                }
                var dt2 = DateTime.Now;

                Console.WriteLine("similar:{2}-{3},The similar is:{0},time:{1}", similar, (dt2 - dt1).TotalMilliseconds, strA.Length, strB.Length);

                Console.WriteLine();
            }
        }
    }
}

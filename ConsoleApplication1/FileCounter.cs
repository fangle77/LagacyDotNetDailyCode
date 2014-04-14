using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class FileCounter
    {
        public static void CountFileLines(string directory)
        {
            if (Directory.Exists(directory) == false) return;

            StringBuilder sb = new StringBuilder();

            var df = new DirectoryInfo(directory);
            foreach (var f in df.GetFiles())
            {
                int lines = CountFileLine(f.FullName);
                sb.AppendFormat("{0}:{1}", f.Name, lines);
                sb.AppendLine();
            }

            File.AppendAllText(df.Name + "_Count.txt", sb.ToString());
        }
        private static int CountFileLine(string file)
        {
            if (File.Exists(file) == false) return 0;
            int line = 0;
            using (var sw = new StreamReader(file))
            {
                while (sw.ReadLine() != null)
                {
                    line++;
                }
            }
            return line;
        }

        public static void CountRepeat(string directory)
        {

            if (Directory.Exists(directory) == false) return;

            StringBuilder sb = new StringBuilder();

            var df = new DirectoryInfo(directory);
            foreach (var f in df.GetFiles())
            {
                int repeat = CountFileRepeat(f.FullName);
                sb.AppendFormat("{0}:{1}", f.Name, repeat);
                sb.AppendLine();
            }

            File.AppendAllText(df.Name + "_RepeatCount.txt", sb.ToString());
        }

        private static int CountFileRepeat(string file)
        {
            if (File.Exists(file) == false) return 0;
            HashSet<int> hs = new HashSet<int>();
            int repeat = 0;
            using (var sr = new StreamReader(file))
            {
                string line = sr.ReadLine();
                while (line != null)
                {
                    int hash = line.GetHashCode();
                    if (hs.Contains(hash)) repeat++;
                    else hs.Add(hash);

                    line = sr.ReadLine();
                }
            }
            hs.Clear();
            return repeat;
        }

        public static void UnRepeat(string file)
        {
            var fi = new FileInfo(file);
            if (fi.Exists == false) return;
            HashSet<int> hs = new HashSet<int>();
            int repeat = 0;
            using (StreamWriter sw = new StreamWriter(fi.FullName + "U.txt"))
            {
                using (var sr = new StreamReader(file))
                {
                    string line = sr.ReadLine();
                    while (line != null)
                    {
                        int hash = line.GetHashCode();
                        if (!hs.Contains(hash))
                        {
                            hs.Add(hash);
                            sw.WriteLine(line);
                        }

                        line = sr.ReadLine();
                    }
                }
            }
            hs.Clear();
        }

        public static void UnRepeatDir(string directory)
        {
            DirectoryInfo di = new DirectoryInfo(directory);
            if (di.Exists == false) return;
            foreach (var f in di.GetFiles())
            {
                UnRepeat(f.FullName);
            }
        }

        private static void Unduping(string file)
        {
            
        }
    }
}

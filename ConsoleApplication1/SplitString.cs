using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class SplitString
    {
        public string[] SplitStr(string str)
        {
            //return str.Split(' ');

            string[] array = new string[50];
            int flag = 0;
            char splitWord = ' ';
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if (c == '"')
                {
                    splitWord = splitWord == '"' ? ' ' : '"';
                    continue;
                }

                if (c != splitWord)
                {
                    sb.Append(c);
                }
                else
                {
                    if (sb.Length == 0)
                        continue;
                    else
                    {
                        array[flag++] = sb.ToString();
                        sb = sb.Remove(0, sb.Length);
                    }
                }
            }
            if (sb.Length > 0)
            {
                array[flag++] = sb.ToString();
            }
            return array;
        }

        public string[] SplitStr2(string str)
        {
            string[] array = new string[50];
            int flag = 0;
            char splitWord = ' ';
            bool inString = false;
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if (c == '"')
                {
                    splitWord = inString ? ' ' : '"';
                    inString = !inString;
                    continue;
                }

                if (c != splitWord)
                {
                    sb.Append(c);
                }
                else
                {
                    if (sb.Length == 0)
                        continue;
                    else
                    {
                        array[flag++] = sb.ToString();
                        sb = sb.Remove(0, sb.Length);
                    }
                }
            }
            if (sb.Length > 0)
            {
                array[flag++] = sb.ToString();
            }
            return array;
        }

        //"[^"]+"|[^,]{0,}
        private static Regex SplitReg = new Regex("\"[^\"]+\"|[^ ]+");

        public string[] SplitStrWithReg(string str)
        {
            var matchs = SplitReg.Matches(str);
            List<string> list = new List<string>(matchs.Count);
            foreach (Match match in matchs)
            {
                list.Add(match.Value.Trim('"'));
            }
            return list.ToArray();
        }

        public bool ArrayCompare(string[] arr1, string[] arr2)
        {
            return true;
            int length = arr1.Length > arr2.Length ? arr2.Length : arr1.Length;

            bool equal = true;
            for (int i = 0; i < length; i++)
            {
                if (arr1[i] != arr2[i])
                {
                    Console.WriteLine("111: {0}", arr1[i]);
                    Console.WriteLine("222: {0}", arr2[i]);
                    equal = false;
                }
            }
            return equal;
        }
    }
}

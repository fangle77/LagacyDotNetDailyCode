using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace LogMonitor
{
    class FileContentFinder
    {
        public static List<ResultItem> Find(string fileName, IMatcher matcher)
        {
            if (File.Exists(fileName) == false)
            {
                return null;
            }

            List<ResultItem> results = new List<ResultItem>();
            using (StreamReader sr = new StreamReader(fileName))
            {
                int lineNum = 0;
                string line = null;
                bool start = false;
                ResultItem result = null;
                while ((line = sr.ReadLine()) != null)
                {
                    lineNum++;
                    if (!start)
                    {
                        if (matcher.BeginMatch(line))
                        {
                            start = true;
                            result = new ResultItem(fileName);
                            result.LineNumber = lineNum;
                            result.AddContent(line);
                        }
                    }
                    else
                    {
                        if (matcher.EndMatch(line))
                        {
                            start = false;
                            results.Add(result);
                            result = null;
                        }
                        else
                        {
                            result.AddContent(line);
                        }
                    }
                }
                if (result != null) results.Add(result);
            }
            return results;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace SearchResponseSpider
{
    class GetPLPUrl
    {
        public void StartRead()
        {
            Console.WriteLine("start=========" + DateTime.Now);
            var dir = new DirectoryInfo(@"D:\ProjectDocument\PLP-perform\link");
            var files = dir.GetFiles();
            List<Action> actions = new List<Action>();
            foreach (var file in files)
            {
                actions.Add(() =>
                {
                    ReadResponse(file);
                });
            }
            TaskUtility.ParallelTask(actions, 2);
            Console.WriteLine("end=========" + DateTime.Now);
            Console.Read();
        }

        public void StartFilter()
        {
            DirectoryInfo dir = new DirectoryInfo(@"E:\OwenProject\Test\SearchResponseSpider\bin\Release\Logs");
            var files = dir.GetFiles("h*.log");
            Logger log = new Logger("filter");
            foreach (var file in files)
            {
                string[] lines = File.ReadAllLines(file.FullName);
                foreach (var line in lines)
                {
                    if (!AppConfig.Instance.IsPLPUrl(line))
                    {
                        log.Log(line);
                        Console.WriteLine(line);
                    }
                }
            }
            log.FlushToFile();
            Console.ReadLine();
        }

        private void ReadResponse(FileInfo file)
        {
            string site = file.Name.Replace(file.Extension, "").Replace("h_", "");
            Logger logger = new Logger(file.Name);
            string taskDomain = AppConfig.Instance.GetTaskDomain(site);
            if (string.IsNullOrEmpty(taskDomain))
            {
                return;
            }

            string[] lines = File.ReadAllLines(file.FullName);
            foreach (string line in lines)
            {
                string url = line.Trim();
                if (string.IsNullOrEmpty(url)) continue;
                
                //if (AppConfig.Instance.IsPLPUrl(url)) continue;
                if (IsNotPlp(url)) continue;

                url = url.Replace(UrlUtility.GetDomain(url), taskDomain);

                url = UrlUtility.AddQuery(url, "debug", "Y");


                string pathAndQuery = url.Replace(UrlUtility.GetDomain(url), "").TrimStart('/');

                //int idx = pathAndQuery.IndexOfAny("/?".ToCharArray());
                //string path = idx > 0 ? pathAndQuery.Substring(0, idx) : pathAndQuery;

                //if (hasReadUrl.Contains(path)) continue;
                Console.WriteLine(url);
                try
                {
                    string response = NetRequest.GetRequest(url);
                    if (response.IndexOf("Task String = ") >= 0)
                    {
                        logger.Log(url);

                        //hasReadUrl.Add(path);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            logger.FlushToFile();
        }

        private bool IsNotPlp(string url)
        {
            string notPlp = "/p/";
            if (url.IndexOf(notPlp) >= 0) return true;

            return false;
        }

        private static HashSet<string> hasReadUrl = new HashSet<string>();
    }
}

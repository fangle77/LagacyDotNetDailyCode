using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace SearchResponseSpider
{
    class SpiderTaskManager
    {
        private int ThreadCount = System.Environment.ProcessorCount;
        private Logger logger = new Logger("TaskString" + DateTime.Now.ToString("-yy-MM-dd HHmmss"));
        private string hashCodeUrlMapping = "response-url-mapping.txt";

        public SpiderTaskManager() { }
        public SpiderTaskManager(int threadCount)
        {
            if (threadCount <= 0 || threadCount > 8) throw new ArgumentOutOfRangeException("argument: threadCount, must be an positive integer greater than 0 and less than 8. ");
            ThreadCount = threadCount;
        }

        public event Action<string> TaskInfoEvent;

        private void Info(string info)
        {
            if (TaskInfoEvent != null) TaskInfoEvent(info);
        }

        private List<FileInfo> GetUrlFile()
        {
            if (AppConfig.Instance.UrlDirectory.Exists == false)
            {
                Info("url-directory does not exist : " + AppConfig.Instance.UrlDirectory.FullName);
                return null;
            }

            return AppConfig.Instance.UrlDirectory.GetFiles().ToList();
        }

        private DirectoryInfo CreateDirectory(FileInfo file)
        {
            var dir = AppConfig.Instance.GetSiteDirectory(file.Name.Replace(file.Extension, ""));
            if (!dir.Exists)
            {
                Directory.CreateDirectory(dir.FullName);
                Info("Create directory: " + dir.FullName);
            }

            return dir;
        }

        public void Start()
        {
            var files = GetUrlFile();
            List<Action> actionList = new List<Action>(files.Count);
            foreach (var file in files)
            {
                actionList.Add(() =>
                {
                    ReadResponse(file);
                });
            }
            TaskUtility.ParallelTask(actionList, ThreadCount);
        }

        private void ReadResponse(FileInfo file)
        {
            string site = file.Name.Replace(file.Extension, "");

            string taskDomain = AppConfig.Instance.GetTaskDomain(site);
            if (string.IsNullOrEmpty(taskDomain))
            {
                Info("unknow site: " + site);
                return;
            }

            string siteBase, searchInterFace;
            siteBase = AppConfig.Instance.GetWebSiteConfig(site, out searchInterFace);
            ConfigModify configModify = new ConfigModify(siteBase, searchInterFace);

            configModify.Modify();

            string[] lines = File.ReadAllLines(file.FullName);
            QuidsiSearchSpider spider = new QuidsiSearchSpider();
            foreach (string line in lines)
            {
                try
                {
                    string url = line.Trim();
                    if (url == string.Empty) continue;
                    if (AppConfig.Instance.IsPLPUrl(url) == false)
                    {
                        Info("not a plp url: " + url);
                        continue;
                    }

                    string realUrl = url.Replace(UrlUtility.GetDomain(url), taskDomain);

                    SpiderTask spiderTask = new SpiderTask();
                    spiderTask.Url = realUrl;
                    spider.GetMercadoResponseWithMercadoTestPage(spiderTask);

                    if (spiderTask.SpiderTaskItems == null || spiderTask.SpiderTaskItems.Count == 0)
                    {
                        Info("capture task string empty, url= " + realUrl);
                        continue;
                    }

                    SaveResponse(file, realUrl, spiderTask);
                    Info("finish url: " + realUrl);
                }
                catch (Exception ex)
                {
                    Info("error: " + ex.Message + "; url:" + line);
                }
            }

            UnDunplicateMappingFile(file);

            configModify.Restore();
        }

        private void SaveResponse(FileInfo file, string url, SpiderTask spiderTask)
        {
            var dir = CreateDirectory(file);
            foreach (SpiderTaskItem spiderTaskItem in spiderTask.SpiderTaskItems)
            {
                if (string.IsNullOrEmpty(spiderTaskItem.ResponseXml))
                {
                    Info(string.Format("capture mercado response empty, url={0} , Task String = {1}", url, spiderTaskItem.TaskString));
                    continue;
                }

                string taskHash = MD5Hasher.Md5Hash(spiderTaskItem.TaskString);

                if (AppConfig.Instance.LogTaskString)
                {
                    logger.Log("==url==" + url);
                    logger.Log("==task has==" + taskHash);
                    logger.Log(spiderTaskItem.TaskString);
                    logger.Log("=========================================");
                    logger.FlushToFile();
                }

                //save response
                string responseFile = Path.Combine(dir.FullName, taskHash + ".xml");
                File.WriteAllText(responseFile, spiderTaskItem.ResponseXml);

                //save mapping
                string url_taskHas = taskHash + "\t" + url + Environment.NewLine;
                File.AppendAllText(Path.Combine(dir.FullName, hashCodeUrlMapping), url_taskHas);
            }
        }

        private void UnDunplicateMappingFile(FileInfo file)
        {
            var dir = CreateDirectory(file);
            var mappingFile = Path.Combine(dir.FullName, hashCodeUrlMapping);

            if (!File.Exists(mappingFile)) return;

            HashSet<string> hashs = new HashSet<string>();
            var lines = File.ReadAllLines(mappingFile);
            List<string> newList = new List<string>();
            foreach (string line in lines)
            {
                if (hashs.Contains(line)) continue;
                hashs.Add(line);

                string[] values = line.Split('\t');
                if (values.Length < 2) continue;
                string hash = values[0].Trim();
                string url = values[1].Trim();

                if (File.Exists(Path.Combine(dir.FullName, hash + ".xml")))
                {
                    newList.Add(hash + "\t" + url);
                }
            }

            newList.Sort((s1, s2) =>
            {
                return s1.Split('\t')[1].CompareTo(s2.Split('\t')[1]);
            });

            StringBuilder sb = new StringBuilder();
            newList.ForEach(l => { sb.AppendLine(l); });
            sb.AppendLine();
            File.WriteAllText(mappingFile, sb.ToString());
        }
    }
}

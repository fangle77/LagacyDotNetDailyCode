using System.Collections.Generic;

namespace SearchResponseSpider
{
    public class SpiderTask
    {
        public SpiderTask()
        {
            SpiderTaskItems = new List<SpiderTaskItem>();
        }
        public string Url { get; set; }
        public List<SpiderTaskItem> SpiderTaskItems { get; set; }
    }

    public class SpiderTaskItem
    {
        public string TaskString { get; set; }
        public string TaskHash { get; set; }
        public string ResponseXml { get; set; }
    }

}

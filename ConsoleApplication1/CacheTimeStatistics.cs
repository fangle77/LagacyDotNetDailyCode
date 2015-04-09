using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    [Serializable]
    public class CacheTimeStatistics
    {
        private readonly ConcurrentDictionary<string, CacheTimeInfo> cacheTimeInfos = new ConcurrentDictionary<string, CacheTimeInfo>();

        public ConcurrentDictionary<string, CacheTimeInfo> CacheTimeInfos
        {
            get { return cacheTimeInfos; }
        }

        public void Record(string cacheKey)
        {
            if (cacheTimeInfos.ContainsKey(cacheKey))
            {
                cacheTimeInfos[cacheKey].Record();
            }
            else
            {
                cacheTimeInfos.TryAdd(cacheKey, new CacheTimeInfo(cacheKey));
            }
        }
    }

    [Serializable]
    public class CacheTimeInfo
    {
        private const int RecordCapacity = 20;
        private readonly DateTime baseTime;
        private readonly double[] recordSeparations;

        private int recordCount = 0;
        public string CacheKey { get; set; }

        public DateTime BaseTime { get { return baseTime; } }

        public CacheTimeInfo(string cacheKey)
        {
            CacheKey = cacheKey;
            baseTime = DateTime.Now;
            recordSeparations = new double[RecordCapacity];
        }

        public void Record()
        {
            recordSeparations[recordCount++ % RecordCapacity] = DateTime.Now.Subtract(baseTime).TotalSeconds;
        }

        public List<DateTime> TimeList
        {
            get
            {
                var list = new List<DateTime>(RecordCapacity);
                for (int i = 0; i < recordSeparations.Length; i++)
                {
                    if (recordSeparations[i] > 0)
                    {
                        list.Add(baseTime.AddSeconds(recordSeparations[i]));
                    }
                }
                list.Sort();
                return list;
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("CacheKey={0}, ", CacheKey)
                .AppendFormat("BaseTime={0}, ", baseTime);

            builder.Append("TimeList=");
            foreach (var time in TimeList)
            {
                builder.AppendFormat("{0},", time);
            }
            return builder.ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class TimeUtility
    {
        private static readonly DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        private const int UtcBeijing = 8;

        public static string GmtNow(string format)
        {
            return DateTime.UtcNow.ToString(format);
        }

        public static string BeijingUtcTimeNow(string format)
        {
            return DateTime.UtcNow.AddHours(UtcBeijing).ToString(format);
        }

        public static long BeijingUtcTimeNowInMillis()
        {
            return (long)(ToBejingUtcTime(DateTime.Now) - Jan1st1970).TotalMilliseconds;
        }

        public static DateTime ToBejingUtcTime(DateTime localTime)
        {
            return localTime.ToUniversalTime().AddHours(UtcBeijing);
        }

        public static string ToBejingUtcTime(DateTime localTime, string format)
        {
            return ToBejingUtcTime(localTime).ToString(format);
        }

        public static long ToBejingUtcTimeInMillis(DateTime localTime)
        {
            return (long)(ToBejingUtcTime(localTime) - Jan1st1970).TotalMilliseconds;
        }
    }
}

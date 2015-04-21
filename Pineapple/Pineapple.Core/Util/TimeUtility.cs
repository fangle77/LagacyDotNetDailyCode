using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pineapple.Core.Util
{
    public class TimeUtility
    {
        private static readonly DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static long ToUtcTimeInMillis(DateTime localTime)
        {
            return (long)(localTime.ToUniversalTime() - Jan1st1970).TotalMilliseconds;
        }
    }
}
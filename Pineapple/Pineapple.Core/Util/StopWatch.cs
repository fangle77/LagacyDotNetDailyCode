using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pineapple.Core.Util
{
    public class StopWatch
    {
        private DateTime startTime;

        public StopWatch()
        {
            startTime = DateTime.Now;
        }

        public void Reset()
        {
            startTime = DateTime.Now;
        }

        public long ElapsedMs
        {
            get
            {
                return (long)DateTime.Now.Subtract(startTime).TotalMilliseconds;
            }
        }
    }
}

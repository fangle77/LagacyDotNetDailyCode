using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace LogMonitor
{
    class ErrorMatcher : IMatcher
    {
        private static Regex TimeFormat = new Regex("");//yyyy-MM-dd HH:mm:ss

        public bool BeginMatch(string content)
        {
            return !string.IsNullOrEmpty(content) && content.IndexOf("] ERROR", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        public bool EndMatch(string content)
        {
            return !string.IsNullOrEmpty(content)
                && content.TrimStart().StartsWith("2014", StringComparison.OrdinalIgnoreCase);
        }
    }
}

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
        private string Year = DateTime.Now.ToString("yyyy-");

        public bool BeginMatch(string content)
        {
            return !string.IsNullOrEmpty(content)
                && (content.IndexOf("] ERROR", StringComparison.OrdinalIgnoreCase) >= 0
                || (content.LastIndexOf("Exception", StringComparison.OrdinalIgnoreCase) > 0
                && content.LastIndexOf("ExceptionInterceptor", StringComparison.OrdinalIgnoreCase) < 0
                ));
        }

        public bool EndMatch(string content)
        {
            return !string.IsNullOrEmpty(content)
                && content.TrimStart().StartsWith(Year, StringComparison.OrdinalIgnoreCase);
        }
    }
}

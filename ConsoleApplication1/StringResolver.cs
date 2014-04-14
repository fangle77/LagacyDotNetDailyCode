using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleApplication1
{
    static class StringResolver
    {
        public static string RemoveRedundantSpace(this string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            Regex reg = new Regex(@"\s+");
            return reg.Replace(input, " ");
        }
        public static string RemoveSpace(this string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            Regex reg = new Regex(@"\s+");
            return reg.Replace(input, "");
        }
    }
}

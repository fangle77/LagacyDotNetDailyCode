using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pineapple.Core.Util
{
    public static class StringExtention
    {
        public static int ToInt(this string input)
        {
            int value = 0;
            int.TryParse(input, out value);
            return value;
        }

        public static int ToInt(this string input, int defaltValue)
        {
            int value = 0;
            if (int.TryParse(input, out value))
                return value;
            else
            {
                return defaltValue;
            }
        }
    }
}

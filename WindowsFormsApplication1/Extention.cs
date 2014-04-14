using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebServerManager
{
   public static class Extention
    {
       public static int ToInt32(this string s)
       {
           int i = 0;
           int.TryParse(s, out i);
           return i;
       }
    }
}

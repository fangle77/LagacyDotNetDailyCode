using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public static class ObjectExternal
    {
        public static long GetSize(this object obj)
        {
            long size = 0;
            using (Stream s = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(s, obj);
                size = s.Length;
            }
            return size;
        }
    }
}

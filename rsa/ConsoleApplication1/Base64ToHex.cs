using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Base64ToHex
    {
        public static string ConvertToHex(string base64String)
        {
            byte[] data = Convert.FromBase64String(base64String);
            string hex = BitConverter.ToString(data);
            return hex;
        }

        public static void ConvertAndSave(string base64)
        {
            string hex = ConvertToHex(base64);
            File.WriteAllText("hex.txt",hex);
        }
    }
}

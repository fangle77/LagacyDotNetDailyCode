using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class EncryptDeEncrypt
    {
        public static string Encrypt(string input)
        {
            string ss = input.Replace("SYS", "1");
            Int64 ii = Convert.ToInt64(ss);
            Int64 num = ii * ii * 17;
            byte[] bytes = Encoding.UTF8.GetBytes(num.ToString());
            return Convert.ToBase64String(bytes);
        }

        public static string Decrypt(string input)
        {
            byte[] by = Convert.FromBase64String(input);
            string abc = Encoding.UTF8.GetString(by);
            Int64 s = Convert.ToInt64(abc) / 17;
            string customernumber = ("SYS" + Convert.ToInt64(Math.Sqrt(s))).Replace("SYS1", "SYS");
            return customernumber;
        }


        public static string EncryptXor(string input)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(bytes);
        }

        public static string DecryptXor(string input)
        {
            return System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(input));
        }
    }

    public class EncryptDeEncryptTest
    {
        public void RunTest()
        {
            Console.WriteLine(EncryptDeEncrypt.DecryptXor("AKTMbBOCH0uJ5PUenu5ungEAAAAI/+d/LLIkFjqcNamS+utrgWQf1TTwhGOYYdxU6GgVTQ=="));
            Console.Read();
            var customerNubmsers = new List<string>()
                                       {
                                           "SYS3614204","SYS3614205","SYS3614206","SYS3770745","SYS3614204","SYS3770744"
                                       };

            foreach (string cn in customerNubmsers)
            {
                Console.WriteLine(cn);

                string encode = EncryptDeEncrypt.Encrypt(cn);
                Console.WriteLine(encode);

                string dncode = EncryptDeEncrypt.Decrypt(encode);
                Console.WriteLine(dncode);

                Console.WriteLine("=======================");
                Console.WriteLine();
            }
        }
    }
}

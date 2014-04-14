using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ConsoleApplication1
{
    class MD5Hasher
    {
        /// <summary>
        /// Hash functions map binary strings of an arbitrary length to small binary strings of a fixed length. 
        /// A cryptographic hash function has the property that it is computationally infeasible to find two distinct inputs that hash to the same value; 
        /// that is, hashes of two sets of data should match if the corresponding data also matches. 
        /// Small changes to the data result in large, unpredictable changes in the hash.
        ///The hash size for the MD5 algorithm is 128 bits.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>returns the hash as a 32-character</returns>
        public static string Md5Hash(string input)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        /// <summary>
        /// Encrypt a string depend on sha256 algorithm
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Encrypted string.</returns>
        public static byte[] SHA1Hash(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }

            SHA1Managed hashAlgorithm = new SHA1Managed();
            try
            {
                return hashAlgorithm.ComputeHash(Encoding.Default.GetBytes(input));
            }
            finally
            {
                hashAlgorithm.Clear();
            }
        }
    }
}

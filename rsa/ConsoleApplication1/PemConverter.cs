using System;

using System.Text;

using System.Security.Cryptography;

using System.Web;

using System.IO;

//http://blog.sina.com.cn/s/blog_4fcd1ea30100yh4s.html

namespace Thinhunan.Cnblogs.Com.RSAUtility
{

    /// <summary>
    /// Author http://thinhunan.cnblogs.com
    /// //http://www.cnblogs.com/thinhunan/archive/2009/09/10/ConvertPem2048ToRSAParemeters.html
    /// </summary>
    public class PemConverter
    {

        /// <summary>
        /// 将pem格式公钥(1024 or 2048)转换为RSAParameters
        /// </summary>
        /// <param name="pemFileConent">pem公钥内容</param>
        /// <returns>转换得到的RSAParamenters</returns>
        public static RSAParameters ConvertFromPemPublicKey(string pemFileConent)
        {

            if (string.IsNullOrEmpty(pemFileConent))
            {

                throw new ArgumentNullException("pemFileConent", "This arg cann't be empty.");

            }

            pemFileConent = pemFileConent.Replace("-----BEGIN PUBLIC KEY-----", "").Replace("-----END PUBLIC KEY-----", "").Replace("\n", "").Replace("\r", "");

            byte[] keyData = Convert.FromBase64String(pemFileConent);

            bool keySize1024 = (keyData.Length == 162);

            bool keySize2048 = (keyData.Length == 294);

            if (!(keySize1024 || keySize2048))
            {

                throw new ArgumentException("pem file content is incorrect, Only support the key size is 1024 or 2048");

            }

            byte[] pemModulus = (keySize1024 ? new byte[128] : new byte[256]);

            byte[] pemPublicExponent = new byte[3];

            Array.Copy(keyData, (keySize1024 ? 29 : 33), pemModulus, 0, (keySize1024 ? 128 : 256));

            Array.Copy(keyData, (keySize1024 ? 159 : 291), pemPublicExponent, 0, 3);

            RSAParameters para = new RSAParameters();

            para.Modulus = pemModulus;

            para.Exponent = pemPublicExponent;
            return para;

        }



        /// <summary>
        /// 将pem格式私钥(1024 or 2048)转换为RSAParameters
        /// </summary>
        /// <param name="pemFileConent">pem私钥内容</param>
        /// <returns>转换得到的RSAParamenters</returns>
        public static RSAParameters ConvertFromPemPrivateKey(string pemFileConent)
        {

            if (string.IsNullOrEmpty(pemFileConent))
            {

                throw new ArgumentNullException("pemFileConent", "This arg cann't be empty.");

            }

            pemFileConent = pemFileConent.Replace("-----BEGIN RSA PRIVATE KEY-----", "").Replace("-----END RSA PRIVATE KEY-----", "").Replace("\n", "").Replace("\r", "");

            byte[] keyData = Convert.FromBase64String(pemFileConent);



            bool keySize1024 = (keyData.Length == 609 || keyData.Length == 610);

            bool keySize2048 = (keyData.Length == 1190 || keyData.Length == 1192);


            if (!(keySize1024 || keySize2048))
            {

                throw new ArgumentException("pem file content is incorrect, Only support the key size is 1024 or 2048");

            }



            int index = (keySize1024 ? 11 : 12);

            byte[] pemModulus = (keySize1024 ? new byte[128] : new byte[256]);

            Array.Copy(keyData, index, pemModulus, 0, pemModulus.Length);



            index += pemModulus.Length;

            index += 2;

            byte[] pemPublicExponent = new byte[3];

            Array.Copy(keyData, index, pemPublicExponent, 0, 3);



            index += 3;

            index += 4;

            if ((int)keyData[index] == 0)
            {

                index++;

            }

            byte[] pemPrivateExponent = (keySize1024 ? new byte[128] : new byte[256]);

            Array.Copy(keyData, index, pemPrivateExponent, 0, pemPrivateExponent.Length);



            index += pemPrivateExponent.Length;

            index += (keySize1024 ? ((int)keyData[index + 1] == 64 ? 2 : 3) : ((int)keyData[index + 2] == 128 ? 3 : 4));

            byte[] pemPrime1 = (keySize1024 ? new byte[64] : new byte[128]);

            Array.Copy(keyData, index, pemPrime1, 0, pemPrime1.Length);



            index += pemPrime1.Length;

            index += (keySize1024 ? ((int)keyData[index + 1] == 64 ? 2 : 3) : ((int)keyData[index + 2] == 128 ? 3 : 4));

            byte[] pemPrime2 = (keySize1024 ? new byte[64] : new byte[128]);

            Array.Copy(keyData, index, pemPrime2, 0, pemPrime2.Length);



            index += pemPrime2.Length;

            index += (keySize1024 ? ((int)keyData[index + 1] == 64 ? 2 : 3) : ((int)keyData[index + 2] == 128 ? 3 : 4));

            byte[] pemExponent1 = (keySize1024 ? new byte[64] : new byte[128]);

            Array.Copy(keyData, index, pemExponent1, 0, pemExponent1.Length);



            index += pemExponent1.Length;

            index += (keySize1024 ? ((int)keyData[index + 1] == 64 ? 2 : 3) : ((int)keyData[index + 2] == 128 ? 3 : 4));

            byte[] pemExponent2 = (keySize1024 ? new byte[64] : new byte[128]);

            Array.Copy(keyData, index, pemExponent2, 0, pemExponent2.Length);



            index += pemExponent2.Length;

            index += (keySize1024 ? ((int)keyData[index + 1] == 64 ? 2 : 3) : ((int)keyData[index + 2] == 128 ? 3 : 4));

            byte[] pemCoefficient = (keySize1024 ? new byte[64] : new byte[128]);

            Array.Copy(keyData, index, pemCoefficient, 0, pemCoefficient.Length);



            RSAParameters para = new RSAParameters();

            para.Modulus = pemModulus;

            para.Exponent = pemPublicExponent;

            para.D = pemPrivateExponent;

            para.P = pemPrime1;

            para.Q = pemPrime2;

            para.DP = pemExponent1;

            para.DQ = pemExponent2;

            para.InverseQ = pemCoefficient;

            return para;

        }

    }

    public class PemConverterTest
    {
        //// qs ...
        private static string publickKey = @"MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCpKPkxNcSWkaEZv1KzSKFg3o2GD3Q7qzWXtvKVxa0Vk8hRT8fQATsel3bkpG8W5gp4d26hOcrZaSpmY8VQle+ME/UWDM5JhEZQYSOeWiunXijZ0UvDpbojcicm096cQsynvnWo9z+C4I0C3t3dCZ/m1lE9i9IgP0wnQ7emS2csFQIDAQAB";
        //private static string privateKey = @"MIICdwIBADANBgkqhkiG9w0BAQEFAASCAmEwggJdAgEAAoGBAKko+TE1xJaRoRm/UrNIoWDejYYPdDurNZe28pXFrRWTyFFPx9ABOx6XduSkbxbmCnh3bqE5ytlpKmZjxVCV74wT9RYMzkmERlBhI55aK6deKNnRS8OluiNyJybT3pxCzKe+daj3P4LgjQLe3d0Jn+bWUT2L0iA/TCdDt6ZLZywVAgMBAAECgYBY7Y5bPW30zehIVdlPIQ6dk0IJSRSMzcvlzyqma/47Cq7TeEKN6ie/RFcfigZQnmzAueCx52TpeKzumOLBI6GDQgnSBx8RVPjx/p8XNGUxw4UQiUG+EW7iNqLEKKKtnwfjYNqFcNAsn1b8CCMORozIwYLhn5ihkBiz10ITQURQoQJBAOBxhfpDYFNhlYHuP3qn7VqKZCzzbAfT6OOuc8GoEJvHeBnKczFrUzMmbiberVn5g/l7rJ9rfakY32f5KYP73XkCQQDA8Z03nqKhqaxdEG2JgStcGgMT1htkWeSmUNoKaY+SBor3BVLrd30VZGp51zzAcTY9pdUG7PQXystxn6Htskh9AkEAhnk+Dp4DvrF/BGQcwH6QpWi5cH1AQshihtflHyh1GwC+IqW7suZc6Q6jfMJ6Fqh6vCWvXaznk0MFx6PvjdZ/8QJBAIZTL7sbK+oUsDUSTNAgJ0m1qlLTCrrwgmjvfP0mxJdLCtAy2qmnxGNyR1aP7HGl37dHjmmF6eHug3iVRCyxpBkCQATsf3oAPErQ0AT2wB0Uof6uv0x+/6r1q0cuR/R3Htd3Sz9GIx+4v79p41DBcSMyr2fubtXp6WiHSlvseiKigWU=";

        //test..
        //private static string publickKey = @"MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDwIqfvxEjqHu8048x4wJ5EId6ASAbWdH5fzgHxvew5kXqECMNcXzRqDVnDVPQT41UeZs8HxouBE+ZA8DfnVlHwP4EIeigOUaqy0sseKpO71tupFU+2LjpcF6O7cVuLjt6476iYfSyrssK4hnmzVYGZNz16OSR9z/SuTd8BhohG4QIDAQAB";
        //private static string privateKey = @"MIICXgIBAAKBgQDwIqfvxEjqHu8048x4wJ5EId6ASAbWdH5fzgHxvew5kXqECMNcXzRqDVnDVPQT41UeZs8HxouBE+ZA8DfnVlHwP4EIeigOUaqy0sseKpO71tupFU+2LjpcF6O7cVuLjt6476iYfSyrssK4hnmzVYGZNz16OSR9z/SuTd8BhohG4QIDAQABAoGBAOmEmhEUrN9XU8D4IVfv4DhbQ1c2M8gKovYhjEx8J6LX8O9C4lAKmRrkfrzv+Sb59EVLLtrd3b2ZD1lpAMQrciMwC5PAa8da/J++lR1VjM5GbzqKjGtfx3WQlzNE1ZaZ2FSY8lAPMM4uLczyD79PJQBsGCcx3KDJRR5ENp6an5cRAkEA/m1FEqol/KKhxOyGsK4GVuansBXhrAgpwMlYLT+vF0gy1jzYQDNNQXzeQFYH6gZY66RTYFl3JPNL8KXLyhwDLQJBAPGew6xkLBoYi4IO9I+NP/gIHzSiQeEl2OxZsgZiz0Yh5E9ndwMr87jTX/4ZBwNlDC0E+MXsJpMSvTFNpw4rcwUCQQC5FU5JLKOjq79YnOPChWYxM2vLKa/YULvm9dGCYTCDFE9/EBYUZf2OZULctHjfYqyvBwRsM8j7hU26CzI7nbMlAkAAkVjwXMPlw80AHzzf4XsXAB3ip8bz2nzqAUPz0+OczJOWxC15am8GLij5leF4VpJywKI9BNMKYW7kYMRVujBpAkEA7gQ8MGqjjrCAfOzrrC9ZuVdGRfEjUEdHMqiF+js7XNBvnT5lBznUOd+eta6CGo7S5hjU7D3CEzmVGQfxUsRZ1w==";

        private static string privateKey = @"66uzUVbmbmL5M1eLU2U7sGOr5/vpdKO2MMXQJJ6Bfy/mZI2lc5KTTnbvLbUn4/V5I74fmAV7OPKrMhlAkpzlaivt73cApOiZ0p+82M5g2dhxO1YzMfvPf884SZB3RfGnRmj1JG26rDXu/f/z6h5AQFrghkFqMEn7h0xvvfjkpbe0U/lYbNCxq53bDkMyjVmAxdbcIRN32z7ayGZnzOmip6sgyIohCJsZ5RVBM12chYvmpXY5R3BTl5SRrJmHrW2tg5jXDtPWBW84+gzCGEaKCeQg72xwLds/xkAg3a0IaPExw+NvQk7UPq7ckIA5IOBIrQIC/fILHm67Y5QfwEpm3It395uhDSSgFBfN32YYrBwpfM8SK8k7bAWngJ9isQrXQmxDvt3OfhRaUhYefI6NJVfuG85xUom9l1+rpIPiXjJ9vvVgBIJSnSQjkx7ka81V7L3F56JnLEAQB2hEHqa2zmgZNnwnSzm8t1P6x3Fn1FuyhSbybPcyyfhSdIz/JcQI4k6CtC6OjY1FHgShZMHEL7tQLY19cRmFhFmJK1gErqv0VgO5Isisz/hV4csmTF48XEJd5A8SCgI8qcaYLgCKEheRVgOWYkxpv/TSSehL6kuW2oGEswS7+1fbavKQmyKXXoA48TVjM9yd6nMgUii+Jti+9LC2aaBUu9wHJ9SpSNt3ulfKuPmhAlbcWBXTqT1Zafrc7IM1n9bwFl+aR4JSaarwqBQEW54ACRL+z2Lo1ZyOqXg61EskKjL/7Ht+DrLjyQP/MgKSH1TM138f3Qwi6909NI9t3xHlRrqYHyCrYC2Yq/KOqAgQ/g==";

        public static void Test()
        {
            TestSignAndEncrypt(privateKey, publickKey);
        }

        public static void TestSignAndEncrypt(string privateKey, string publicKey)
        {
            //sign
            RSAParameters para = PemConverter.ConvertFromPemPrivateKey(privateKey);
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.ImportParameters(para);
            byte[] testData = Encoding.UTF8.GetBytes("hello");
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] signData = rsa.SignData(testData, md5);

            //verify
            RSAParameters paraPub = PemConverter.ConvertFromPemPublicKey(publicKey);
            RSACryptoServiceProvider rsaPub = new RSACryptoServiceProvider();
            rsaPub.ImportParameters(paraPub);
            if (rsaPub.VerifyData(testData, md5, signData))
            {
                Console.WriteLine("verify sign successful");
            }
            else
            {
                Console.WriteLine("verify sign failed");
            }

            //encrypt and decrypt data
            byte[] encryptedData = rsaPub.Encrypt(testData, false);
            byte[] decryptedData = rsa.Decrypt(encryptedData, false);

            Console.WriteLine(Encoding.UTF8.GetString(decryptedData));
        }
    }

}
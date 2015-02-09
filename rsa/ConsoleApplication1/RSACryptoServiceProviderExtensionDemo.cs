/*********************************************************************************
 * Copyright (c) 2013, Christian Etter info@christian-etter.de
 * 
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 * 
 * 1. Redistributions of source code must retain the above copyright notice, this
 *    list of conditions and the following disclaimer. 
 * 2. Redistributions in binary form must reproduce the above copyright notice,
 *    this list of conditions and the following disclaimer in the documentation
 *    and/or other materials provided with the distribution. 
 * 
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
 * ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR
 * ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
 * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
 * LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
 * ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
 * SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 *********************************************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

/// <summary>Demonstrates usage of extension methods to RSACryptoServiceProvider which allow loading of public/private keys in PEM and DER format.</summary>
public class RSACryptoServiceProviderExtensionDemo
{
    /// <summary>Demonstrates usage of the RSACryptoServiceProviderExtension functionality.</summary>
    public static void Main1(string[] args)
    {
        string sDataToSign =
            "Sed ut perspiciatis, unde omnis iste natus error sit voluptatem accusantium " +
            "doloremque laudantium, totam rem aperiam eaque ipsa, quae ab illo inventore " +
            "veritatis et quasi architecto beatae vitae dicta sunt, explicabo. Nemo enim " +
            "ipsam voluptatem, quia voluptas sit, aspernatur aut odit aut fugit, sed quia " +
            "consequuntur magni dolores eos, qui ratione voluptatem sequi nesciunt, neque " +
            "porro quisquam est, qui dolorem ipsum, quia dolor sit amet, consectetur, " +
            "adipisci[ng] velit, sed quia non numquam [do] eius modi tempora inci[di]dunt, " +
            "ut labore et dolore magnam aliquam quaerat voluptatem.";

        byte[] dataToSign = Encoding.UTF8.GetBytes(sDataToSign);

        RSACryptoServiceProviderExtensionDemo.TestPEM(dataToSign);
        //RSACryptoServiceProviderExtensionDemo.TestDER(dataToSign);

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    /// <summary>Demonstrates signing and verifying based on DER binary public/private key.</summary>
    protected static void TestDER(byte[] dataToSign)
    {
        Console.WriteLine("Testing DER...\n");

        byte[] publicKeyDER = RSACryptoServiceProviderExtensionDemo.GetDataFromResource("RSACryptoServiceProviderExtensionPublicKey.der");
        byte[] privateKeyDER = RSACryptoServiceProviderExtensionDemo.GetDataFromResource("RSACryptoServiceProviderExtensionPrivateKey.der");

        Console.WriteLine("Public key:\n{0}\n", BitConverter.ToString(publicKeyDER).Replace("-", ""));
        Console.WriteLine("Private key:\n{0}\n", BitConverter.ToString(privateKeyDER).Replace("-", ""));

        byte[] signature;
        bool bVerifyResultOriginal;
        bool bVerifyResultModified;

        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
        {
            rsa.PersistKeyInCsp = false;
            rsa.LoadPrivateKeyDER(privateKeyDER);
            using (SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider())
                signature = rsa.SignData(dataToSign, sha1);
        }
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
        {
            rsa.PersistKeyInCsp = false;
            rsa.LoadPublicKeyDER(publicKeyDER);
            using (SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider())
                bVerifyResultOriginal = rsa.VerifyData(dataToSign, sha1, signature);
            // invalidate signature so the next check must fail
            signature[signature.Length - 1] ^= 0xFF;
            using (SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider())
                bVerifyResultModified = rsa.VerifyData(dataToSign, sha1, signature);
        }

        Console.WriteLine("DER: original signature is {0}valid.", bVerifyResultOriginal ? String.Empty : "in");
        Console.WriteLine("DER: tampered signature is {0}valid.", bVerifyResultModified ? String.Empty : "in");
        Console.WriteLine("\nDone testing DER.\n");
    }

    /// <summary>Demonstrates signing and verifying based on PEM textual public/private key.</summary>
    protected static void TestPEM(byte[] dataToSign)
    {
        Console.WriteLine("Testing PEM...\n");

        // -----BEGIN PUBLIC KEY-----...-----END PUBLIC KEY-----
        string sPublicKeyPEM = Encoding.ASCII.GetString(RSACryptoServiceProviderExtensionDemo.GetDataFromResource("RSACryptoServiceProviderExtensionPublicKey.pem"));
        // -----BEGIN RSA PRIVATE KEY-----...-----END RSA PRIVATE KEY-----
        string sPrivateKeyPEM = Encoding.ASCII.GetString(RSACryptoServiceProviderExtensionDemo.GetDataFromResource("RSACryptoServiceProviderExtensionPrivateKey.pem"));

        Console.WriteLine("Public key:\n{0}", sPublicKeyPEM);
        Console.WriteLine("Private key:\n{0}", sPrivateKeyPEM);

        byte[] signature;
        bool bVerifyResultOriginal;
        bool bVerifyResultModified;

        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
        {
            rsa.PersistKeyInCsp = false;
            rsa.LoadPrivateKeyPEM(sPrivateKeyPEM);
            using (SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider())
                signature = rsa.SignData(dataToSign, sha1);
        }
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
        {
            rsa.PersistKeyInCsp = false;
            rsa.LoadPublicKeyPEM(sPublicKeyPEM);
            using (SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider())
                bVerifyResultOriginal = rsa.VerifyData(dataToSign, sha1, signature);
            // invalidate signature so the next check must fail
            signature[signature.Length - 1] ^= 0xFF;
            using (SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider())
                bVerifyResultModified = rsa.VerifyData(dataToSign, sha1, signature);
        }

        Console.WriteLine("PEM: original signature is {0}valid.", bVerifyResultOriginal ? String.Empty : "in");
        Console.WriteLine("PEM: tampered signature is {0}valid.", bVerifyResultModified ? String.Empty : "in");
        Console.WriteLine("\nDone testing PEM.\n");
    }

    /// <summary>Finds a resource by name and returns its data.</summary>
    protected static byte[] GetDataFromResource(string sResourceFileName)
    {
        string x = RSAKeyss[sResourceFileName];

        StringBuilder sb = new StringBuilder();
        if (x.IndexOf('\n') < 0)
        {
            int length = 0;
            while (length < x.Length)
            {
                sb.Append(x.Substring(length, Math.Min(x.Length - length, 64)));
                sb.Append('\n');
                length += 64;
            }
        }
        if (sResourceFileName.IndexOf("private", StringComparison.OrdinalIgnoreCase) >= 0)
        {
            if (x.IndexOf("-----BEGIN RSA PRIVATE KEY-----", StringComparison.OrdinalIgnoreCase) < 0)
            {
                sb.Insert(0, "-----BEGIN RSA PRIVATE KEY-----\n");
            }
            if (x.LastIndexOf("-----END RSA PRIVATE KEY-----", StringComparison.OrdinalIgnoreCase) < 0)
            {
                sb.Append("-----END RSA PRIVATE KEY-----\n");
            }
        }
        else if (sResourceFileName.IndexOf("public", StringComparison.OrdinalIgnoreCase) >= 0)
        {
            if (x.IndexOf("-----BEGIN PUBLIC KEY-----", StringComparison.OrdinalIgnoreCase) < 0)
            {
                sb.Insert(0, "-----BEGIN PUBLIC KEY-----\n");
            }
            if (x.LastIndexOf("-----END PUBLIC KEY-----", StringComparison.OrdinalIgnoreCase) < 0)
            {
                sb.Append("-----END PUBLIC KEY-----\n");
            }
        }
        if (sb.Length == 0) sb.Append(x);
        string y = sb.ToString();
        return Encoding.UTF8.GetBytes(y);
    }

    private static Dictionary<string, string> RSAKeyss1 = new Dictionary<string, string>()
                                                           {
                                                               {"RSACryptoServiceProviderExtensionPrivateKey.pem",@"-----BEGIN RSA PRIVATE KEY-----
MIICXgIBAAKBgQDwIqfvxEjqHu8048x4wJ5EId6ASAbWdH5fzgHxvew5kXqECMNc
XzRqDVnDVPQT41UeZs8HxouBE+ZA8DfnVlHwP4EIeigOUaqy0sseKpO71tupFU+2
LjpcF6O7cVuLjt6476iYfSyrssK4hnmzVYGZNz16OSR9z/SuTd8BhohG4QIDAQAB
AoGBAOmEmhEUrN9XU8D4IVfv4DhbQ1c2M8gKovYhjEx8J6LX8O9C4lAKmRrkfrzv
+Sb59EVLLtrd3b2ZD1lpAMQrciMwC5PAa8da/J++lR1VjM5GbzqKjGtfx3WQlzNE
1ZaZ2FSY8lAPMM4uLczyD79PJQBsGCcx3KDJRR5ENp6an5cRAkEA/m1FEqol/KKh
xOyGsK4GVuansBXhrAgpwMlYLT+vF0gy1jzYQDNNQXzeQFYH6gZY66RTYFl3JPNL
8KXLyhwDLQJBAPGew6xkLBoYi4IO9I+NP/gIHzSiQeEl2OxZsgZiz0Yh5E9ndwMr
87jTX/4ZBwNlDC0E+MXsJpMSvTFNpw4rcwUCQQC5FU5JLKOjq79YnOPChWYxM2vL
Ka/YULvm9dGCYTCDFE9/EBYUZf2OZULctHjfYqyvBwRsM8j7hU26CzI7nbMlAkAA
kVjwXMPlw80AHzzf4XsXAB3ip8bz2nzqAUPz0+OczJOWxC15am8GLij5leF4VpJy
wKI9BNMKYW7kYMRVujBpAkEA7gQ8MGqjjrCAfOzrrC9ZuVdGRfEjUEdHMqiF+js7
XNBvnT5lBznUOd+eta6CGo7S5hjU7D3CEzmVGQfxUsRZ1w==
-----END RSA PRIVATE KEY-----
"},
 {"RSACryptoServiceProviderExtensionPublicKey.pem",@"-----BEGIN PUBLIC KEY-----
MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDwIqfvxEjqHu8048x4wJ5EId6A
SAbWdH5fzgHxvew5kXqECMNcXzRqDVnDVPQT41UeZs8HxouBE+ZA8DfnVlHwP4EI
eigOUaqy0sseKpO71tupFU+2LjpcF6O7cVuLjt6476iYfSyrssK4hnmzVYGZNz16
OSR9z/SuTd8BhohG4QIDAQAB
-----END PUBLIC KEY-----
"}                                                };

    private static Dictionary<string, string> RSAKeyss = new Dictionary<string, string>()
                                                           {
                                                               {"RSACryptoServiceProviderExtensionPrivateKey.pem",@"MIICdwIBADANBgkqhkiG9w0BAQEFAASCAmEwggJdAgEAAoGBAKko+TE1xJaRoRm/UrNIoWDejYYPdDurNZe28pXFrRWTyFFPx9ABOx6XduSkbxbmCnh3bqE5ytlpKmZjxVCV74wT9RYMzkmERlBhI55aK6deKNnRS8OluiNyJybT3pxCzKe+daj3P4LgjQLe3d0Jn+bWUT2L0iA/TCdDt6ZLZywVAgMBAAECgYBY7Y5bPW30zehIVdlPIQ6dk0IJSRSMzcvlzyqma/47Cq7TeEKN6ie/RFcfigZQnmzAueCx52TpeKzumOLBI6GDQgnSBx8RVPjx/p8XNGUxw4UQiUG+EW7iNqLEKKKtnwfjYNqFcNAsn1b8CCMORozIwYLhn5ihkBiz10ITQURQoQJBAOBxhfpDYFNhlYHuP3qn7VqKZCzzbAfT6OOuc8GoEJvHeBnKczFrUzMmbiberVn5g/l7rJ9rfakY32f5KYP73XkCQQDA8Z03nqKhqaxdEG2JgStcGgMT1htkWeSmUNoKaY+SBor3BVLrd30VZGp51zzAcTY9pdUG7PQXystxn6Htskh9AkEAhnk+Dp4DvrF/BGQcwH6QpWi5cH1AQshihtflHyh1GwC+IqW7suZc6Q6jfMJ6Fqh6vCWvXaznk0MFx6PvjdZ/8QJBAIZTL7sbK+oUsDUSTNAgJ0m1qlLTCrrwgmjvfP0mxJdLCtAy2qmnxGNyR1aP7HGl37dHjmmF6eHug3iVRCyxpBkCQATsf3oAPErQ0AT2wB0Uof6uv0x+/6r1q0cuR/R3Htd3Sz9GIx+4v79p41DBcSMyr2fubtXp6WiHSlvseiKigWU="},
 {"RSACryptoServiceProviderExtensionPublicKey.pem",@"MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCpKPkxNcSWkaEZv1KzSKFg3o2GD3Q7qzWXtvKVxa0Vk8hRT8fQATsel3bkpG8W5gp4d26hOcrZaSpmY8VQle+ME/UWDM5JhEZQYSOeWiunXijZ0UvDpbojcicm096cQsynvnWo9z+C4I0C3t3dCZ/m1lE9i9IgP0wnQ7emS2csFQIDAQAB"}                                                };

    private static Dictionary<string, string> RSAKeyss2 = new Dictionary<string, string>()
                                                           {
                                                               {"RSACryptoServiceProviderExtensionPrivateKey.pem",@"MIICXgIBAAKBgQDwIqfvxEjqHu8048x4wJ5EId6ASAbWdH5fzgHxvew5kXqECMNcXzRqDVnDVPQT41UeZs8HxouBE+ZA8DfnVlHwP4EIeigOUaqy0sseKpO71tupFU+2LjpcF6O7cVuLjt6476iYfSyrssK4hnmzVYGZNz16OSR9z/SuTd8BhohG4QIDAQABAoGBAOmEmhEUrN9XU8D4IVfv4DhbQ1c2M8gKovYhjEx8J6LX8O9C4lAKmRrkfrzv+Sb59EVLLtrd3b2ZD1lpAMQrciMwC5PAa8da/J++lR1VjM5GbzqKjGtfx3WQlzNE1ZaZ2FSY8lAPMM4uLczyD79PJQBsGCcx3KDJRR5ENp6an5cRAkEA/m1FEqol/KKhxOyGsK4GVuansBXhrAgpwMlYLT+vF0gy1jzYQDNNQXzeQFYH6gZY66RTYFl3JPNL8KXLyhwDLQJBAPGew6xkLBoYi4IO9I+NP/gIHzSiQeEl2OxZsgZiz0Yh5E9ndwMr87jTX/4ZBwNlDC0E+MXsJpMSvTFNpw4rcwUCQQC5FU5JLKOjq79YnOPChWYxM2vLKa/YULvm9dGCYTCDFE9/EBYUZf2OZULctHjfYqyvBwRsM8j7hU26CzI7nbMlAkAAkVjwXMPlw80AHzzf4XsXAB3ip8bz2nzqAUPz0+OczJOWxC15am8GLij5leF4VpJywKI9BNMKYW7kYMRVujBpAkEA7gQ8MGqjjrCAfOzrrC9ZuVdGRfEjUEdHMqiF+js7XNBvnT5lBznUOd+eta6CGo7S5hjU7D3CEzmVGQfxUsRZ1w=="},
 {"RSACryptoServiceProviderExtensionPublicKey.pem",@"MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDwIqfvxEjqHu8048x4wJ5EId6ASAbWdH5fzgHxvew5kXqECMNcXzRqDVnDVPQT41UeZs8HxouBE+ZA8DfnVlHwP4EIeigOUaqy0sseKpO71tupFU+2LjpcF6O7cVuLjt6476iYfSyrssK4hnmzVYGZNz16OSR9z/SuTd8BhohG4QIDAQAB"}                                        
    };
}

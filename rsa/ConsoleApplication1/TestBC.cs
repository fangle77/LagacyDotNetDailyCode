using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;    // X509Certificate2
using System.IO;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Signers;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Crypto.Engines;  //IAsymmetricBlockCipher engine = new RsaEngine();
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Asn1.X509; //X509Name
using Org.BouncyCastle.X509;
using Org.BouncyCastle.Utilities.Collections;   //X509V3CertificateGenerator
using Org.BouncyCastle.Asn1.Pkcs;   //PrivateKeyInfo
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Asn1;

namespace ConsoleApplication1
{
    class TestBC
    {
        public static void Main1(string[] args)
        {
            //公钥和密钥的生成，并加密解密测试
            //RsaKeyGeneratorTest();    //done!!!!!

            byte[] msg = Encoding.UTF8.GetBytes("abcdefg");

            string priKeyString = File.ReadAllText(@"E:\OwenProject\RSA\pc8_bc.pem");
            string pubKeyString = File.ReadAllText(@"E:\OwenProject\RSA\pc8_bc_pub.pem");

            using (TextReader priReader = new StringReader(priKeyString)
                , pubReader = new StringReader(pubKeyString))
            {
                PemReader pemReader = new PemReader(priReader);
                var obj = pemReader.ReadObject();
                var pri = obj as RsaPrivateCrtKeyParameters;
                

                //RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                //RSAParameters p = new RSAParameters();
                //p.DP = pri.DP.ToByteArray();
                //p.DQ = pri.DQ.ToByteArray();
                //p.Exponent = pri.Exponent.ToByteArray();
                //p.P = pri.P.ToByteArray();
                //p.Q = pri.Q.ToByteArray();
                //p.Modulus = pri.Modulus.ToByteArray();
                //p.D = pri.PublicExponent.ToByteArray();
                //p.InverseQ = pri.QInv.ToByteArray();
                //rsa.ImportParameters(p);

                PemReader pemReaderPub = new PemReader(pubReader);
                var objPub = pemReaderPub.ReadObject();
                var pub = objPub as RsaKeyParameters;

                //AsymmetricCipherKeyPair kp = new AsymmetricCipherKeyPair(pri, pub);
                RsaDigestSigner signer = new RsaDigestSigner(new Sha1Digest());
                signer.Init(true, pri);
                signer.BlockUpdate(msg, 0, msg.Length);
                byte[] sig = signer.GenerateSignature();

                Console.WriteLine(Convert.ToBase64String(sig));

                signer.Init(false, pub);
                signer.BlockUpdate(msg, 0, msg.Length);
                bool valid = signer.VerifySignature(sig);
                Console.WriteLine(valid);
            }


            var priKeyContent = Convert.FromBase64String(@"MIICdwIBADANBgkqhkiG9w0BAQEFAASCAmEwggJdAgEAAoGBAKko+TE1xJaRoRm/UrNIoWDejYYPdDurNZe28pXFrRWTyFFPx9ABOx6XduSkbxbmCnh3bqE5ytlpKmZjxVCV74wT9RYMzkmERlBhI55aK6deKNnRS8OluiNyJybT3pxCzKe+daj3P4LgjQLe3d0Jn+bWUT2L0iA/TCdDt6ZLZywVAgMBAAECgYBY7Y5bPW30zehIVdlPIQ6dk0IJSRSMzcvlzyqma/47Cq7TeEKN6ie/RFcfigZQnmzAueCx52TpeKzumOLBI6GDQgnSBx8RVPjx/p8XNGUxw4UQiUG+EW7iNqLEKKKtnwfjYNqFcNAsn1b8CCMORozIwYLhn5ihkBiz10ITQURQoQJBAOBxhfpDYFNhlYHuP3qn7VqKZCzzbAfT6OOuc8GoEJvHeBnKczFrUzMmbiberVn5g/l7rJ9rfakY32f5KYP73XkCQQDA8Z03nqKhqaxdEG2JgStcGgMT1htkWeSmUNoKaY+SBor3BVLrd30VZGp51zzAcTY9pdUG7PQXystxn6Htskh9AkEAhnk+Dp4DvrF/BGQcwH6QpWi5cH1AQshihtflHyh1GwC+IqW7suZc6Q6jfMJ6Fqh6vCWvXaznk0MFx6PvjdZ/8QJBAIZTL7sbK+oUsDUSTNAgJ0m1qlLTCrrwgmjvfP0mxJdLCtAy2qmnxGNyR1aP7HGl37dHjmmF6eHug3iVRCyxpBkCQATsf3oAPErQ0AT2wB0Uof6uv0x+/6r1q0cuR/R3Htd3Sz9GIx+4v79p41DBcSMyr2fubtXp6WiHSlvseiKigWU=");
            var pubKeyContent = Convert.FromBase64String(@"MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCpKPkxNcSWkaEZv1KzSKFg3o2GD3Q7qzWXtvKVxa0Vk8hRT8fQATsel3bkpG8W5gp4d26hOcrZaSpmY8VQle+ME/UWDM5JhEZQYSOeWiunXijZ0UvDpbojcicm096cQsynvnWo9z+C4I0C3t3dCZ/m1lE9i9IgP0wnQ7emS2csFQIDAQAB");
            //var priKeyContent = Convert.FromBase64String(@"MIICWwIBAAKBgQDsQIuN+M/x9grPYectc3Ye5rboKWT12w9KcSWLgdRlUg9IGe6oDYj/CEGh0rJi4pijlRRY06Ri8MLkn76T/sYYXHuYSzVgAaolhD1Fpv0NwQBBGwWhogz/FF1qhW2dMQiMlBlQjp6jblzbJwnPAg9mESvywj8h03KNG3VnPAhCMwIDAQABAoGAfJsWgA0JcG77CKJ0ke5iEK5TLmQW0e12RSckTE5vvfjoAnla/NwWs5yhMT61w54ML8tDbg5Cl8SwpnDyzZAE3nTX86+oqRUDdJV96YOR2gti6lcfiZYd6qqPgnbZ4KQmcE+LRMpWhnL9zoAdg4kSorHP/nl8UfaRZQqhILg6G4ECQQD/8EOpEq/+M011yLlXMOO496/ckCrfm08DAw9vl0K5FVBMD9Fsd31/7pZh7Um7dqCabaNsYYorbIsffMQXB/6hAkEA7E8SClZJtTE+hUK2Vn9SNTQlofuztui2kRt0lt0+rjqgh+GH3FkPQeVVTQ7dxkq2aUntkEtELso+RRkYgbg0UwJACFF0wX/7/FUKhXN6opzSKebS7mY5Hn9buAtXaxcNchqBO5egBNh1Wb0VYiVmKhOW8K3zi8g3x2WFuAZEEUOPQQJAGTyMiawTbRVYPvUT8gLg7aunBTiTRcpujOqotd/k7Mh4EmrkjoS4W2o5hOQ8jQu3lWD+zPUsz+5rXgfDFT9t3wJACdFWlzj1TC64f9bNo7I2n1S+xWn1Rm/z0e+fVq2n7sbtVtaYQY1HSXPOuDgCKMPcx5Fp1EuhzDvnxIaPdUXzyQ==");

            var asn1Seq = Asn1Sequence.GetInstance(priKeyContent);
            if(asn1Seq is DerSequence)
            {
                asn1Seq = (Asn1Sequence)asn1Seq;
            }
            var ppriv = PrivateKeyFactory.CreateKey(PrivateKeyInfo.GetInstance(asn1Seq)) as AsymmetricKeyParameter;
            var ppubl = PublicKeyFactory.CreateKey((pubKeyContent));

            RsaDigestSigner signer1 = new RsaDigestSigner(new Sha1Digest());
            signer1.Init(true, ppriv);
            signer1.BlockUpdate(msg, 0, msg.Length);
            byte[] sig1 = signer1.GenerateSignature();

            Console.WriteLine(Convert.ToBase64String(sig1));

            signer1.Init(false, ppubl);
            signer1.BlockUpdate(msg, 0, msg.Length);
            bool valid2 = signer1.VerifySignature(sig1);
            Console.WriteLine(valid2);
        }

        private static string pubKeyFile = "a.pub";
        private static string priKeyFile = "a.pri";
        private static string ecyFile = "encryptedBytes";

        private static void RsaKeyGeneratorTest()
        {
            //RSA密钥对的构造器
            RsaKeyPairGenerator keyGenerator = new RsaKeyPairGenerator();
            //RSA密钥构造器的参数
            RsaKeyGenerationParameters param = new RsaKeyGenerationParameters(
                Org.BouncyCastle.Math.BigInteger.ValueOf(3),
                new Org.BouncyCastle.Security.SecureRandom(),
                1024,   //密钥长度
                25);
            //用参数初始化密钥构造器
            keyGenerator.Init(param);
            //产生密钥对
            AsymmetricCipherKeyPair keyPair = keyGenerator.GenerateKeyPair();
            //获取公钥和私钥
            AsymmetricKeyParameter publicKey = keyPair.Public;
            AsymmetricKeyParameter privateKey = keyPair.Private;

            if (((RsaKeyParameters)publicKey).Modulus.BitLength < 1024)
            {
                Console.WriteLine("failed key generation (1024) length test");
            }
            savetheKey(publicKey, privateKey);



            //一个测试……………………
            //输入，十六进制的字符串，解码为byte[]
            //string input = "4e6f77206973207468652074696d6520666f7220616c6c20676f6f64206d656e";
            //byte[] testData = Org.BouncyCastle.Utilities.Encoders.Hex.Decode(input);           
            string input = "popozh RSA test";
            byte[] testData = Encoding.UTF8.GetBytes(input);
            //非对称加密算法，加解密用
            IAsymmetricBlockCipher engine = new RsaEngine();
            //公钥加密 
            //从保存在本地的磁盘文件中读取公钥
            Asn1Object aobject = Asn1Object.FromStream(new FileStream(pubKeyFile, FileMode.Open, FileAccess.Read));  //a.puk??
            SubjectPublicKeyInfo pubInfo = SubjectPublicKeyInfo.GetInstance(aobject);
            AsymmetricKeyParameter testpublicKey = (RsaKeyParameters)PublicKeyFactory.CreateKey(pubInfo);
            FileStream fs;
            engine.Init(true, testpublicKey);
            try
            {
                //Console.WriteLine("加密前:" + Convert.ToBase64String(testData) + Environment.NewLine);
                testData = engine.ProcessBlock(testData, 0, testData.Length);
                Console.WriteLine("加密完成！" + Environment.NewLine);
                fs = new FileStream(ecyFile, FileMode.Create, FileAccess.Write);
                fs.Write(testData, 0, testData.Length);
                fs.Close();
                Console.WriteLine("保存密文成功" + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine("failed - exception " + Environment.NewLine + ex.ToString());
            }
            //私钥解密
            //获取加密的私钥，进行解密，获得私钥
            fs = new FileStream(ecyFile, FileMode.Open, FileAccess.Read);
            byte[] anothertestdata = new byte[1024];
            fs.Read(anothertestdata, 0, anothertestdata.Length);
            fs.Close();
            Asn1Object aobj = Asn1Object.FromStream(new FileStream(priKeyFile, FileMode.Open, FileAccess.Read));   //a.pvk??
            EncryptedPrivateKeyInfo enpri = EncryptedPrivateKeyInfo.GetInstance(aobj);
            char[] password = "123456".ToCharArray();
            PrivateKeyInfo priKey = PrivateKeyInfoFactory.CreatePrivateKeyInfo(password, enpri);    //解密
            AsymmetricKeyParameter anotherprivateKey = PrivateKeyFactory.CreateKey(priKey);    //私钥
            engine.Init(false, anotherprivateKey);
            try
            {
                anothertestdata = engine.ProcessBlock(anothertestdata, 0, testData.Length);
                Console.WriteLine("解密后密文为：" + Encoding.UTF8.GetString(anothertestdata) + Environment.NewLine);
            }
            catch (Exception e)
            {
                Console.WriteLine("failed - exception " + e.ToString());
            }

            Console.Read();

        }
        private static void savetheKey(AsymmetricKeyParameter publicKey, AsymmetricKeyParameter privateKey)
        {
            //保存公钥到文件
            SubjectPublicKeyInfo publicKeyInfo = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(publicKey);
            Asn1Object aobject = publicKeyInfo.ToAsn1Object();
            byte[] pubInfoByte = aobject.GetEncoded();
            FileStream fs = new FileStream(pubKeyFile, FileMode.Create, FileAccess.Write);
            fs.Write(pubInfoByte, 0, pubInfoByte.Length);
            fs.Close();
            //保存私钥到文件
            /*
            PrivateKeyInfo privateKeyInfo = PrivateKeyInfoFactory.CreatePrivateKeyInfo(privateKey);
            aobject = privateKeyInfo.ToAsn1Object();
            byte[] priInfoByte = aobject.GetEncoded();
            fs = new FileStream(@"E:/Desktop/a.pri", FileMode.Create, FileAccess.Write);
            fs.Write(priInfoByte, 0, priInfoByte.Length);
            fs.Close();
            */
            string alg = "1.2.840.113549.1.12.1.3"; // 3 key triple DES with SHA-1
            byte[] salt = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            int count = 1000;
            char[] password = "123456".ToCharArray();
            EncryptedPrivateKeyInfo enPrivateKeyInfo = EncryptedPrivateKeyInfoFactory.CreateEncryptedPrivateKeyInfo(
                alg,
                password,
                salt,
                count,
                privateKey);
            byte[] priInfoByte = enPrivateKeyInfo.ToAsn1Object().GetEncoded();
            fs = new FileStream(priKeyFile, FileMode.Create, FileAccess.Write);
            fs.Write(priInfoByte, 0, priInfoByte.Length);
            fs.Close();
            //还原
            //PrivateKeyInfo priInfo = PrivateKeyInfoFactory.CreatePrivateKeyInfo(password, enPrivateKeyInfo);
            //AsymmetricKeyParameter privateKey = PrivateKeyFactory.CreateKey(priInfoByte);
        }
    }
}
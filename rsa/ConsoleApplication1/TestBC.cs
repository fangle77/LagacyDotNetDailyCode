using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;    // X509Certificate2
using System.IO;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Crypto.Engines;  //IAsymmetricBlockCipher engine = new RsaEngine();
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Asn1.X509; //X509Name
using Org.BouncyCastle.X509;
using Org.BouncyCastle.Utilities.Collections;   //X509V3CertificateGenerator
using Org.BouncyCastle.Asn1.Pkcs;   //PrivateKeyInfo
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Asn1;

namespace ConsoleApplication1
{
    class TestBC
    {
        public static void Main1(string[] args)
        {
            //公钥和密钥的生成，并加密解密测试
            RsaKeyGeneratorTest();    //done!!!!!

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
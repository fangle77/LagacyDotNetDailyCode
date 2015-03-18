using System;
using JavaScience;

namespace ConsoleApplication1
{
    class TestPCK8
    {
        public static void DecodeEncryptedPrivateKeyInfo()
        {
            string privateKey = @"MIICdwIBADANBgkqhkiG9w0BAQEFAASCAmEwggJdAgEAAoGBAKko+TE1xJaRoRm/UrNIoWDejYYPdDurNZe28pXFrRWTyFFPx9ABOx6XduSkbxbmCnh3bqE5ytlpKmZjxVCV74wT9RYMzkmERlBhI55aK6deKNnRS8OluiNyJybT3pxCzKe+daj3P4LgjQLe3d0Jn+bWUT2L0iA/TCdDt6ZLZywVAgMBAAECgYBY7Y5bPW30zehIVdlPIQ6dk0IJSRSMzcvlzyqma/47Cq7TeEKN6ie/RFcfigZQnmzAueCx52TpeKzumOLBI6GDQgnSBx8RVPjx/p8XNGUxw4UQiUG+EW7iNqLEKKKtnwfjYNqFcNAsn1b8CCMORozIwYLhn5ihkBiz10ITQURQoQJBAOBxhfpDYFNhlYHuP3qn7VqKZCzzbAfT6OOuc8GoEJvHeBnKczFrUzMmbiberVn5g/l7rJ9rfakY32f5KYP73XkCQQDA8Z03nqKhqaxdEG2JgStcGgMT1htkWeSmUNoKaY+SBor3BVLrd30VZGp51zzAcTY9pdUG7PQXystxn6Htskh9AkEAhnk+Dp4DvrF/BGQcwH6QpWi5cH1AQshihtflHyh1GwC+IqW7suZc6Q6jfMJ6Fqh6vCWvXaznk0MFx6PvjdZ/8QJBAIZTL7sbK+oUsDUSTNAgJ0m1qlLTCrrwgmjvfP0mxJdLCtAy2qmnxGNyR1aP7HGl37dHjmmF6eHug3iVRCyxpBkCQATsf3oAPErQ0AT2wB0Uof6uv0x+/6r1q0cuR/R3Htd3Sz9GIx+4v79p41DBcSMyr2fubtXp6WiHSlvseiKigWU=";
            var cspBytes = Convert.FromBase64String(privateKey);
            opensslkey.DecodeEncryptedPrivateKeyInfo(cspBytes);
        }
    }
}

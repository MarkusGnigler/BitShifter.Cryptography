using System;
using System.Security.Cryptography;

namespace PixelDance.Cryptography.Common
{
    public static class CipherHelper
    {
        public static string GenerateNewKey()
        {
            using var aes = Aes.Create();

            aes.GenerateKey();

            return Convert.ToBase64String(aes.Key);
        }
    }
}

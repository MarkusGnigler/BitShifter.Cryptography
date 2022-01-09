using System.Linq;
using Microsoft.Extensions.Configuration;
using PixelDance.Cryptography.SymmetricCiphers;

namespace PixelDance.Cryptography.Extensions
{
    public static class ConfigurationRootExtensions
    {
        public static IConfigurationRoot Decrypt(this IConfigurationRoot root, string keyPath, string cipherPrefix)
        {
            var secret = root[keyPath];

            var cipher = new SymmetricCipher(secret);

            DecryptInChildren(cipher, root, cipherPrefix);

            return root;
        }

        private static void DecryptInChildren(SymmetricCipher cipher, IConfiguration parent, string cipherPrefix)
        {
            // Apply decryption
            parent.GetChildren()
                .Where(x => StartsWithChiperPrefix(x, cipherPrefix))
                .ToList()
                .ForEach(child => OverrideConfigurationChild(parent, child, cipher, cipherPrefix));

            // Recursion -> Apply child decryption
            parent.GetChildren()
                .Where(x => !StartsWithChiperPrefix(x, cipherPrefix))
                .ToList()
                .ForEach(child => DecryptInChildren(cipher, child, cipherPrefix));
        }

        private static bool StartsWithChiperPrefix(
            IConfigurationSection child, string cipherPrefix)
                => child.Value?.StartsWith(cipherPrefix) == true;

        private static void OverrideConfigurationChild(
            IConfiguration parent, IConfigurationSection child,
            SymmetricCipher cipher, string cipherPrefix)
        {
            var cipherText = child.Value.Substring(cipherPrefix.Length);
            parent[child.Key] = cipher.Decrypt(cipherText);
        }
    }
}
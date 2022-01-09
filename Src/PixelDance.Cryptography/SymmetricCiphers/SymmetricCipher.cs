using System;
using System.Linq;
using System.Runtime.CompilerServices;
using PixelDance.Cryptography.Common;

[assembly: InternalsVisibleTo("PixelDance.Cryptography.Tests")]
[assembly: InternalsVisibleTo("PixelDance.Cryptography.Extensions")]
namespace PixelDance.Cryptography.SymmetricCiphers
{
    internal class SymmetricCipher : AbstractCipher, ISymmetricCipher
    {
        public SymmetricCipher(string secret)
            : base(secret)
        { }

        public string Decrypt(string value)
            => OperateOnAes(aes =>
            {
                int ivLength = aes.BlockSize / 8;

                byte[] ivAndCipherText = Convert.FromBase64String(value);

                aes.IV = ivAndCipherText
                    .Take(ivLength)
                    .ToArray();

                byte[] cipherText = ivAndCipherText
                    .Skip(ivLength)
                    .ToArray();

                using var cipher = aes.CreateDecryptor();

                byte[] text = GetChiperText(cipher, cipherText);

                return _encoder.GetString(text);
            });

        public string Encrypt(string value)
            => OperateOnAes(aes =>
            {
                aes.GenerateIV();

                byte[] text = _encoder.GetBytes(value);

                using var cipher = aes.CreateEncryptor();

                byte[] cipherText = GetChiperText(cipher, text);

                return Convert
                    .ToBase64String(
                        aes.IV.Concat(cipherText).ToArray());
            });
    }
}
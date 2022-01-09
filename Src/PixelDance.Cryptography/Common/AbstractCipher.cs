using System;
using System.Text;
using System.Security.Cryptography;

namespace PixelDance.Cryptography.Common
{
    public abstract class AbstractCipher
    {
        protected readonly byte[] _secret;
        protected readonly Encoding _encoder = Encoding.UTF8;

        protected AbstractCipher(string secret)
        {
            if (string.IsNullOrWhiteSpace(secret))
                throw new NullReferenceException("No secret was given!");

            if (!secret.IsBase64String())
                throw new ArgumentException("No secret is no valid base64!");

            _secret = Convert.FromBase64String(secret);
        }

        protected byte[] GetChiperText(ICryptoTransform cipher, byte[] inputBuffer)
            => cipher.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);

        protected T OperateOnAes<T>(Func<Aes, T> operation)
        {
            using var aes = Aes.Create();

            aes.Key = _secret;

            return operation(aes);
        }
    }
}
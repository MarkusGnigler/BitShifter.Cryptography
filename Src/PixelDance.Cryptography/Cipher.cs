#pragma warning disable CS8618
using PixelDance.Cryptography.SymmetricCiphers;

namespace PixelDance.Cryptography
{
    internal class Cipher : ICipher
    {
        public ISymmetricCipher SymetricCipher { get; }
        //public IAsymmetricCipher AsymetricCipher { get; }

        public Cipher(string secret)
        {
            SymetricCipher = new SymmetricCipher(secret);
        }
    }
}

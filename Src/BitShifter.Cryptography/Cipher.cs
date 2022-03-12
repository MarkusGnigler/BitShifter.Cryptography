#pragma warning disable CS8618
using BitShifter.Cryptography.SymmetricCiphers;

namespace BitShifter.Cryptography
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

using PixelDance.Cryptography.SymmetricCiphers;

namespace PixelDance.Cryptography
{
    public interface ICipher
    {
        public ISymmetricCipher SymetricCipher { get; }
        //public IAsymmetricCipher AsymetricCipher { get; }
    }
}
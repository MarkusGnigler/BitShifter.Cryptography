using BitShifter.Cryptography.SymmetricCiphers;

namespace BitShifter.Cryptography
{
    public interface ICipher
    {
        public ISymmetricCipher SymetricCipher { get; }
        //public IAsymmetricCipher AsymetricCipher { get; }
    }
}
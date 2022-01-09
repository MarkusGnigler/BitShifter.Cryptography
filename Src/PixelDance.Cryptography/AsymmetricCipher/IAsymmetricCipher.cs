namespace PixelDance.Cryptography.SymmetricCiphers
{
    public interface IAsymmetricCipher
    {
        string Decrypt(string value);
        string Encrypt(string value);
    }
}

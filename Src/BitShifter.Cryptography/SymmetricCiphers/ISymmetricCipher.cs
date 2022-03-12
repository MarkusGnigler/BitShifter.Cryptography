namespace BitShifter.Cryptography.SymmetricCiphers
{
    public interface ISymmetricCipher
    {
        string Decrypt(string value);
        string Encrypt(string value);
    }
}

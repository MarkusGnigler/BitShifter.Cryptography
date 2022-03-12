using System;

using Xunit;
using FluentAssertions;
using BitShifter.Cryptography.Common;
using BitShifter.Cryptography.SymmetricCiphers;

namespace BitShifter.Cryptography.Tests.Ciphers
{
    public class SymetricCipherTest
    {
        private readonly static string SECRET = "l8cpD27QcWDXjAg8ut+qH0IkWv/p38DrAst4Ee83jMg=";

        [Fact]
        public void TextEncoder_ThrowsException_IfSecretIsEmptyOrWhitespace()
        {
            // Arrange
            string connectionString = " ";

            // Act
            var sutAction = () => new SymmetricCipher(connectionString);

            // Assert
            sutAction.Should()
                .Throw<NullReferenceException>()
                .WithMessage("No secret was given!");
        }

        [Fact]
        public void TextEncoder_ThrowsException_IfSecretIsNotBase64()
        {
            // Arrange
            //string connectionString = "Das Ist kein gültiger Base64 String==";
            string connectionString = "Das+Ist+kein+gült/iger+Base64+Stringsdfsdfsdfsdfsdfsdfsdfsdf==";

            // Act
            var sutAction = () => new SymmetricCipher(connectionString);

            // Assert
            sutAction.Should()
                .Throw<ArgumentException>()
                .WithMessage("No secret is no valid base64!");
        }

        [Fact]
        public void TextEncoder_EncryptTo_Base64()
        {
            // Arrange
            string connectionString = "Data Source=localhost;Initial Catalog=database_name;Integrated Security=False;User Id=sa;Password=<SECRET+*#~|!§$%&/()=?ß-_.SECRET>;MultipleActiveResultSets=True;App=ApplicationName";
            string expectedConnectionString = "oB5+lImBWurf4sNUGxEktjubGgHEsdS+6bs3pOSQayEplrsk8a9LCbxFmTePx9vBxVf33kl4nX5RZBEG2s1W4hmZNq2nShNvYRM604wHeqvbPKAj/wQghJBhqU5kAYpMxoHIA50ijmoKVywpztSFIM6xkFQv0FSGiHtmdEgXmPGXNpiLaVde2DDsT0GXIyz1MkUGBz9IL89PQJgCRTmMmPgg3DmxMEIaeta2lQS8WtmXKnfEwmSIxX7X3TM6UYJTdRyXJO3N1EREPNlIrkki3Q==";

            var sut = new SymmetricCipher(SECRET);

            // Act
            string encryptedString = sut.Encrypt(connectionString);

            // Assert
            //encryptedString.Should()
            //    .Be(expectedConnectionString);

            encryptedString.Should()
                .NotBeNullOrWhiteSpace();
            encryptedString.IsBase64String().Should()
                .BeTrue();
        }

        [Fact]
        public void TextEncoder_DecryptToNormalString()
        {
            // Arrange
            string connectionString = "oB5+lImBWurf4sNUGxEktjubGgHEsdS+6bs3pOSQayEplrsk8a9LCbxFmTePx9vBxVf33kl4nX5RZBEG2s1W4hmZNq2nShNvYRM604wHeqvbPKAj/wQghJBhqU5kAYpMxoHIA50ijmoKVywpztSFIM6xkFQv0FSGiHtmdEgXmPGXNpiLaVde2DDsT0GXIyz1MkUGBz9IL89PQJgCRTmMmPgg3DmxMEIaeta2lQS8WtmXKnfEwmSIxX7X3TM6UYJTdRyXJO3N1EREPNlIrkki3Q==";
            string expectedConnectionString = "Data Source=localhost;Initial Catalog=database_name;Integrated Security=False;User Id=sa;Password=<SECRET+*#~|!§$%&/()=?ß-_.SECRET>;MultipleActiveResultSets=True;App=ApplicationName";

            var sut = new SymmetricCipher(SECRET);

            // Act
            string decryptedString = sut.Decrypt(connectionString);

            // Assert
            decryptedString.Should()
                .Be(expectedConnectionString);
        }

        [Fact]
        public void Generate_NewKey_As_Base64()
        {
            string secret = CipherHelper.GenerateNewKey();

            secret.IsBase64String().Should()
                .BeTrue();
        }
    }
}

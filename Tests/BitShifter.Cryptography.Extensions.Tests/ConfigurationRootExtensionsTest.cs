using System.IO;

using Xunit;
using FluentAssertions;

using Microsoft.Extensions.Configuration;

namespace BitShifter.Cryptography.Extensions.Tests
{
    public class ConfigurationRootExtensionsTests
    {
        [Theory]
        [ClassData(typeof(AppsettingsValues))]
        public void TestIf_ConfigurationValue_WasDecrypted(
            string configKey, string expectedValue)
        {
            // Arrange
            IConfiguration sut = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build()

                .Decrypt(keyPath: "CipherKey", cipherPrefix: "CipherText:");

            // Act
            var connectionString = sut[configKey];

            // Assert
            connectionString.Should()
                .Be(expectedValue);
        }

    }
}
using System;

using Xunit;
using FluentAssertions;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

namespace BitShifter.Cryptography.Extensions.Tests
{
    public class HostBuilderExtensionsTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void TestIf_Empty_KeyPath_WThrowsException(string keyPath)
        {
            // Act
            Action sutAction = () => Host.CreateDefaultBuilder()
                .ConfigureChiperSecurity(() => (keyPath: keyPath, cipherPrefix: "CipherPrefix:"));

            // Assert
            sutAction.Should()
                .Throw<ArgumentException>()
                .WithMessage("SecretKey can't be empty.");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void TestIf_Empty_CipherPrefix_WThrowsException(string cipherPrefix)
        {
            // Act
            Action sutAction = () => Host.CreateDefaultBuilder()
                .ConfigureChiperSecurity(() => (keyPath: "KeyPath", cipherPrefix: cipherPrefix));

            // Assert
            sutAction.Should()
                .Throw<ArgumentException>()
                .WithMessage("Cipher text prefix can't be empty.");
        }

        //[Theory]
        //[ClassData(typeof(AppsettingsValues))]
        //public void TestIf_ConfigurationValue_WasDecrypted_With_ConfigureChiperSecurity_Registration(
        //    string configKey, string expectedValue)
        //{
        //    // Arrange
        //    var host = Host.CreateDefaultBuilder()
        //        .ConfigureChiperSecurity(() => (keyPath: "CipherKey", cipherPrefix: "CipherText:"))
        //        .Build();

        //    IConfiguration? sut = host.Services.GetService(typeof(IConfiguration)) as IConfiguration;

        //    // Act
        //    var connectionString = sut?[configKey];

        //    // Assert
        //    connectionString.Should()
        //        .Be(expectedValue);
        //}

        //[Theory]
        //[ClassData(typeof(AppsettingsValues))]
        //public void TestIf_ConfigurationValue_WasDecrypted_With_ConfigureChiperSecurity_Registration_(
        //    string configKey, string expectedValue)
        //{
        //    // Arrange
        //    var host = Host.CreateDefaultBuilder()
        //        .ConfigureChiperSecurity(() => (keyPath: "CipherKey", cipherPrefix: "CipherText:"))
        //        .ConfigureAppConfiguration(builder =>
        //        {
        //            var config = builder
        //                .AddEnvironmentVariables()
        //                .Build();

        //            // Act
        //            var connectionString = config[configKey];

        //            // Assert
        //            connectionString.Should()
        //                .Be(expectedValue);
        //        })
        //        .Build();
        //}
    }
}

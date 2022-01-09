using System;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

namespace PixelDance.Cryptography.Extensions
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder ConfigureChiperSecurity(this IHostBuilder builder)
        {
            builder.ConfigureChiperSecurity(() => (keyPath: "CipherKey", cipherPrefix: "CipherText:"));

            return builder;
        }

        public static IHostBuilder ConfigureChiperSecurity(
            this IHostBuilder builder,
            Func<(string keyPath, string cipherPrefix)> configure)
        {
            var (keyPath, cipherPrefix) = configure();

            if (string.IsNullOrWhiteSpace(keyPath))
                throw new ArgumentException("SecretKey can't be empty.");

            if (string.IsNullOrWhiteSpace(cipherPrefix))
                throw new ArgumentException("Cipher text prefix can't be empty.");

            builder
                .ConfigureServices((context, services) 
                    => services.AddCryptography(context.Configuration, keyPath))
                .ConfigureAppConfiguration(builder =>
                {
                    var config = builder
                        .AddEnvironmentVariables()
                        .Build()
                        .Decrypt(keyPath, cipherPrefix);
                });

            return builder;
        }

    }
}

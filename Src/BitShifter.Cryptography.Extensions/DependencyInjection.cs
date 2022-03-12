using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BitShifter.Cryptography.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCryptography(
            this IServiceCollection services,
            IConfiguration configuration,
            string secretKey)
        {
            var secret = configuration[secretKey];
            if (string.IsNullOrWhiteSpace(secret))
                throw new ArgumentException("No secret provided in 'appsettings.json' or env variables.");

            services.AddSingleton<ICipher, Cipher>(sp => new Cipher(secret));

            return services;
        }
    }
}

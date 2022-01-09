using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PixelDance.Cryptography.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host
    //.ConfigureAppConfiguration(builder =>
    //{
    //    var config = builder
    //        .AddEnvironmentVariables()
    //        .Build()
    //        .Decrypt(keyPath: "CipherKey", cipherPrefix: "CipherText:");
    //})
    //.ConfigureChiperSecurity()
    .ConfigureChiperSecurity(() => (keyPath: "CipherKey", cipherPrefix: "CipherText:"))
    .ConfigureServices(collection =>
    {
        var sp = collection.BuildServiceProvider();
        var config = sp.GetRequiredService<IConfiguration>();
        var Logger = sp.GetRequiredService<ILogger<Programm>>();

        Logger.LogInformation($"Decrypted DbConnectionString is: {config["DbConnectionString"]}");
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "EncryptionTester", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EncryptionTester v1"));
}

app.UseHttpsRedirection();

app.Run();

internal class Programm { }
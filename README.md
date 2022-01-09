# Secure OnPremise applications through appsettings.json

Secure your on prem asp.core application wether it's console based or web hosted.

[Inspired by gaevoy](https://github.com/gaevoy/Gaev.Blog.Examples/tree/master/Gaev.Blog.SecuredAppSettingsJson)

## Setup Cryptography basics

1. Add ChiperKey (Your Secret) to an env variable<br>
   Generate a key with `CipherHelper.GenerateNewKey()` and save it to a env variable.

   ```cmd
   $ set CipherKey=l8cpD27QcWDXjAg8ut+qH0IkWv/p38DrAst4Ee83jMg=
   ```

2. Add a encrypted text to appsetting.json<br>
   Prefix every encrypted text with a prefix e.g. **CipherText:**

   ```json
   {
     "DbConnectionString": "CipherText:/7smRN77Eo4hQ5yiPHTAu7cC0kO6zwYcVP77FGWtJkbTcB5gUHiPceO/rmeK9nY5mVR/jbGMUeF08zuiyF5sIqOnnixiFKrONDjJVHjFI+AVeuuqmhi2aR9s/zA3SHBJ5Egc7FAX3CDJt0bdKX9h75QYnr166vGSh0BsbHMvkRAhWwb36dJeN7qZAyWttCmHAZcGAo7UVJ9puPoexMw9Mq276rUQsU89BpNk4cXSJv8="
   }
   ```

## Add Cryptography to application

Within a console application

```cs
var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build()
    .Decrypt(keyPath: "CipherKey", cipherPrefix: "CipherText:");

Console.WriteLine($"DbConnectionString: {config["DbConnectionString"]}");
```

Within a webhosted application

```cs
public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        })
        .ConfigureAppConfiguration(builder => {
            var config = builder
                .AddEnvironmentVariables()
                .Build()
                .Decrypt(keyPath: "CipherKey", cipherPrefix: "CipherText:");

            Console.WriteLine($"DbConnectionString: {config["DbConnectionString"]}");
        });
```

Within net6.0 webhosted application

> With standard cipher value (keyPath: "CipherKey", cipherPrefix: "CipherText:")

```cs
var builder = WebApplication.CreateBuilder(args);

builder.Host
    .ConfigureChiperSecurity()
    .ConfigureServices(collection =>
    {
        var sp = collection.BuildServiceProvider();
        var config = sp.GetRequiredService<IConfiguration>();
        var Logger = sp.GetRequiredService<ILogger<Programm>>();

        Logger.LogInformation($"Decrypted DbConnectionString is: {config["DbConnectionString"]}");
    });
```

> With configurable cipher values

```cs
var builder = WebApplication.CreateBuilder(args);

builder.Host
    .ConfigureChiperSecurity(() => (keyPath: "CipherKey", cipherPrefix: "CipherText:"))
    .ConfigureServices(collection =>
    {
        var sp = collection.BuildServiceProvider();
        var config = sp.GetRequiredService<IConfiguration>();
        var Logger = sp.GetRequiredService<ILogger<Programm>>();

        Logger.LogInformation($"Decrypted DbConnectionString is: {config["DbConnectionString"]}");
    });
```

> With custom configuration

```cs
var builder = WebApplication.CreateBuilder(args);

builder.Host
    .ConfigureAppConfiguration(builder =>
    {
       var config = builder
           .AddEnvironmentVariables()
           .Build()
           .Decrypt(keyPath: "CipherKey", cipherPrefix: "CipherText:");
    })
    .ConfigureServices(collection =>
    {
        var sp = collection.BuildServiceProvider();
        var config = sp.GetRequiredService<IConfiguration>();
        var Logger = sp.GetRequiredService<ILogger<Programm>>();

        Logger.LogInformation($"Decrypted DbConnectionString is: {config["DbConnectionString"]}");
    });
```

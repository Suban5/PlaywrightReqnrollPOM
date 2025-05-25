using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace PlaywrightReqnrollFramework.Config;

public class ConfigReader
{
    public static TestSettings LoadSettings()
    {
        // Default to "CI" if not set
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "CI";

        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile($"appsettings.{environment}.json", optional: true)
            .Build();
        return config.Get<TestSettings>(); 
    }

}

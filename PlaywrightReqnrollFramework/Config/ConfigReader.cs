using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace PlaywrightReqnrollFramework.Config;

public class ConfigReader
{
    public static TestSettings LoadSettings()
    {
        //set the environment variable "ENVIRONMENT" to specify the configuration file to load
        //on Mac/Linux, you can set it in the terminal like this:
        // export ENVIRONMENT=Development
        // on Windows, you can set it in the command prompt like this:
        // set ENVIRONMENT=Development
        

        // Default to "Development" if not set (CI environment should explicitly set ENVIRONMENT=ci)
        var environment = Environment.GetEnvironmentVariable("ENVIRONMENT") ?? "Development";

        var basePath = Directory.GetCurrentDirectory();
        var baseConfigPath = Path.Combine(basePath, "appsettings.json");
        
        if (!File.Exists(baseConfigPath))
        {
            throw new FileNotFoundException($"Configuration file not found: {baseConfigPath}");
        }

        var config = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile($"{environment}.appsettings.json", optional: true)
            .Build();
        
        var settings = config.Get<TestSettings>();
        
        if (settings == null)
        {
            throw new InvalidOperationException("Failed to bind configuration to TestSettings");
        }
        
        return settings;
    }

}

using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace PlaywrightReqnrollFramework.Config;

public class ConfigReader
{
    public static T LoadConfig<T>(string sectionName) where T : new()
    {
        //set the environment variable "ENVIRONMENT" to specify the configuration file to load
        //on Mac/Linux, you can set it in the terminal like this:
        // export ENVIRONMENT=Development
        // on Windows, you can set it in the command prompt like this:
        // set ENVIRONMENT=Development
        

        // Default to "CI" if not set
        var environment = Environment.GetEnvironmentVariable("ENVIRONMENT") ?? "CI";

        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile($"{environment}.appsettings.json", optional: true)
            .Build();
        return config.GetSection(sectionName).Get<T>() ?? new T(); 
    }

}

using System;
using Microsoft.Extensions.Configuration;

namespace PlaywrightReqnrollFramework.Config;

public class ConfigReader
{
    public static TestSettings LoadSettings()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
        return config.Get<TestSettings>();  
    }

}

using System;
using System.Collections.Generic;

namespace PlaywrightReqnrollFramework.Config;

public class ApiTestSettings
{
    public string ApiKey { get; init; } = "reqres-free-v1";
    public string BaseUrl { get; init; } = "https://reqres.in/api";
    public Dictionary<string, string> DefaultHeaders { get; init; } = new()
    {
        ["Accept"] = "application/json"
    };
}

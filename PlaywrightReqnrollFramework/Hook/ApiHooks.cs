using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Playwright;
using PlaywrightReqnrollFramework.Config;
using Reqnroll;

namespace PlaywrightReqnrollFramework.UIHook;

[Binding]
public class ApiHooks(ScenarioContext scenarioContext)
{
    private readonly ScenarioContext _scenarioContext = scenarioContext;

    [BeforeScenario("API", Order = 1)]
    public async Task InitApiClient()
    {
        var apiSettings = ConfigReader.LoadConfig<ApiTestSettings>("ApiTestSettings");
        var playwright = await Playwright.CreateAsync();
        var apiRequest = await playwright.APIRequest.NewContextAsync(new()
        {
            BaseURL = apiSettings.BaseUrl,
            ExtraHTTPHeaders = new Dictionary<string, string> { ["x-api-key"] = apiSettings.ApiKey }
        });
         _scenarioContext.Set(apiRequest, "apiRequestContext");
        _scenarioContext.Set(apiSettings, "apiTestSettings");
    }

    [AfterScenario( "API")]
    public async Task DisposeApiClient()
    {
        if (_scenarioContext.TryGetValue<IAPIRequestContext>(nameof(IAPIRequestContext), out var apiRequest))
            await apiRequest.DisposeAsync();
    }

}

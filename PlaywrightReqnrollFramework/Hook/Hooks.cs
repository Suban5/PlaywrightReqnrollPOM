using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using PlaywrightReqnrollFramework.Config;
using PlaywrightReqnrollFramework.Driver;
using Reqnroll;
using static PlaywrightReqnrollFramework.Config.TestSettings;

namespace PlaywrightReqnrollFramework.Hook;

[Binding]
public class Hooks
{
    private static PlaywrightDriver _driver;

    private readonly ScenarioContext _scenarioContext;
    private IPage _page;

    public Hooks(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }


    [BeforeScenario("@web")]
    public async Task BeforeScenario()
    {
        TestSettings testSettings = new TestSettings
        {
            Headless = false, // Set to true for headless mode
            SlowMo = 500, // Slow down operations by 50ms
            BaseUrl = "http://example.com", // Set your base URL
            BrowserType = BrowserTypeEnum.Firefox // Choose your browser type
        };
        _driver = new PlaywrightDriver(testSettings);
        _page = await _driver.InitializeAsync();
        _scenarioContext.Set(_page, "currentPage");


    }

    [AfterScenario("@web")]
    public async Task AfterScenario()
    {
        await _driver.DisposeAsync();
    }
}

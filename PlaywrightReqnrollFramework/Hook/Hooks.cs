using System;
using System.Threading.Tasks;
using PlaywrightReqnrollFramework.Config;
using PlaywrightReqnrollFramework.Driver;
using Reqnroll;
using static PlaywrightReqnrollFramework.Config.TestSettings;

namespace PlaywrightReqnrollFramework.Hook;

[Binding]
public class Hooks(ScenarioContext scenarioContext)
{
    private static PlaywrightDriver _driver;

    private readonly ScenarioContext _scenarioContext = scenarioContext;

    //This method is executed before each scenario tagged with @web
    [BeforeScenario("@web")]
    public async Task BeforeScenario()
    {
        TestSettings testSettings = new TestSettings
        {
            Headless = false, // Set to true for headless mode
            SlowMo = 500,
            Timeout = 15000, // Slow down operations by ms
            BaseUrl = "https://www.saucedemo.com", // Set your base URL
            BrowserType = BrowserTypeEnum.Firefox // Choose your browser type
        };
        _driver = new PlaywrightDriver(testSettings);
        var page = await _driver.InitializeAsync();
        page.SetDefaultTimeout(testSettings.Timeout);
        _scenarioContext.Set(page, "currentPage");
        _scenarioContext.Set(testSettings, "testSettings");
    }

    [AfterScenario("@web")]
    public static async Task AfterScenario()
    {
        await _driver.DisposeAsync();
    }
}

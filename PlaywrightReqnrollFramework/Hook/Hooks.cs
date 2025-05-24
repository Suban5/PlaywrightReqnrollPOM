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
        var settings = ConfigReader.LoadSettings();
        TestSettings testSettings = new TestSettings
        {
            Headless = settings.Headless, // Set to true for headless mode
            SlowMo = settings.SlowMo, // Slow down operations by ms
            Timeout = settings.Timeout, // Slow down operations by ms
            BaseUrl = settings.BaseUrl, // Set your base URL
            BrowserType = settings.BrowserType // Choose your browser type
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

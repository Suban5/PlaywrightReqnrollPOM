using System;
using System.IO;
using System.Threading.Tasks;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
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
    public async Task IntializePlaywright()
    {
        // Load settings directly from configuration
        var testSettings = ConfigReader.LoadSettings();
        
        _driver = new PlaywrightDriver(testSettings);
        var page = await _driver.InitializeAsync();
        
        // Set default timeout (convert seconds to milliseconds for Playwright)
        page.SetDefaultTimeout(testSettings.Timeout * 1000);
        
        _scenarioContext.Set(page, "currentPage");
        _scenarioContext.Set(testSettings, "testSettings");
    }

    [AfterScenario("@web")]
    public static async Task AfterScenario()
    {
        await _driver.DisposeAsync();
    }

   
}

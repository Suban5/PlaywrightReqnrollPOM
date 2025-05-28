using System;
using System.IO;
using System.Threading.Tasks;
using PlaywrightReqnrollFramework.Config;
using PlaywrightReqnrollFramework.Driver;
using Reqnroll;

namespace PlaywrightReqnrollFramework.UIHook;

[Binding]
public class UIHooks(ScenarioContext scenarioContext)
{
    private static PlaywrightDriver _driver;

    private readonly ScenarioContext _scenarioContext = scenarioContext;

    private UITestSettings _uiSettings;
    

    //This method is executed before each scenario tagged with @web
    [BeforeScenario("@web")]
    public async Task IntializePlaywright()
    {
        _uiSettings = ConfigReader.LoadConfig<UITestSettings>("UITestSettings");
        _driver = new PlaywrightDriver(_uiSettings);
        var page = await _driver.InitializeAsync();
        page.SetDefaultTimeout(_uiSettings.Timeout);
        _scenarioContext.Set(page, "currentPage");
        _scenarioContext.Set(_uiSettings, "uiTestSettings");


    }

    [AfterScenario("@web")]
    public static async Task AfterScenario()
    {
        await _driver.DisposeAsync();
    }

   
}

using System;
using System.Threading.Tasks;
using PlaywrightReqnrollFramework.Config;
using PlaywrightReqnrollFramework.Driver;
using Reqnroll;

namespace PlaywrightReqnrollFramework.Hook;

[Binding]
public class Hooks(ScenarioContext scenarioContext)
{
    private PlaywrightDriver _driver;
    private readonly ScenarioContext _scenarioContext = scenarioContext;

    /// <summary>
    /// Initialize Playwright browser before each scenario tagged with @web
    /// </summary>
    [BeforeScenario("@web")]
    public async Task InitializePlaywright()
    {
        try
        {
            // Load settings directly from configuration
            var testSettings = ConfigReader.LoadSettings();

            _driver = new PlaywrightDriver(testSettings);
            var page = await _driver.InitializeAsync();

            // Set default timeout (convert seconds to milliseconds for Playwright)
            page.SetDefaultTimeout(testSettings.Timeout * 1000);

            _scenarioContext.Set(page, "currentPage");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to initialize Playwright: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// Cleanup browser resources after each scenario tagged with @web
    /// </summary>
    [AfterScenario("@web")]
    public async Task CleanupPlaywright()
    {
        try
        {
            if (_driver != null)
            {
                await _driver.DisposeAsync();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during cleanup: {ex.Message}");
            // Don't rethrow - cleanup errors shouldn't fail the test
        }
    }
}

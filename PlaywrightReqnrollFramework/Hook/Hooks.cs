using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Playwright;
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
            
            if (testSettings == null)
            {
                throw new InvalidOperationException("Failed to load test settings from configuration");
            }

            _driver = new PlaywrightDriver(testSettings);
            var page = await _driver.InitializeAsync();

            if (page == null)
            {
                throw new InvalidOperationException("Failed to initialize Playwright page");
            }

            // Store test settings for later use
            _scenarioContext.Set(testSettings, "testSettings");
            _scenarioContext.Set(page, "currentPage");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to initialize Playwright: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
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
            // Handle tracing and screenshots before cleanup
            if (_scenarioContext.ContainsKey("currentPage"))
            {
                var page = _scenarioContext.Get<IPage>("currentPage");
                if (page != null && !page.IsClosed && _scenarioContext.ContainsKey("testSettings"))
                {
                    var testSettings = _scenarioContext.Get<TestSettings>("testSettings");
                    var context = page.Context;
                    
                    // Stop and save trace based on configuration
                    if (testSettings.ContextSettings.RecordTrace == "on" || 
                        (testSettings.ContextSettings.RecordTrace == "retain-on-failure" && _scenarioContext.TestError != null))
                    {
                        try
                        {
                            var tracePath = Path.Combine("traces", $"{_scenarioContext.ScenarioInfo.Title}_{DateTime.Now:yyyyMMdd_HHmmss}.zip");
                            Directory.CreateDirectory("traces");
                            await context.Tracing.StopAsync(new() { Path = tracePath });
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Failed to save trace: {ex.Message}");
                        }
                    }
                    else if (testSettings.ContextSettings.RecordTrace != "off")
                    {
                        try
                        {
                            await context.Tracing.StopAsync();
                        }
                        catch { /* Ignore */ }
                    }
                    
                    // Take screenshot on failure
                    if (_scenarioContext.TestError != null)
                    {
                        try
                        {
                            var screenshot = await page.ScreenshotAsync(new() { FullPage = true });
                            var fileName = $"failure_{_scenarioContext.ScenarioInfo.Title}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
                            Directory.CreateDirectory("Screenshots");
                            await File.WriteAllBytesAsync(Path.Combine("Screenshots", fileName), screenshot);
                        }
                        catch
                        {
                            // Ignore screenshot errors during cleanup
                        }
                    }
                }
            }

            // Clean up driver resources
            if (_driver != null)
            {
                await _driver.DisposeAsync();
            }

            // Remove page from context
            if (_scenarioContext.ContainsKey("currentPage"))
            {
                _scenarioContext.Remove("currentPage");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during cleanup: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
            // Don't rethrow - cleanup errors shouldn't fail the test
        }
    }
}

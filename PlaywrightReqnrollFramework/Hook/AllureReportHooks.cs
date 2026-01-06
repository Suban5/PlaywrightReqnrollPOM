using System;
using System.Threading.Tasks;
using Allure.Net.Commons;
using Microsoft.Playwright;
using Reqnroll;

namespace PlaywrightReqnrollFramework.Hook;

/// <summary>
/// Allure reporting hooks for Reqnroll scenarios
/// Note: Allure.Reqnroll handles test lifecycle automatically
/// This hook only adds screenshots on failure
/// </summary>
[Binding]
public class AllureReportHooks(ScenarioContext scenarioContext)
{
    private readonly ScenarioContext _scenarioContext = scenarioContext;

    [AfterStep]
    public async Task AfterStep()
    {
        // Add screenshot on failure - Allure.Reqnroll handles everything else automatically
        if (_scenarioContext.TestError != null)
        {
            var screenshotBytes = await TakeScreenshotAsync();
            if (screenshotBytes != null)
            {
                var stepText = _scenarioContext.StepContext.StepInfo.Text;
                
                // Using AllureApi.AddAttachment as per official documentation
                AllureApi.AddAttachment(
                    $"Screenshot: {stepText}",
                    "image/png",
                    screenshotBytes,
                    ".png"
                );
            }
        }
    }

    private async Task<byte[]> TakeScreenshotAsync()
    {
        try
        {
            if (_scenarioContext.TryGetValue("currentPage", out IPage page) && page != null)
            {
                return await page.ScreenshotAsync();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to take screenshot: {ex.Message}");
        }

        return null;
    }
}

using System;
using System.IO;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using Microsoft.Playwright;
using PlaywrightReqnrollFramework.Helpers;
using Reqnroll;
using Reqnroll.Bindings;
using static PlaywrightReqnrollFramework.Helpers.ExtentReportManager;

namespace PlaywrightReqnrollFramework.Hook;

[Binding]
public class ExtentReportHooks(ScenarioContext scenarioContext, FeatureContext featureContext)
{
    private readonly ScenarioContext _scenarioContext = scenarioContext;
    private readonly FeatureContext _featureContext = featureContext;

    [BeforeTestRun]
    public static void InitializeExtentReporter()
    {
        ExtentReportManager.InitializeExtentReport();
    }
    [BeforeFeature]
    public static void BeforeFeature(FeatureContext featureContext)
    {
        ExtentReportManager.CreateFeature(featureContext.FeatureInfo.Title);
    }

    [BeforeScenario]
    public void BeforeScenario()
    {
        ExtentReportManager.CreateScenario(_scenarioContext.ScenarioInfo.Title);
    }
    private StepType GetStepType(StepDefinitionType stepDefinitionType)
{
    // Map Reqnroll step types to Gherkin step types
    return stepDefinitionType switch
    {
        StepDefinitionType.Given => StepType.Given,
        StepDefinitionType.When => StepType.When,
        StepDefinitionType.Then => StepType.Then,
        _ => StepType.And // Handle And/But steps
    };
}
    [AfterStep]
    public void AfterStep()
    {
        var stepType = GetStepType(_scenarioContext.StepContext.StepInfo.StepDefinitionType);
        var stepText = _scenarioContext.StepContext.StepInfo.Text;

        if (_scenarioContext.TestError == null)
        {
            ExtentReportManager.LogStep(Status.Pass, stepText,stepType);
        }
        else
        {
             var screenshotPath = TakeScreenshot();
            ExtentReportManager.LogFailedStepWithScreenshot(
            stepText, 
            stepType, 
            _scenarioContext.TestError.Message, 
            screenshotPath);
        }
    }
    [AfterTestRun]
    public static void AfterTestRun()
    {
        ExtentReportManager.FlushReport();
    }

     private string TakeScreenshot()
    {
       try
    {
        var screenshotDir = Path.Combine(Directory.GetCurrentDirectory(), "TestResults", "Screenshots");
        Directory.CreateDirectory(screenshotDir); // No need to check exists first
        
        var screenshotPath = Path.Combine(screenshotDir, $"screenshot_{DateTime.Now:yyyyMMddHHmmss}.png");
        var _page = _scenarioContext.Get<IPage>("currentPage") ?? 
                   throw new InvalidOperationException("Current page is not set in ScenarioContext.");
        
        _page.ScreenshotAsync(new PageScreenshotOptions 
        { 
            Path = screenshotPath,
            FullPage = true  // Capture full page
        }).Wait();
        
        return screenshotPath;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Failed to take screenshot: {ex.Message}");
        return null;
    }
    }

}

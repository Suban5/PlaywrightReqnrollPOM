using System;
using System.IO;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;

namespace PlaywrightReqnrollFramework.Helpers;

public static class ExtentReportManager
{
    private static ExtentTest _feature;
    private static ExtentTest _scenario;
    private static ExtentReports _extent;
    private static string _reportPath = Path.Combine(Directory.GetCurrentDirectory(), "TestResults", "ExtentReports");

    public static void InitializeExtentReport()
    {
        if (!Directory.Exists(_reportPath))
        {
            Directory.CreateDirectory(_reportPath);
        }

        var sparkReporter = new ExtentSparkReporter(Path.Combine(_reportPath, $"ExtentReport_{DateTime.Now:yyyyMMddHHmmss}.html"));

        // Optional: Configure Spark Reporter (modern look)
        sparkReporter.Config.Theme = AventStack.ExtentReports.Reporter.Config.Theme.Dark;
        sparkReporter.Config.DocumentTitle = "Playwright Test Report";
        sparkReporter.Config.ReportName = "Automation Test Results";

        _extent = new ExtentReports();
        _extent.AttachReporter(sparkReporter);

    }

    public static void CreateFeature(string featureName)
    {
        _feature = _extent.CreateTest<Feature>(featureName);
    }

    public static void CreateScenario(string scenarioName)
    {
        _scenario = _feature.CreateNode<Scenario>(scenarioName);
    }

    public static void LogStep(Status status, string message, StepType stepType = StepType.Given)
    {
        if (_scenario == null) return;

        try
        {
            ExtentTest stepNode = stepType switch
            {
                StepType.Given => _scenario.CreateNode<Given>(message),
                StepType.When => _scenario.CreateNode<When>(message),
                StepType.Then => _scenario.CreateNode<Then>(message),
                StepType.And => _scenario.CreateNode<And>(message),
                StepType.But => _scenario.CreateNode<But>(message),
                _ => _scenario.CreateNode<Given>(message)
            };

            if (status == Status.Pass)
            {
                stepNode.Pass("passed");
            }
            else
            {
                stepNode.Fail("failed");


            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to log step: {ex.Message}");
        }
    }

    // StepType enum for better readability
    public enum StepType
    {
        Given,
        When,
        Then,
        And,
        But
    }

    public static void LogFailedStepWithScreenshot(string message, StepType stepType, string errorMessage, string screenshotPath = null)
    {
        if (_scenario == null) return;

        try
        {
            ExtentTest stepNode = stepType switch
            {
                StepType.Given => _scenario.CreateNode<Given>(message),
                StepType.When => _scenario.CreateNode<When>(message),
                StepType.Then => _scenario.CreateNode<Then>(message),
                _ => _scenario.CreateNode<And>(message)
            };

            if (!string.IsNullOrEmpty(screenshotPath) && File.Exists(screenshotPath))
            {
                // This is the key change - properly attach the screenshot
                stepNode.Fail(errorMessage,
                    MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
            }
            else
            {
                stepNode.Fail(errorMessage);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to log failed step: {ex.Message}");
        }
    }
    public static void AddScreenshot(string screenshotPath, string title = "Screenshot")
    {
        if (File.Exists(screenshotPath))
        {
            _scenario.Info(title, MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
        }
    }

    public static void FlushReport()
    {
        try
        {
            _extent.Flush();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to flush report: {ex.Message}");
        }
    }

}

using System;
using System.IO;
using Allure.Net.Commons;

namespace PlaywrightReqnrollFramework.Helpers;

/// <summary>
/// Manager for Allure Report integration with Reqnroll
/// Uses AllureApi as per official Allure.Reqnroll documentation
/// </summary>
public static class AllureReportManager
{
    /// <summary>
    /// Add screenshot to current test step
    /// </summary>
    public static void AddScreenshot(string name, byte[] screenshot)
    {
        try
        {
            if (screenshot != null && screenshot.Length > 0)
            {
                AllureApi.AddAttachment(name, "image/png", screenshot, ".png");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to add screenshot to Allure: {ex.Message}");
        }
    }

    /// <summary>
    /// Add screenshot from file path
    /// </summary>
    public static void AddScreenshotFromFile(string name, string filePath)
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(filePath) && File.Exists(filePath))
            {
                var screenshot = File.ReadAllBytes(filePath);
                AddScreenshot(name, screenshot);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to add screenshot from file to Allure: {ex.Message}");
        }
    }

    /// <summary>
    /// Add text attachment
    /// </summary>
    public static void AddText(string name, string content)
    {
        try
        {
            AllureApi.AddAttachment(name, "text/plain", System.Text.Encoding.UTF8.GetBytes(content), ".txt");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to add text attachment to Allure: {ex.Message}");
        }
    }

    /// <summary>
    /// Add JSON attachment
    /// </summary>
    public static void AddJson(string name, string json)
    {
        try
        {
            AllureApi.AddAttachment(name, "application/json", System.Text.Encoding.UTF8.GetBytes(json), ".json");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to add JSON attachment to Allure: {ex.Message}");
        }
    }

    /// <summary>
    /// Add step to current test (Note: Steps are automatically captured by Reqnroll)
    /// </summary>
    public static void AddStep(string stepName, Status status = Status.passed)
    {
        try
        {
            // Steps are automatically captured by Reqnroll/NUnit integration
            // This is here for compatibility but does nothing as Allure.NUnit handles it
            Console.WriteLine($"Step: {stepName} - {status}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to log step: {ex.Message}");
        }
    }

    /// <summary>
    /// Add parameter to current test
    /// </summary>
    public static void AddParameter(string name, string value)
    {
        try
        {
            AllureLifecycle.Instance.UpdateTestCase(tc => 
            {
                tc.parameters.Add(new Parameter { name = name, value = value });
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to add parameter to Allure: {ex.Message}");
        }
    }

    /// <summary>
    /// Set test description
    /// </summary>
    public static void SetDescription(string description)
    {
        try
        {
            AllureLifecycle.Instance.UpdateTestCase(tc => tc.description = description);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to set description in Allure: {ex.Message}");
        }
    }

    /// <summary>
    /// Add link to test (e.g., Jira ticket, test management system)
    /// </summary>
    public static void AddLink(string name, string url, string type = "link")
    {
        try
        {
            AllureLifecycle.Instance.UpdateTestCase(tc => 
            {
                tc.links.Add(new Link { name = name, url = url, type = type });
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to add link to Allure: {ex.Message}");
        }
    }
}

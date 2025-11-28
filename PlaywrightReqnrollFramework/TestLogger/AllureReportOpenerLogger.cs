using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;

namespace PlaywrightReqnrollFramework.TestLogger;

/// <summary>
/// Custom test logger that automatically opens Allure report after tests complete
/// Works with VS Code Test Explorer, Visual Studio Test Explorer, and dotnet test
/// </summary>
[FriendlyName("AllureReportOpener")]
[ExtensionUri("logger://AllureReportOpener")]
public class AllureReportOpenerLogger : ITestLoggerWithParameters
{
    public void Initialize(TestLoggerEvents events, string testResultsDirPath)
    {
        if (events == null) throw new ArgumentNullException(nameof(events));

        events.TestRunComplete += OnTestRunComplete;
    }

    public void Initialize(TestLoggerEvents events, Dictionary<string, string> parameters)
    {
        Initialize(events, parameters?.GetValueOrDefault("TestRunDirectory"));
    }

    private void OnTestRunComplete(object sender, TestRunCompleteEventArgs e)
    {
        try
        {
            Console.WriteLine("");
            Console.WriteLine("========================================");
            Console.WriteLine("🎯 Test Execution Complete!");
            Console.WriteLine($"Total: {e.TestRunStatistics.ExecutedTests}");
            Console.WriteLine($"✅ Passed: {e.TestRunStatistics[TestOutcome.Passed]}");
            Console.WriteLine($"❌ Failed: {e.TestRunStatistics[TestOutcome.Failed]}");
            Console.WriteLine($"⏭️  Skipped: {e.TestRunStatistics[TestOutcome.Skipped]}");
            Console.WriteLine("========================================");
            Console.WriteLine("");

            // Find allure-results directory
            var allureResultsPath = FindAllureResultsDirectory();
            
            if (allureResultsPath != null && Directory.Exists(allureResultsPath))
            {
                var resultsCount = Directory.GetFiles(allureResultsPath, "*.json").Length;
                
                if (resultsCount > 0)
                {
                    Console.WriteLine($"📊 Found {resultsCount} Allure test results");
                    Console.WriteLine("🚀 Opening Allure Report...");
                    Console.WriteLine("");
                    
                    OpenAllureReport(allureResultsPath);
                }
                else
                {
                    Console.WriteLine("⚠️  No Allure results found. Skipping report generation.");
                }
            }
            else
            {
                Console.WriteLine("⚠️  Allure results directory not found. Skipping report generation.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"⚠️  Failed to open Allure report: {ex.Message}");
        }
    }

    private string FindAllureResultsDirectory()
    {
        // Try to find bin/Debug/net8.0/allure-results or bin/Release/net8.0/allure-results
        var currentDir = Directory.GetCurrentDirectory();
        
        // Common paths to check
        var pathsToCheck = new[]
        {
            Path.Combine(currentDir, "PlaywrightReqnrollFramework", "bin", "Debug", "net8.0", "allure-results"),
            Path.Combine(currentDir, "PlaywrightReqnrollFramework", "bin", "Release", "net8.0", "allure-results"),
            Path.Combine(currentDir, "bin", "Debug", "net8.0", "allure-results"),
            Path.Combine(currentDir, "bin", "Release", "net8.0", "allure-results")
        };

        foreach (var path in pathsToCheck)
        {
            if (Directory.Exists(path))
            {
                return path;
            }
        }

        return null;
    }

    private void OpenAllureReport(string allureResultsPath)
    {
        try
        {
            // Find allure executable
            var allurePath = FindAllureExecutable();
            
            if (allurePath == null)
            {
                Console.WriteLine("⚠️  Allure CLI not found. Install with: brew install allure");
                Console.WriteLine($"   Results available at: {allureResultsPath}");
                return;
            }

            // Start allure serve in background
            var processInfo = new ProcessStartInfo
            {
                FileName = allurePath,
                Arguments = $"serve \"{allureResultsPath}\"",
                UseShellExecute = true,
                CreateNoWindow = false
            };

            Process.Start(processInfo);
            Console.WriteLine("✅ Allure report server started!");
            Console.WriteLine("   The report will open in your browser shortly...");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"⚠️  Could not start Allure: {ex.Message}");
        }
    }

    private string FindAllureExecutable()
    {
        // Check common installation paths
        var pathsToCheck = new[]
        {
            "/opt/homebrew/bin/allure",  // Apple Silicon Mac
            "/usr/local/bin/allure",      // Intel Mac / Linux
            "allure"                       // In PATH
        };

        foreach (var path in pathsToCheck)
        {
            if (File.Exists(path) || IsInPath(path))
            {
                return path;
            }
        }

        return null;
    }

    private bool IsInPath(string command)
    {
        try
        {
            var processInfo = new ProcessStartInfo
            {
                FileName = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "where" : "which",
                Arguments = command,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = Process.Start(processInfo);
            process?.WaitForExit();
            return process?.ExitCode == 0;
        }
        catch
        {
            return false;
        }
    }
}

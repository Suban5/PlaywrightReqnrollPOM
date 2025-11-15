using System;

namespace PlaywrightReqnrollFramework.Config;

public class TestSettings
{
    /// <summary>
    /// Timeout in seconds for page operations (will be converted to milliseconds for Playwright)
    /// </summary>
    public float Timeout { get; set; } = 30f;
    
    /// <summary>
    /// Run browser in headless mode (no UI)
    /// </summary>
    public bool Headless { get; set; } = false;
    
    /// <summary>
    /// Slow down operations by specified milliseconds (useful for debugging)
    /// </summary>
    public int SlowMo { get; set; } = 500;
    
    /// <summary>
    /// Base URL for the application under test
    /// </summary>
    public string BaseUrl { get; set; } = "https://www.saucedemo.com";

    /// <summary>
    /// Browser type to use for testing
    /// </summary>
    public BrowserTypeEnum BrowserType { get; set; }

    public enum BrowserTypeEnum
    {
        Chromium,
        Chrome,
        Edge,
        Firefox,
        Webkit
    }
}

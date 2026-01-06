using System;

namespace PlaywrightReqnrollFramework.Config;

public class TestSettings
{
    /// <summary>
    /// Default timeout in seconds for page operations (will be converted to milliseconds for Playwright)
    /// </summary>
    public float Timeout { get; set; } = 30f;
    
    /// <summary>
    /// Timeout in seconds for actions like click, fill, etc.
    /// </summary>
    public float ActionTimeout { get; set; } = 10f;
    
    /// <summary>
    /// Timeout in seconds for navigation operations
    /// </summary>
    public float NavigationTimeout { get; set; } = 30f;
    
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
    
    /// <summary>
    /// Browser context configuration
    /// </summary>
    public BrowserContextSettings ContextSettings { get; set; } = new();

    public enum BrowserTypeEnum
    {
        Chromium,
        Chrome,
        Edge,
        Firefox,
        Webkit
    }
}

public class BrowserContextSettings
{
    /// <summary>
    /// Viewport width in pixels
    /// </summary>
    public int ViewportWidth { get; set; } = 1920;
    
    /// <summary>
    /// Viewport height in pixels
    /// </summary>
    public int ViewportHeight { get; set; } = 1080;
    
    /// <summary>
    /// Locale for the browser context (e.g., "en-US", "de-DE")
    /// </summary>
    public string Locale { get; set; } = "en-US";
    
    /// <summary>
    /// Timezone for the browser context (e.g., "America/New_York", "Europe/Berlin")
    /// </summary>
    public string TimezoneId { get; set; } = "America/Los_Angeles";
    
    /// <summary>
    /// Whether to automatically accept downloads
    /// </summary>
    public bool AcceptDownloads { get; set; } = true;
    
    /// <summary>
    /// Whether to ignore HTTPS errors
    /// </summary>
    public bool IgnoreHTTPSErrors { get; set; } = false;
    
    /// <summary>
    /// Device scale factor (pixel ratio)
    /// </summary>
    public float DeviceScaleFactor { get; set; } = 1.0f;
    
    /// <summary>
    /// Whether to emulate mobile device
    /// </summary>
    public bool IsMobile { get; set; } = false;
    
    /// <summary>
    /// Whether to record video of test execution
    /// </summary>
    public bool RecordVideo { get; set; } = false;
    
    /// <summary>
    /// Directory to save recorded videos (if RecordVideo is true)
    /// </summary>
    public string VideoDir { get; set; } = "videos";
    
    /// <summary>
    /// Record trace for debugging (off, on, retain-on-failure)
    /// </summary>
    public string RecordTrace { get; set; } = "retain-on-failure";
}

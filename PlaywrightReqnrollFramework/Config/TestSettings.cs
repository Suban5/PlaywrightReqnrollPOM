using System;

namespace PlaywrightReqnrollFramework.Config;

public class TestSettings
{
    public float Timeout { get; set; } = 30f; // Default timeout in seconds
    public bool Headless { get; set; } = false;
    public int SlowMo { get; set; } = 500; // Slow down operations by 500ms
    public string BaseUrl { get; set; } = "https://www.saucedemo.com"; // Default base URL

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

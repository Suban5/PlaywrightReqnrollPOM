using System;

namespace PlaywrightReqnrollFramework.Config;

public class UITestSettings
{
    public float Timeout { get; set; } = 30f; // Default timeout in seconds
    public bool Headless { get; set; } = false;
    public int SlowMo { get; set; } = 500; // Slow down operations by 500ms
    public string BaseUrl { get; set; } = "https://www.saucedemo.com"; // Default base URL
    public BrowserTypeEnum BrowserType { get; set; } = BrowserTypeEnum.Firefox; // Default browser type
    public enum BrowserTypeEnum { Chromium, Chrome, Firefox, Webkit, Edge }
}

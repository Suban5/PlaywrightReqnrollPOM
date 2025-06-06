using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using PlaywrightReqnrollFramework.Config;
using static PlaywrightReqnrollFramework.Config.TestSettings;

namespace PlaywrightReqnrollFramework.Driver;

public class PlaywrightDriver(TestSettings testSettings)
{

    private IPlaywright _playwright;
    private IBrowser _browser;
    private IBrowserContext _context;
    private IPage _page;
    private readonly TestSettings testSettings = testSettings;

    public async Task<IPage> InitializeAsync()
    {
        // Initialize Playwright
        _playwright = await Playwright.CreateAsync();
        // Get the browser instance based on the test settings
        _browser = await GetBrowserAsync();
        // Create a new browser context
        _context = await _browser.NewContextAsync();
        // Create a new page in the context
        _page = await _context.NewPageAsync();
        return _page;
    }
    public async Task DisposeAsync()
    {
        // Dispose of the browser context and browser
        await _browser.CloseAsync();
        await _browser.DisposeAsync();

    }

    private async Task<IBrowser> GetBrowserAsync()
    {

        // Browser Type Launch Options
        var launchOptions = new BrowserTypeLaunchOptions
        {
            Headless = testSettings.Headless, // Set headless mode based on the configuration
            SlowMo = testSettings.SlowMo // Slow down operations by 50ms
        };

        switch (testSettings.BrowserType)
        {
            case BrowserTypeEnum.Firefox:
                return await _playwright.Firefox.LaunchAsync(launchOptions);
            case BrowserTypeEnum.Webkit:
                return await _playwright.Webkit.LaunchAsync(launchOptions);
            case BrowserTypeEnum.Edge:
                launchOptions.Channel = "msedge"; // Specify the Edge browser channel
                return await _playwright.Chromium.LaunchAsync(launchOptions);
            case BrowserTypeEnum.Chrome:
                launchOptions.Channel = "chrome"; // Specify the Chrome browser channel
                return await _playwright.Chromium.LaunchAsync(launchOptions);
            default:
                return await _playwright.Chromium.LaunchAsync(launchOptions);
        }

    }
}

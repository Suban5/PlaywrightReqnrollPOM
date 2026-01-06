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
        // Create a new browser context with configuration
        _context = await CreateBrowserContextAsync();
        // Create a new page in the context
        _page = await _context.NewPageAsync();
        
        // Set page-level timeouts
        _page.SetDefaultTimeout(testSettings.Timeout * 1000);
        _page.SetDefaultNavigationTimeout(testSettings.NavigationTimeout * 1000);
        
        return _page;
    }
    
    private async Task<IBrowserContext> CreateBrowserContextAsync()
    {
        var contextOptions = new BrowserNewContextOptions
        {
            ViewportSize = new ViewportSize
            {
                Width = testSettings.ContextSettings.ViewportWidth,
                Height = testSettings.ContextSettings.ViewportHeight
            },
            Locale = testSettings.ContextSettings.Locale,
            TimezoneId = testSettings.ContextSettings.TimezoneId,
            AcceptDownloads = testSettings.ContextSettings.AcceptDownloads,
            IgnoreHTTPSErrors = testSettings.ContextSettings.IgnoreHTTPSErrors,
            DeviceScaleFactor = testSettings.ContextSettings.DeviceScaleFactor,
            IsMobile = testSettings.ContextSettings.IsMobile,
        };
        
        // Configure video recording if enabled
        if (testSettings.ContextSettings.RecordVideo)
        {
            contextOptions.RecordVideoDir = testSettings.ContextSettings.VideoDir;
            contextOptions.RecordVideoSize = new RecordVideoSize
            {
                Width = testSettings.ContextSettings.ViewportWidth,
                Height = testSettings.ContextSettings.ViewportHeight
            };
        }
        
        var context = await _browser.NewContextAsync(contextOptions);
        
        // Start tracing if configured
        if (testSettings.ContextSettings.RecordTrace != "off")
        {
            await context.Tracing.StartAsync(new()
            {
                Screenshots = true,
                Snapshots = true,
                Sources = true
            });
        }
        
        return context;
    }
    public async Task DisposeAsync()
    {
        // Dispose of all resources in reverse order of creation
        if (_page != null)
        {
            await _page.CloseAsync();
        }

        if (_context != null)
        {
            await _context.CloseAsync();
            await _context.DisposeAsync();
        }

        if (_browser != null)
        {
            await _browser.CloseAsync();
            await _browser.DisposeAsync();
        }

        if (_playwright != null)
        {
            _playwright.Dispose();
        }
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

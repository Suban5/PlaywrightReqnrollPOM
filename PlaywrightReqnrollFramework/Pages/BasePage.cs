using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.Playwright;
using PlaywrightReqnrollFramework.Config;
using Reqnroll;

namespace PlaywrightReqnrollFramework.Pages;

public abstract class BasePage(ScenarioContext scenarioContext)
{
    protected readonly IPage _page = scenarioContext.Get<IPage>("currentPage");
    protected readonly ScenarioContext _scenarioContext = scenarioContext;
    private readonly TestSettings _testSettings = scenarioContext.Get<TestSettings>("testSettings");

    /// <summary>
    /// Gets the configured action timeout in milliseconds
    /// </summary>
    protected float ActionTimeout => _testSettings.ActionTimeout * 1000;

    #region Action Helper Methods with Timeout

    /// <summary>
    /// Click an element with action timeout
    /// </summary>
    protected async Task ClickAsync(ILocator locator, LocatorClickOptions options = null)
    {
        options ??= new LocatorClickOptions();
        options.Timeout = ActionTimeout;
        await locator.ClickAsync(options);
    }

    /// <summary>
    /// Fill an input with action timeout
    /// </summary>
    protected async Task FillAsync(ILocator locator, string value, LocatorFillOptions options = null)
    {
        options ??= new LocatorFillOptions();
        options.Timeout = ActionTimeout;
        await locator.FillAsync(value, options);
    }

    /// <summary>
    /// Check a checkbox with action timeout
    /// </summary>
    protected async Task CheckAsync(ILocator locator, LocatorCheckOptions options = null)
    {
        options ??= new LocatorCheckOptions();
        options.Timeout = ActionTimeout;
        await locator.CheckAsync(options);
    }

    /// <summary>
    /// Select option from dropdown with action timeout
    /// </summary>
    protected async Task SelectOptionAsync(ILocator locator, string value, LocatorSelectOptionOptions options = null)
    {
        options ??= new LocatorSelectOptionOptions();
        options.Timeout = ActionTimeout;
        await locator.SelectOptionAsync(value, options);
    }

    /// <summary>
    /// Wait for element to be visible with action timeout
    /// </summary>
    protected async Task WaitForVisibleAsync(ILocator locator)
    {
        await locator.WaitForAsync(new LocatorWaitForOptions
        {
            State = WaitForSelectorState.Visible,
            Timeout = ActionTimeout
        });
    }

    /// <summary>
    /// Wait for element to be hidden with action timeout
    /// </summary>
    protected async Task WaitForHiddenAsync(ILocator locator)
    {
        await locator.WaitForAsync(new LocatorWaitForOptions
        {
            State = WaitForSelectorState.Hidden,
            Timeout = ActionTimeout
        });
    }

    /// <summary>
    /// Navigate to URL with navigation timeout
    /// </summary>
    protected async Task GotoAsync(string url, PageGotoOptions options = null)
    {
        options ??= new PageGotoOptions();
        options.Timeout = _testSettings.NavigationTimeout * 1000;
        await _page.GotoAsync(url, options);
    }

    /// <summary>
    /// Get inner text with action timeout
    /// </summary>
    protected async Task<string> GetTextAsync(ILocator locator)
    {
        return await locator.InnerTextAsync(new LocatorInnerTextOptions
        {
            Timeout = ActionTimeout
        });
    }

    /// <summary>
    /// Check if element is visible with action timeout
    /// </summary>
    protected async Task<bool> IsVisibleAsync(ILocator locator)
    {
        try
        {
            return await locator.IsVisibleAsync();
        }
        catch (TimeoutException)
        {
            return false;
        }
    }

    #endregion

    /// <summary>
    /// Parses a price string (e.g., "$12.99") to a decimal value
    /// </summary>
    /// <param name="priceText">Price text with or without currency symbol</param>
    /// <returns>Decimal value of the price</returns>
    protected decimal ParsePrice(string priceText)
    {
        if (string.IsNullOrWhiteSpace(priceText))
            throw new ArgumentException("Price text cannot be null or empty", nameof(priceText));

        var cleanedPrice = priceText.Replace("$", "").Replace("Item total:", "").Replace("Tax:", "").Replace("Total:", "").Trim();
        
        if (decimal.TryParse(cleanedPrice, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out var price))
        {
            return price;
        }
        
        throw new FormatException($"Unable to parse price from text: '{priceText}'");
    }
}

using System;
using System.Globalization;
using Microsoft.Playwright;
using Reqnroll;

namespace PlaywrightReqnrollFramework.Pages;

public abstract class BasePage(ScenarioContext scenarioContext)
{
    protected readonly IPage _page = scenarioContext.Get<IPage>("currentPage");
    protected readonly ScenarioContext _scenarioContext = scenarioContext;

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

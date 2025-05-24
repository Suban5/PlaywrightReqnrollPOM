using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Reqnroll;

namespace PlaywrightReqnrollFramework.Pages;

public class CheckoutOverviewPage(ScenarioContext scenarioContext) : BasePage(scenarioContext)
{
    private ILocator FinishButton => _page.Locator("#finish");
    private ILocator CancelButton => _page.Locator("#cancel");
    private ILocator ItemTotal => _page.Locator(".summary_subtotal_label");
    private ILocator Tax => _page.Locator(".summary_tax_label");
    private ILocator Total => _page.Locator(".summary_total_label");

    private ILocator GetCheckoutOverviewTitle()
    {
        return _page.Locator(".title");
    }

    private ILocator GetProductName(string productName)
    {
        return _page.Locator(".cart_item")
                    .Filter(new() { HasText = productName })
                    .Locator(".inventory_item_name");
    }
    private ILocator GetProductPrice(string productName)
    {
        return _page.Locator(".cart_item")
                    .Filter(new() { HasText = productName })
                    .Locator(".inventory_item_price");
    }

    private ILocator PaymentInfo => _page.Locator(".summary_value_label");
    private ILocator ShippingInfo => _page.Locator(".summary_value_label");

    public async Task<bool> IsCheckoutOverviewTitleVisibleAsync()
    {
        var title = GetCheckoutOverviewTitle();
        return await title.IsVisibleAsync() && (await title.InnerTextAsync()) == "Checkout: Overview";
    }

    public async Task IsProductInCheckoutOverviewAsync(string productName)
    {
        var productLocator = GetProductName(productName);
        if (!await productLocator.IsVisibleAsync())
        {
            throw new Exception($"Product '{productName}' is not present in the checkout overview.");
        }
    }
    public async Task<float> GetProductPriceAsync(string productName)
    {
        var priceLocator = GetProductPrice(productName);
        if (!await priceLocator.IsVisibleAsync())
        {
            throw new Exception($"Product '{productName}' is not present in the checkout overview.");
        }
        var priceText = await priceLocator.InnerTextAsync();
        return float.Parse(priceText.Replace("$", ""));
    }
    public async Task<float> GetItemTotalAsync()
    {
        var itemTotalText = await ItemTotal.InnerTextAsync();
        return float.Parse(itemTotalText.Replace("Item total: $", ""));
    }
    public async Task<float> GetTaxAsync()
    {
        var taxText = await Tax.InnerTextAsync();
        return float.Parse(taxText.Replace("Tax: $", ""));
    }
    public async Task<float> GetTotalAsync()
    {
        var totalText = await Total.InnerTextAsync();
        return float.Parse(totalText.Replace("Total: $", ""));
    }
    public async Task ClickFinishButtonAsync()
    {
        if (await FinishButton.IsVisibleAsync())
        {
            await FinishButton.ClickAsync();
        }
        else
        {
            throw new Exception("Finish button is not visible.");
        }
    }
    public async Task ClickCancelButtonAsync()
    {
        if (await CancelButton.IsVisibleAsync())
        {
            await CancelButton.ClickAsync();
        }
        else
        {
            throw new Exception("Cancel button is not visible.");
        }
    }
    public async Task<string> GetPaymentInfoAsync()
    {
        if (await PaymentInfo.IsVisibleAsync())
        {
            return await PaymentInfo.InnerTextAsync();
        }
        else
        {
            throw new Exception("Payment information is not visible.");
        }
    }
    public async Task<string> GetShippingInfoAsync()
    {
        if (await ShippingInfo.IsVisibleAsync())
        {
            return await ShippingInfo.InnerTextAsync();
        }
        else
        {
            throw new Exception("Shipping information is not visible.");
        }
    }
}

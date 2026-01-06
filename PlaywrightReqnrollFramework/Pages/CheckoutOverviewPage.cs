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
        return await IsVisibleAsync(title) && (await GetTextAsync(title)) == "Checkout: Overview";
    }

    public async Task<bool> IsProductInCheckoutOverviewAsync(string productName)
    {
        var productLocator = GetProductName(productName);
        return await IsVisibleAsync(productLocator);
    }

    public async Task<decimal> GetProductPriceAsync(string productName)
    {
        var priceLocator = GetProductPrice(productName);
        var priceText = await GetTextAsync(priceLocator);
        return ParsePrice(priceText);
    }

    public async Task<decimal> GetItemTotalAsync()
    {
        var itemTotalText = await GetTextAsync(ItemTotal);
        return ParsePrice(itemTotalText);
    }

    public async Task<decimal> GetTaxAsync()
    {
        var taxText = await GetTextAsync(Tax);
        return ParsePrice(taxText);
    }

    public async Task<decimal> GetTotalAsync()
    {
        var totalText = await GetTextAsync(Total);
        return ParsePrice(totalText);
    }

    public async Task ClickFinishButtonAsync()
    {
        await ClickAsync(FinishButton);
    }

    public async Task ClickCancelButtonAsync()
    {
        await ClickAsync(CancelButton);
    }

    public async Task<string> GetPaymentInfoAsync()
    {
        return await GetTextAsync(PaymentInfo);
    }

    public async Task<string> GetShippingInfoAsync()
    {
        return await GetTextAsync(ShippingInfo);
    }
}

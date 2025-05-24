using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Reqnroll;

namespace PlaywrightReqnrollFramework.Pages;

public class InventoryPage(ScenarioContext scenarioContext) : BasePage(scenarioContext)
{
    private ILocator BtnContinueShopping => _page.Locator("#continue-shopping");
    private ILocator BtnCheckout => _page.Locator("#checkout");

    private ILocator GetProductName(string productName)
    {
        return _page.Locator(".cart_item")
                    .Filter(new() { HasText = productName })
                    .Locator(".inventory_item_name");
    }
    private ILocator GetInventoryRemoveBtn(string productName)
    {
        return _page.Locator(".cart_item")
                    .Filter(new() { HasText = productName })
                    .Locator(".cart_button")
                    .Filter(new() { HasText = "Remove" });
    }
    private ILocator GetInventoryItemPrice(string productName)
    {
        return _page.Locator(".cart_item")
                    .Filter(new() { HasText = productName })
                    .Locator(".inventory_item_price");
    }

    public async Task<bool> isProductInInventoryAsync(string productName)
    {
        var productLocator = GetProductName(productName);
        return await productLocator.IsVisibleAsync();
    }
    public async Task ClickContinueShoppingAsync()
    {
        await BtnContinueShopping.ClickAsync();
    }
    public async Task ClickCheckoutAsync()
    {
        await BtnCheckout.ClickAsync();
    }
    public async Task RemoveProductFromCartAsync(string productName)
    {
        var removeButton = GetInventoryRemoveBtn(productName);
        if (await removeButton.IsVisibleAsync())
        {
            await removeButton.ClickAsync();
        }
    }
    public async Task<float> GetProductPriceAsync(string productName)
    {
        var priceLocator = GetInventoryItemPrice(productName);
        if (await priceLocator.IsVisibleAsync())
        {
            var priceText = await priceLocator.InnerTextAsync();
            return float.Parse(priceText.Replace("$", "").Trim());
        }
        else
        {
            throw new Exception($"Price for '{productName}' is not visible.");
        }
    }
}

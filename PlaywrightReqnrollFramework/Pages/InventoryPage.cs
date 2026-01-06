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
        return await IsVisibleAsync(productLocator);
    }
    
    public async Task ClickContinueShoppingAsync()
    {
        await ClickAsync(BtnContinueShopping);
    }
    
    public async Task ClickCheckoutAsync()
    {
        await ClickAsync(BtnCheckout);
    }
    
    public async Task RemoveProductFromCartAsync(string productName)
    {
        var removeButton = GetInventoryRemoveBtn(productName);
        await ClickAsync(removeButton);
    }
    
    public async Task<decimal> GetProductPriceAsync(string productName)
    {
        var priceLocator = GetInventoryItemPrice(productName);
        var priceText = await GetTextAsync(priceLocator);
        return ParsePrice(priceText);
    }
}

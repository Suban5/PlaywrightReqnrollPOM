using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework.Internal;
using PlaywrightReqnrollFramework.Config;
using Reqnroll;

namespace PlaywrightReqnrollFramework.Pages;

public class ProductPage(ScenarioContext scenarioContext) : BasePage(scenarioContext)
{



    // Define locators for elements on the inventory page
    private ILocator InventoryItems => _page.Locator(".inventory_item");

    private ILocator ProductsHeader => _page.Locator("[data-test='title']");
    private ILocator CartButton => _page.Locator("#shopping_cart_container");


    private ILocator GetAddToCartButton(string productName)
    {
        return _page.Locator(".inventory_item")
                    .Filter(new() { HasText = productName })
                    .Locator(".btn_inventory");
    }
    private ILocator GetProductPrice(string productName)
    {
        return _page.Locator(".inventory_item")
                    .Filter(new() { HasText = productName })
                    .Locator(".inventory_item_price");
    }



    // Method to verify if the inventory page is loaded
    public async Task<bool> IsInventoryPageLoadedAsync()
    {
        return await InventoryItems.CountAsync() > 0;
    }

    // Method to navigate to the cart
    public async Task NavigateToCartAsync()
    {
        await ClickAsync(CartButton);
    }

    public async Task<bool> IsHeaderVisibleAsync()
    {
        return await IsVisibleAsync(ProductsHeader) &&
               (await GetTextAsync(ProductsHeader)) == "Products";
    }

    public async Task AddProductToCartAsync(string productName)
    {
        var addToCartButton = GetAddToCartButton(productName);
        await ClickAsync(addToCartButton);
    }

    public async Task<decimal> GetProductPriceAsync(string productName)
    {
        var priceLocator = GetProductPrice(productName);
        var priceText = await GetTextAsync(priceLocator);
        return ParsePrice(priceText);
    }
    
    public async Task<bool> IsProductInCartAsync(string productName)
    {
        // Navigate to the cart
        await NavigateToCartAsync();

        // Check if the product is in the cart
        var productInCart = _page.Locator(".cart_item").Filter(new() { HasText = productName });
        return await productInCart.CountAsync() > 0;
    }
    
    public async Task<int> GetCartItemCountAsync()
    {
        var cartItemCountLocator = _page.Locator(".shopping_cart_badge");
        if (await IsVisibleAsync(cartItemCountLocator))
        {
            string countText = await GetTextAsync(cartItemCountLocator);
            return int.Parse(countText);
        }
        return 0; // Return 0 if the cart is empty or the badge is not visible
    }

}

using System;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace PlaywrightReqnrollFramework.Pages;

public class InventoryPage
{
    private readonly IPage _page;

    public InventoryPage(IPage page)
    {
        _page = page;
    }

    // Define locators for elements on the inventory page
    private ILocator InventoryItems => _page.Locator(".inventory_item");

    private ILocator ProductsHeader => _page.Locator("[data-test='title']");
    private ILocator CartButton => _page.Locator("#shopping_cart_container");

    // Method to verify if the inventory page is loaded
    public async Task<bool> IsInventoryPageLoadedAsync()
    {
        return await InventoryItems.CountAsync() > 0;
    }

    // Method to navigate to the cart
    public async Task NavigateToCartAsync()
    {
        await CartButton.ClickAsync();
    }

    public async Task<bool> isHeaderVisibleAsync()
    {
        return await ProductsHeader.IsVisibleAsync() &&
               (await ProductsHeader.InnerTextAsync()) == "Products";
    }

}

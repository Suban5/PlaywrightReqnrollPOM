using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Reqnroll;

namespace PlaywrightReqnrollFramework.Pages;

public class CheckoutCompletePage(ScenarioContext scenarioContext) : BasePage(scenarioContext)
{
    private ILocator CheckoutCompleteTitle => _page.Locator(".title");
    private ILocator CheckoutCompleteMessage => _page.Locator(".complete-text");
    private ILocator BackHomeButton => _page.Locator("#back-to-products");

    public async Task<bool> IsCheckoutCompleteTitleVisibleAsync()
    {
        return await CheckoutCompleteTitle.IsVisibleAsync() &&
               (await CheckoutCompleteTitle.InnerTextAsync()) == "Checkout: Complete!";
    }
    public async Task<bool> IsCheckoutCompleteMessageVisibleAsync()
    {
        return await CheckoutCompleteMessage.IsVisibleAsync() &&
               (await CheckoutCompleteMessage.InnerTextAsync()) == "Thank you for your order!";
    }
    public async Task ClickBackHomeButtonAsync()
    {
        await BackHomeButton.ClickAsync();
    }
}

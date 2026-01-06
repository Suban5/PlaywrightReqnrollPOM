using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Reqnroll;

namespace PlaywrightReqnrollFramework.Pages;

public class CheckoutPage(ScenarioContext scenarioContext) : BasePage(scenarioContext)
{
    private ILocator CheckoutTitle => _page.Locator(".title");
    private ILocator FirstNameField => _page.Locator("#first-name");
    private ILocator LastNameField => _page.Locator("#last-name");
    private ILocator PostalCodeField => _page.Locator("#postal-code");

    private ILocator ContinueButton => _page.Locator("#continue");
    private ILocator CancelButton => _page.Locator("#cancel");

    public async Task<bool> IsCheckoutTitleVisibleAsync()
    {
        return await IsVisibleAsync(CheckoutTitle) &&
               (await GetTextAsync(CheckoutTitle)) == "Checkout: Your Information";
    }

    public async Task FillCheckoutFormAsync(string firstName, string lastName, string postalCode)
    {
        await FillAsync(FirstNameField, firstName);
        await FillAsync(LastNameField, lastName);
        await FillAsync(PostalCodeField, postalCode);
    }
    
    public async Task ClickContinueButtonAsync()
    {
        await ClickAsync(ContinueButton);
    }
    
    public async Task ClickCancelButtonAsync()
    {
        await ClickAsync(CancelButton);
    }
}

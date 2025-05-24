using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using PlaywrightReqnrollFramework.Config;
using Reqnroll;

namespace PlaywrightReqnrollFramework.Pages;

public class LoginPage(ScenarioContext scenarioContext) : BasePage(scenarioContext)
{
    private ILocator UsernameField => _page.Locator("#user-name");
    private ILocator PasswordField => _page.Locator("#password");
    private ILocator LoginButton => _page.Locator("#login-button");

    private ILocator ErrorMessage => _page.Locator("h3[data-test='error']");

    public async Task NavigateToAsync(string url)
    {
        await _page.GotoAsync(url);
    }
    public async Task LoginAsync(string username, string password)
    {
        await UsernameField.FillAsync(username);
        await PasswordField.FillAsync(password);
        await LoginButton.ClickAsync();
    }

    public async Task<bool> IsErrorMessageVisibleAsync()
    {
        return await ErrorMessage.IsVisibleAsync();
    }
    public async Task<string> GetErrorMessageTextAsync()
    {
        return await ErrorMessage.InnerTextAsync();
    }

}

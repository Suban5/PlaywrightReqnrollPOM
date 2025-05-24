using System;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace PlaywrightReqnrollFramework.Pages;

public class LoginPage
{
    private readonly IPage _page;
    public LoginPage(IPage page)
    {
        _page = page;
    }

    private ILocator UsernameField => _page.Locator("#user-name");
    private ILocator PasswordField => _page.Locator("#password");
    private ILocator LoginButton => _page.Locator("#login-button");

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

}

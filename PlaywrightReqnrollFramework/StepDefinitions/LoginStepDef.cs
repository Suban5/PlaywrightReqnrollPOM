using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;
using PlaywrightReqnrollFramework.Pages;
using Reqnroll;

namespace PlaywrightReqnrollFramework.LoginStepDef
{
    [Binding]
    public class LoginStepDef
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly IPage _page;
        private readonly LoginPage _loginPage;
        private readonly InventoryPage _inventoryPage;

        public LoginStepDef(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _page = scenarioContext.Get<IPage>("currentPage");
            _loginPage = new LoginPage(_page);
            _inventoryPage = new InventoryPage(_page);
        }


        [Given(@"I navigate to {string}")]
        public async Task GivenINavigateTo(string url)
        {
            await _loginPage.NavigateToAsync(url);
        }

        [When("I login with username {string} and password {string}")]
        public async Task WhenILoginWithUsernameAndPassword(string username, string password)
        {
            await _loginPage.LoginAsync(username, password);
        }

        [Then(@"I should be redirected to the inventory page")]
        public async Task ThenIshouldberedirectedtotheinventorypage()
        {
            bool isInventoryPageLoaded = await _inventoryPage.IsInventoryPageLoadedAsync();
            Assert.That(isInventoryPageLoaded, Is.True, "Inventory page is not loaded after login.");
        }

        [Then(@"I should see the products header")]
        public async Task ThenIshouldseetheproductsheader()
        {
            bool isVisible = await _inventoryPage.IsInventoryPageLoadedAsync();
            Assert.That(isVisible, Is.True, "The 'Products' header is not visible on the page.");
        }


    }
}

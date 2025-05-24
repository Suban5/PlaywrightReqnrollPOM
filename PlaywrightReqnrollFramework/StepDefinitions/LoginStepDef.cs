using NUnit.Framework;
using PlaywrightReqnrollFramework.Pages;
using Reqnroll;

namespace PlaywrightReqnrollFramework.LoginStepDef
{
    [Binding]
    public class LoginStepDef : BasePage
    {
        private readonly PageFactory _factory;
        private readonly LoginPage _loginPage;
        private readonly ProductPage _productPage;

        public LoginStepDef(ScenarioContext scenarioContext) : base(scenarioContext)
        {
            _factory = new PageFactory(scenarioContext);
            _loginPage = _factory.GetPage<LoginPage>();
            _productPage = _factory.GetPage<ProductPage>();
        }



        [Given(@"I navigate to {string}")]
        public void GivenINavigateTo(string url)
        {
            _loginPage.NavigateToAsync(url).GetAwaiter().GetResult();
        }

        [When("I login with username {string} and password {string}")]
        public void WhenILoginWithUsernameAndPassword(string username, string password)
        {
            _loginPage.LoginAsync(username, password).GetAwaiter().GetResult();
        }

        [Then(@"I should be redirected to the inventory page")]
        public void ThenIshouldberedirectedtotheinventorypage()
        {
            bool isInventoryPageLoaded = _productPage.IsInventoryPageLoadedAsync().GetAwaiter().GetResult();
            Assert.That(isInventoryPageLoaded, Is.True, "Inventory page is not loaded after login.");
        }

        [Then(@"I should see the products header")]
        public void ThenIshouldseetheproductsheader()
        {
            bool isVisible = _productPage.IsInventoryPageLoadedAsync().GetAwaiter().GetResult();
            Assert.That(isVisible, Is.True, "The 'Products' header is not visible on the page.");
        }


        [Then(@"I should see the error message ""(.*)""")]
        public void ThenIshouldseetheerrormessage(string errorMessage)
        {
            bool isErrorMessageVisible = _loginPage.IsErrorMessageVisibleAsync().GetAwaiter().GetResult();
            Assert.That(isErrorMessageVisible, Is.True, "Error message is not visible.");
            string actualErrorMessage = _loginPage.GetErrorMessageTextAsync().GetAwaiter().GetResult();
            Assert.That(actualErrorMessage, Is.EqualTo(errorMessage), $"Expected error message '{errorMessage}' but got '{actualErrorMessage}'.");
        }


    }
}

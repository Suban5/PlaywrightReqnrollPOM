using System.Data;
using NUnit.Framework;
using PlaywrightReqnrollFramework.Pages;
using Reqnroll;

namespace PlaywrightReqnrollFramework.LoginStepDef
{
    [Binding]
    public class LoginStepDef : BasePage
    {

        public LoginStepDef(ScenarioContext scenarioContext) : base(scenarioContext)
        {

        }



        [Given(@"I navigate to {string}")]
        public void GivenINavigateTo(string url)
        {
            LoginPage.NavigateToAsync(url).GetAwaiter().GetResult();
        }

        [When("I login with username {string} and password {string}")]
        public void WhenILoginWithUsernameAndPassword(string username, string password)
        {
            LoginPage.LoginAsync(username, password).GetAwaiter().GetResult();
        }

        [Then(@"I should be redirected to the inventory page")]
        public void ThenIshouldberedirectedtotheinventorypage()
        {
            bool isInventoryPageLoaded = ProductPage.IsInventoryPageLoadedAsync().GetAwaiter().GetResult();
            Assert.That(isInventoryPageLoaded, Is.True, "Inventory page is not loaded after login.");
        }

        [Then(@"I should see the products header")]
        public void ThenIshouldseetheproductsheader()
        {
            bool isVisible = ProductPage.IsInventoryPageLoadedAsync().GetAwaiter().GetResult();
            Assert.That(isVisible, Is.True, "The 'Products' header is not visible on the page.");
        }


        [Then(@"I should see the error message ""(.*)""")]
        public void ThenIshouldseetheerrormessage(string errorMessage)
        {
            bool isErrorMessageVisible = LoginPage.IsErrorMessageVisibleAsync().GetAwaiter().GetResult();
            Assert.That(isErrorMessageVisible, Is.True, "Error message is not visible.");
            string actualErrorMessage = LoginPage.GetErrorMessageTextAsync().GetAwaiter().GetResult();
            Assert.That(actualErrorMessage, Is.EqualTo(errorMessage), $"Expected error message '{errorMessage}' but got '{actualErrorMessage}'.");
        }


    }
}

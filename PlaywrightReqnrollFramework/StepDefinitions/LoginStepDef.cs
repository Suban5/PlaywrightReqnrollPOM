using System.Data;
using System.Threading.Tasks;
using NUnit.Framework;
using PlaywrightReqnrollFramework.Pages;
using PlaywrightReqnrollFramework.StepDefinitions;
using Reqnroll;

namespace PlaywrightReqnrollFramework.LoginStepDef
{
    [Binding]
    public class LoginStepDef (ScenarioContext scenarioContext) : BaseSteps(scenarioContext)
    {

        [Given(@"I navigate to {string}")]
        public async Task GivenINavigateTo(string url)
        {
             await Page<LoginPage>().NavigateToAsync(url);
        }

        [When("I login with username {string} and password {string}")]
        public async Task WhenILoginWithUsernameAndPasswordAsync(string username, string password)
        {
            await Page<LoginPage>().LoginAsync(username, password);
        }

        [Then(@"I should be redirected to the inventory page")]
        public async Task ThenIshouldberedirectedtotheinventorypageAsync()
        {
            bool isInventoryPageLoaded = await Page<ProductPage>().IsInventoryPageLoadedAsync();
            Assert.That(isInventoryPageLoaded, Is.True, "Inventory page is not loaded after login.");
        }

        [Then(@"I should see the products header")]
        public async Task ThenIshouldseetheproductsheaderAsync()
        {
            bool isVisible = await Page<ProductPage>().IsInventoryPageLoadedAsync();
            Assert.That(isVisible, Is.True, "The 'Products' header is not visible on the page.");
        }


        [Then(@"I should see the error message ""(.*)""")]
        public async Task ThenIshouldseetheerrormessageAsync(string errorMessage)
        {
            bool isErrorMessageVisible = await Page<LoginPage>().IsErrorMessageVisibleAsync();
            Assert.That(isErrorMessageVisible, Is.True, "Error message is not visible.");
            string actualErrorMessage = await Page<LoginPage>().GetErrorMessageTextAsync();
            Assert.That(actualErrorMessage, Is.EqualTo(errorMessage), $"Expected error message '{errorMessage}' but got '{actualErrorMessage}'.");
        }


    }
}

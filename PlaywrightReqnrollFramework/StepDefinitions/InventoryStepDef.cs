using System;
using System.Threading.Tasks;
using NUnit.Framework;
using PlaywrightReqnrollFramework.Model;
using PlaywrightReqnrollFramework.Pages;
using Reqnroll;

namespace PlaywrightReqnrollFramework.StepDefinitions;

[Binding]
public class InventoryStepDef(ScenarioContext scenarioContext) : BaseSteps(scenarioContext)
{
    private float totalPrice = 0.0f;

    [When(@"I add following item to the cart")]
    public async Task WhenIaddfollowingitemtothecart(DataTable table)
    {
        var items = table.CreateSet<ProductItem>();

        foreach (var item in items)
        {
            await ProductPage.AddProductToCartAsync(item.ItemName);
            var itemPrice = await ProductPage.GetProductPriceAsync(item.ItemName);
            totalPrice += float.Parse(itemPrice);

        }
        Console.WriteLine($"Total Price of items in cart: {totalPrice}");
    }


    [Then(@"I navigate to the cart")]
    public async Task ThenInavigatetothecartAsync()
    {
       await ProductPage.NavigateToCartAsync();
    }


    [Then(@"I should see following item in the inventory")]
    public async Task ThenIshouldseefollowingitemintheinventoryAsync(DataTable table)
    {
        var items = table.CreateSet<ProductItem>();

        foreach (var item in items)
        {
            bool isProductVisible = await InventoryPage.isProductInInventoryAsync(item.ItemName);
            Assert.That(isProductVisible, Is.True, $"Product '{item.ItemName}' is not visible in the inventory.");
            float productPrice = await InventoryPage.GetProductPriceAsync(item.ItemName);
            Assert.That(productPrice, Is.EqualTo(item.ItemPrice), $"Product price for '{item.ItemName}' does not match. Expected: {item.ItemPrice}, Actual: {productPrice}");
        }
    }


    [Then(@"I clicked on the checkout button")]
    public async Task ThenIclickedonthecheckoutbuttonAsync()
    {
        await InventoryPage.ClickCheckoutAsync();
    }


    [Then(@"I should be redirected to the checkout page")]
    public async Task ThenIshouldberedirectedtothecheckoutpageAsync()
    {
        bool isCheckedOutPage = await CheckoutPage.IsCheckoutTitleVisibleAsync();
        Assert.That(isCheckedOutPage, Is.True, "Checkout page is not loaded after clicking checkout button.");
    }


    [When(@"I fill the checkout form with following details")]
    public void WhenIfillthecheckoutformwithfollowingdetails(DataTable table)
    {
        //CrateInstance only fetch first row of the table
        //If you want to fetch all rows, use CreateSet<T>() method
        var details = table.CreateInstance<CheckoutDetails>();
        CheckoutPage.FillCheckoutFormAsync(details.FirstName, details.LastName, details.PostalCode).GetAwaiter().GetResult();

    }


    [When(@"I click on the continue button")]
    public async Task WhenIclickonthecontinuebutton()
    {
        await CheckoutPage.ClickContinueButtonAsync();
    }


    [Then(@"I should be redirected to the overview page")]
    public async Task ThenIshouldberedirectedtotheoverviewpageAsync()
    {
       await CheckoutOverviewPage.IsCheckoutOverviewTitleVisibleAsync();
    }


    [Then(@"I should see the overview page with following details")]
    public async Task ThenIshouldseetheoverviewpagewithfollowingdetailsAsync(DataTable table)
    {
        var items = table.CreateSet<ProductItem>();

        foreach (var item in items)
        {
            await CheckoutOverviewPage.IsProductInCheckoutOverviewAsync(item.ItemName);
            float productPrice = await CheckoutOverviewPage.GetProductPriceAsync(item.ItemName);
            Assert.That(productPrice, Is.EqualTo(item.ItemPrice), $"Product price for '{item.ItemName}' does not match. Expected: {item.ItemPrice}, Actual: {productPrice}");
        }
    }


    [Then(@"The Item total should be {float}")]
    public async Task ThenTheItemtotalshouldbeAsync(float expectedItemTotal)
    {

        float actualItemTotal = await CheckoutOverviewPage.GetItemTotalAsync();
        Assert.That(actualItemTotal, Is.EqualTo(expectedItemTotal), $"Expected Item Total: {expectedItemTotal}, Actual Item Total: {actualItemTotal}");
    }


    [Then(@"The Tax should be {float}")]
    public async Task ThenTheTaxshouldbeAsync(float expectedTax)
    {
        float actualTax = await CheckoutOverviewPage.GetTaxAsync();
        Assert.That(actualTax, Is.EqualTo(expectedTax), $"Expected Tax: {expectedTax}, Actual Tax: {actualTax}");
    }

    [Then(@"The Total should be {float}")]
    public async Task ThenTheTotalshouldbeAsync(float expectedGrandTotal)
    {
        float actualTotal = await CheckoutOverviewPage.GetTotalAsync();
        Assert.That(actualTotal, Is.EqualTo(expectedGrandTotal), $"Expected Total: {expectedGrandTotal}, Actual Total: {actualTotal}");
    }


    [When(@"I click on the finish button")]
    public async Task WhenIclickonthefinishbuttonAsync()
    {
       await CheckoutOverviewPage.ClickFinishButtonAsync();
    }

    [Then(@"I should see the confirmation message ""(.*)""")]
    public async Task ThenIshouldseetheconfirmationmessageAsync(string args1)
    {
        await CheckoutCompletePage.IsCheckoutCompleteMessageVisibleAsync();

    }

    [When(@"I click on the back home button")]
    public async Task WhenIclickonthebackhomebuttonAsync()
    {
        await CheckoutCompletePage.ClickBackHomeButtonAsync();
    }


}

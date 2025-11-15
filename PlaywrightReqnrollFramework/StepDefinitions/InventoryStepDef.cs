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
    private decimal totalPrice = 0.0m;

    [When(@"I add following item to the cart")]
    public async Task WhenIaddfollowingitemtothecart(DataTable table)
    {
        var items = table.CreateSet<ProductItem>();

        foreach (var item in items)
        {
            await Page<ProductPage>().AddProductToCartAsync(item.ItemName);
            var itemPrice = await Page<ProductPage>().GetProductPriceAsync(item.ItemName);
            totalPrice += itemPrice;

        }
        Console.WriteLine($"Total Price of items in cart: {totalPrice}");
    }


    [Then(@"I navigate to the cart")]
    public async Task ThenInavigatetothecartAsync()
    {
       await Page<ProductPage>().NavigateToCartAsync();
    }


    [Then(@"I should see following item in the inventory")]
    public async Task ThenIshouldseefollowingitemintheinventoryAsync(DataTable table)
    {
        var items = table.CreateSet<ProductItem>();

        foreach (var item in items)
        {
            bool isProductVisible = await Page<InventoryPage>().isProductInInventoryAsync(item.ItemName);
            Assert.That(isProductVisible, Is.True, $"Product '{item.ItemName}' is not visible in the inventory.");
            decimal productPrice = await Page<InventoryPage>().GetProductPriceAsync(item.ItemName);
            Assert.That(productPrice, Is.EqualTo(item.ItemPrice), $"Product price for '{item.ItemName}' does not match. Expected: {item.ItemPrice}, Actual: {productPrice}");
        }
    }


    [Then(@"I clicked on the checkout button")]
    public async Task ThenIclickedonthecheckoutbuttonAsync()
    {
        await Page<InventoryPage>().ClickCheckoutAsync();
    }


    [Then(@"I should be redirected to the checkout page")]
    public async Task ThenIshouldberedirectedtothecheckoutpageAsync()
    {
        bool isCheckedOutPage = await Page<CheckoutPage>().IsCheckoutTitleVisibleAsync();
        Assert.That(isCheckedOutPage, Is.True, "Checkout page is not loaded after clicking checkout button.");
    }


    [When(@"I fill the checkout form with following details")]
    public void WhenIfillthecheckoutformwithfollowingdetails(DataTable table)
    {
        //CrateInstance only fetch first row of the table
        //If you want to fetch all rows, use CreateSet<T>() method
        var details = table.CreateInstance<CheckoutDetails>();
        Page<CheckoutPage>().FillCheckoutFormAsync(details.FirstName, details.LastName, details.PostalCode).GetAwaiter().GetResult();

    }


    [When(@"I click on the continue button")]
    public async Task WhenIclickonthecontinuebutton()
    {
        await Page<CheckoutPage>().ClickContinueButtonAsync();
    }


    [Then(@"I should be redirected to the overview page")]
    public async Task ThenIshouldberedirectedtotheoverviewpageAsync()
    {
       await Page<CheckoutOverviewPage>().IsCheckoutOverviewTitleVisibleAsync();
    }


    [Then(@"I should see the overview page with following details")]
    public async Task ThenIshouldseetheoverviewpagewithfollowingdetailsAsync(DataTable table)
    {
        var items = table.CreateSet<ProductItem>();

        foreach (var item in items)
        {
            bool isProductVisible = await Page<CheckoutOverviewPage>().IsProductInCheckoutOverviewAsync(item.ItemName);
            Assert.That(isProductVisible, Is.True, $"Product '{item.ItemName}' is not visible in the checkout overview.");
            decimal productPrice = await Page<CheckoutOverviewPage>().GetProductPriceAsync(item.ItemName);
            Assert.That(productPrice, Is.EqualTo(item.ItemPrice), $"Product price for '{item.ItemName}' does not match. Expected: {item.ItemPrice}, Actual: {productPrice}");
        }
    }


    [Then(@"The Item total should be {decimal}")]
    public async Task ThenTheItemtotalshouldbeAsync(decimal expectedItemTotal)
    {
        decimal actualItemTotal = await Page<CheckoutOverviewPage>().GetItemTotalAsync();
        Assert.That(actualItemTotal, Is.EqualTo(expectedItemTotal), $"Expected Item Total: {expectedItemTotal}, Actual Item Total: {actualItemTotal}");
    }


    [Then(@"The Tax should be {decimal}")]
    public async Task ThenTheTaxshouldbeAsync(decimal expectedTax)
    {
        decimal actualTax = await Page<CheckoutOverviewPage>().GetTaxAsync();
        Assert.That(actualTax, Is.EqualTo(expectedTax), $"Expected Tax: {expectedTax}, Actual Tax: {actualTax}");
    }

    [Then(@"The Total should be {decimal}")]
    public async Task ThenTheTotalshouldbeAsync(decimal expectedGrandTotal)
    {
        decimal actualTotal = await Page<CheckoutOverviewPage>().GetTotalAsync();
        Assert.That(actualTotal, Is.EqualTo(expectedGrandTotal), $"Expected Total: {expectedGrandTotal}, Actual Total: {actualTotal}");
    }


    [When(@"I click on the finish button")]
    public async Task WhenIclickonthefinishbuttonAsync()
    {
       await Page<CheckoutOverviewPage>().ClickFinishButtonAsync();
    }

    [Then(@"I should see the confirmation message ""(.*)""")]
    public async Task ThenIshouldseetheconfirmationmessageAsync(string args1)
    {
        await Page<CheckoutCompletePage>().IsCheckoutCompleteMessageVisibleAsync();

    }

    [When(@"I click on the back home button")]
    public async Task WhenIclickonthebackhomebuttonAsync()
    {
        await Page<CheckoutCompletePage>().ClickBackHomeButtonAsync();
    }


}

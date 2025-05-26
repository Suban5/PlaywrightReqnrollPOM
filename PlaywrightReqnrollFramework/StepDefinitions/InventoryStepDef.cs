using System;
using NUnit.Framework;
using PlaywrightReqnrollFramework.Model;
using PlaywrightReqnrollFramework.Pages;
using Reqnroll;

namespace PlaywrightReqnrollFramework.StepDefinitions;

[Binding]
public class InventoryStepDef : BasePage
{
    private float totalPrice = 0.0f;
    public InventoryStepDef(ScenarioContext scenarioContext) : base(scenarioContext)
    {
       
    }

    [When(@"I add following item to the cart")]
    public void WhenIaddfollowingitemtothecart(DataTable table)
    {
        var items = table.CreateSet<ProductItem>();

        foreach (var item in items)
        {
            ProductPage.AddProductToCartAsync(item.ItemName).GetAwaiter().GetResult();
            var itemPrice = ProductPage.GetProductPriceAsync(item.ItemName).GetAwaiter().GetResult();
            totalPrice += float.Parse(itemPrice);

        }
        Console.WriteLine($"Total Price of items in cart: {totalPrice}");
    }


    [Then(@"I navigate to the cart")]
    public void ThenInavigatetothecart()
    {
        ProductPage.NavigateToCartAsync().GetAwaiter().GetResult();
    }


    [Then(@"I should see following item in the inventory")]
    public void ThenIshouldseefollowingitemintheinventory(DataTable table)
    {
        var items = table.CreateSet<ProductItem>();

        foreach (var item in items)
        {
            bool isProductVisible = InventoryPage.isProductInInventoryAsync(item.ItemName).GetAwaiter().GetResult();
            Assert.That(isProductVisible, Is.True, $"Product '{item.ItemName}' is not visible in the inventory.");
            float productPrice = InventoryPage.GetProductPriceAsync(item.ItemName).GetAwaiter().GetResult();
            Assert.That(productPrice, Is.EqualTo(item.ItemPrice), $"Product price for '{item.ItemName}' does not match. Expected: {item.ItemPrice}, Actual: {productPrice}");
        }
    }


    [Then(@"I clicked on the checkout button")]
    public void ThenIclickedonthecheckoutbutton()
    {
        InventoryPage.ClickCheckoutAsync().GetAwaiter().GetResult();
    }


    [Then(@"I should be redirected to the checkout page")]
    public void ThenIshouldberedirectedtothecheckoutpage()
    {
        bool isCheckedOutPage = CheckoutPage.IsCheckoutTitleVisibleAsync().GetAwaiter().GetResult();
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
    public void WhenIclickonthecontinuebutton()
    {
        CheckoutPage.ClickContinueButtonAsync().GetAwaiter().GetResult();
    }


    [Then(@"I should be redirected to the overview page")]
    public void ThenIshouldberedirectedtotheoverviewpage()
    {
        CheckoutOverviewPage.IsCheckoutOverviewTitleVisibleAsync().GetAwaiter().GetResult();
    }


    [Then(@"I should see the overview page with following details")]
    public void ThenIshouldseetheoverviewpagewithfollowingdetails(DataTable table)
    {
        var items = table.CreateSet<ProductItem>();

        foreach (var item in items)
        {
            CheckoutOverviewPage.IsProductInCheckoutOverviewAsync(item.ItemName).GetAwaiter().GetResult();
            float productPrice = CheckoutOverviewPage.GetProductPriceAsync(item.ItemName).GetAwaiter().GetResult();
            Assert.That(productPrice, Is.EqualTo(item.ItemPrice), $"Product price for '{item.ItemName}' does not match. Expected: {item.ItemPrice}, Actual: {productPrice}");
        }
    }


    [Then(@"The Item total should be {float}")]
    public void ThenTheItemtotalshouldbe(float expectedItemTotal)
    {

        float actualItemTotal = CheckoutOverviewPage.GetItemTotalAsync().GetAwaiter().GetResult();
        Assert.That(actualItemTotal, Is.EqualTo(expectedItemTotal), $"Expected Item Total: {expectedItemTotal}, Actual Item Total: {actualItemTotal}");
    }


    [Then(@"The Tax should be {float}")]
    public void ThenTheTaxshouldbe(float expectedTax)
    {
        float actualTax = CheckoutOverviewPage.GetTaxAsync().GetAwaiter().GetResult();
        Assert.That(actualTax, Is.EqualTo(expectedTax), $"Expected Tax: {expectedTax}, Actual Tax: {actualTax}");
    }

    [Then(@"The Total should be {float}")]
    public void ThenTheTotalshouldbe(float expectedGrandTotal)
    {
        float actualTotal = CheckoutOverviewPage.GetTotalAsync().GetAwaiter().GetResult();
        Assert.That(actualTotal, Is.EqualTo(expectedGrandTotal), $"Expected Total: {expectedGrandTotal}, Actual Total: {actualTotal}");
    }


    [When(@"I click on the finish button")]
    public void WhenIclickonthefinishbutton()
    {
        CheckoutOverviewPage.ClickFinishButtonAsync().GetAwaiter().GetResult();
    }

    [Then(@"I should see the confirmation message ""(.*)""")]
    public void ThenIshouldseetheconfirmationmessage(string args1)
    {
        CheckoutCompletePage.IsCheckoutCompleteMessageVisibleAsync().GetAwaiter().GetResult();

    }

    [When(@"I click on the back home button")]
    public void WhenIclickonthebackhomebutton()
    {
        CheckoutCompletePage.ClickBackHomeButtonAsync().GetAwaiter().GetResult();
    }


}

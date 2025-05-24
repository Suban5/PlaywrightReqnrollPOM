using System;
using NUnit.Framework;
using PlaywrightReqnrollFramework.Model;
using PlaywrightReqnrollFramework.Pages;
using Reqnroll;

namespace PlaywrightReqnrollFramework.StepDefinitions;

[Binding]
public class InventoryStepDef : BasePage
{
    private readonly PageFactory _factory;
    private readonly InventoryPage _inventoryPage;
    private readonly ProductPage _productPage;
    private readonly CheckoutPage _checkoutPage;
    private readonly CheckoutOverviewPage _checkoutOverviewPage;
    private readonly CheckoutCompletePage _checkoutCompletePage;
    private float totalPrice = 0.0f;
    public InventoryStepDef(ScenarioContext scenarioContext) : base(scenarioContext)
    {
        _factory = new PageFactory(scenarioContext);
        _inventoryPage = _factory.GetPage<InventoryPage>();
        _productPage = _factory.GetPage<ProductPage>();
        _checkoutPage = _factory.GetPage<CheckoutPage>();
        _checkoutOverviewPage = _factory.GetPage<CheckoutOverviewPage>();
        _checkoutCompletePage = _factory.GetPage<CheckoutCompletePage>();
    }

    [When(@"I add following item to the cart")]
    public void WhenIaddfollowingitemtothecart(DataTable table)
    {
        var items = table.CreateSet<ProductItem>();

        foreach (var item in items)
        {
            _productPage.AddProductToCartAsync(item.ItemName).GetAwaiter().GetResult();
            var itemPrice = _productPage.GetProductPriceAsync(item.ItemName).GetAwaiter().GetResult();
            totalPrice += float.Parse(itemPrice);

        }
        Console.WriteLine($"Total Price of items in cart: {totalPrice}");
    }


    [Then(@"I navigate to the cart")]
    public void ThenInavigatetothecart()
    {
        _productPage.NavigateToCartAsync().GetAwaiter().GetResult();
    }


    [Then(@"I should see following item in the inventory")]
    public void ThenIshouldseefollowingitemintheinventory(DataTable table)
    {
        var items = table.CreateSet<ProductItem>();

        foreach (var item in items)
        {
            bool isProductVisible = _inventoryPage.isProductInInventoryAsync(item.ItemName).GetAwaiter().GetResult();
            Assert.That(isProductVisible, Is.True, $"Product '{item.ItemName}' is not visible in the inventory.");
            float productPrice = _inventoryPage.GetProductPriceAsync(item.ItemName).GetAwaiter().GetResult();
            Assert.That(productPrice, Is.EqualTo(item.ItemPrice), $"Product price for '{item.ItemName}' does not match. Expected: {item.ItemPrice}, Actual: {productPrice}");
        }
    }


    [Then(@"I clicked on the checkout button")]
    public void ThenIclickedonthecheckoutbutton()
    {
        _inventoryPage.ClickCheckoutAsync().GetAwaiter().GetResult();
    }


    [Then(@"I should be redirected to the checkout page")]
    public void ThenIshouldberedirectedtothecheckoutpage()
    {
        bool isCheckedOutPage = _checkoutPage.IsCheckoutTitleVisibleAsync().GetAwaiter().GetResult();
        Assert.That(isCheckedOutPage, Is.True, "Checkout page is not loaded after clicking checkout button.");
    }


    [When(@"I fill the checkout form with following details")]
    public void WhenIfillthecheckoutformwithfollowingdetails(DataTable table)
    {
        //CrateInstance only fetch first row of the table
        //If you want to fetch all rows, use CreateSet<T>() method
        var details = table.CreateInstance<CheckoutDetails>();
        _checkoutPage.FillCheckoutFormAsync(details.FirstName, details.LastName, details.PostalCode).GetAwaiter().GetResult();

    }


    [When(@"I click on the continue button")]
    public void WhenIclickonthecontinuebutton()
    {
        _checkoutPage.ClickContinueButtonAsync().GetAwaiter().GetResult();
    }


    [Then(@"I should be redirected to the overview page")]
    public void ThenIshouldberedirectedtotheoverviewpage()
    {
        _checkoutOverviewPage.IsCheckoutOverviewTitleVisibleAsync().GetAwaiter().GetResult();
    }


    [Then(@"I should see the overview page with following details")]
    public void ThenIshouldseetheoverviewpagewithfollowingdetails(DataTable table)
    {
        var items = table.CreateSet<ProductItem>();

        foreach (var item in items)
        {
            _checkoutOverviewPage.IsProductInCheckoutOverviewAsync(item.ItemName).GetAwaiter().GetResult();
            float productPrice = _checkoutOverviewPage.GetProductPriceAsync(item.ItemName).GetAwaiter().GetResult();
            Assert.That(productPrice, Is.EqualTo(item.ItemPrice), $"Product price for '{item.ItemName}' does not match. Expected: {item.ItemPrice}, Actual: {productPrice}");
        }
    }


    [Then(@"The Item total should be {float}")]
    public void ThenTheItemtotalshouldbe(float expectedItemTotal)
    {

        float actualItemTotal = _checkoutOverviewPage.GetItemTotalAsync().GetAwaiter().GetResult();
        Assert.That(actualItemTotal, Is.EqualTo(expectedItemTotal), $"Expected Item Total: {expectedItemTotal}, Actual Item Total: {actualItemTotal}");
    }


    [Then(@"The Tax should be {float}")]
    public void ThenTheTaxshouldbe(float expectedTax)
    {
        float actualTax = _checkoutOverviewPage.GetTaxAsync().GetAwaiter().GetResult();
        Assert.That(actualTax, Is.EqualTo(expectedTax), $"Expected Tax: {expectedTax}, Actual Tax: {actualTax}");
    }

    [Then(@"The Total should be {float}")]
    public void ThenTheTotalshouldbe(float expectedGrandTotal)
    {
        float actualTotal = _checkoutOverviewPage.GetTotalAsync().GetAwaiter().GetResult();
        Assert.That(actualTotal, Is.EqualTo(expectedGrandTotal), $"Expected Total: {expectedGrandTotal}, Actual Total: {actualTotal}");
    }


    [When(@"I click on the finish button")]
    public void WhenIclickonthefinishbutton()
    {
        _checkoutOverviewPage.ClickFinishButtonAsync().GetAwaiter().GetResult();
    }

    [Then(@"I should see the confirmation message ""(.*)""")]
    public void ThenIshouldseetheconfirmationmessage(string args1)
    {
        _checkoutCompletePage.IsCheckoutCompleteMessageVisibleAsync().GetAwaiter().GetResult();

    }

    [When(@"I click on the back home button")]
    public void WhenIclickonthebackhomebutton()
    {
        _checkoutCompletePage.ClickBackHomeButtonAsync().GetAwaiter().GetResult();
    }


}

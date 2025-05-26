using System;
using PlaywrightReqnrollFramework.Pages;
using Reqnroll;

namespace PlaywrightReqnrollFramework.StepDefinitions;

public class BaseSteps
{
    protected readonly PageFactory Factory;

    protected BaseSteps(ScenarioContext scenarioContext)
    {
        Factory = new PageFactory(scenarioContext);
    }

    protected InventoryPage InventoryPage => Factory.GetPage<InventoryPage>();
    protected ProductPage ProductPage => Factory.GetPage<ProductPage>();
    protected CheckoutPage CheckoutPage => Factory.GetPage<CheckoutPage>();
    protected CheckoutOverviewPage CheckoutOverviewPage => Factory.GetPage<CheckoutOverviewPage>();
    protected CheckoutCompletePage CheckoutCompletePage => Factory.GetPage<CheckoutCompletePage>();

    protected LoginPage LoginPage => Factory.GetPage<LoginPage>();
    
}
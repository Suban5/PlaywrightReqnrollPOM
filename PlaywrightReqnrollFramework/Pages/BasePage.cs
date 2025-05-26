using System;
using AventStack.ExtentReports.Model;
using Microsoft.Playwright;
using PlaywrightReqnrollFramework.Config;
using Reqnroll;

namespace PlaywrightReqnrollFramework.Pages;

public abstract class BasePage(ScenarioContext scenarioContext)
{
    protected readonly IPage _page = scenarioContext.Get<IPage>("currentPage");

    protected readonly PageFactory _pageFactory = new PageFactory(scenarioContext);
    protected readonly ScenarioContext _scenarioContext = scenarioContext;
    protected readonly TestSettings _testSettings = scenarioContext.Get<TestSettings>("testSettings");

    protected CheckoutCompletePage CheckoutCompletePage => _pageFactory.GetPage<CheckoutCompletePage>();
    protected CheckoutOverviewPage CheckoutOverviewPage => _pageFactory.GetPage<CheckoutOverviewPage>();
    protected CheckoutPage CheckoutPage => _pageFactory.GetPage<CheckoutPage>();
    protected InventoryPage InventoryPage => _pageFactory.GetPage<InventoryPage>();
    protected ProductPage ProductPage => _pageFactory.GetPage<ProductPage>();
    protected LoginPage LoginPage => _pageFactory.GetPage<LoginPage>();
   
}

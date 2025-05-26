using System;
using AventStack.ExtentReports.Model;
using Microsoft.Playwright;
using PlaywrightReqnrollFramework.Config;
using Reqnroll;

namespace PlaywrightReqnrollFramework.Pages;

public abstract class BasePage(ScenarioContext scenarioContext)
{
    protected readonly IPage _page = scenarioContext.Get<IPage>("currentPage");

    protected readonly ScenarioContext _scenarioContext = scenarioContext;
    protected readonly TestSettings _testSettings = scenarioContext.Get<TestSettings>("testSettings");

   
}

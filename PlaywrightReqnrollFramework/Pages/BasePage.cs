using System;
using Microsoft.Playwright;
using Reqnroll;

namespace PlaywrightReqnrollFramework.Pages;

public abstract class BasePage(ScenarioContext scenarioContext)
{
    protected readonly IPage _page = scenarioContext.Get<IPage>("currentPage");
    protected readonly ScenarioContext _scenarioContext = scenarioContext;
}

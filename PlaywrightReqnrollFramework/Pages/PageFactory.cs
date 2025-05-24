using System;
using Reqnroll;

namespace PlaywrightReqnrollFramework.Pages;

public class PageFactory(ScenarioContext scenarioContext)
{
    private readonly ScenarioContext _scenarioContext = scenarioContext;

    public T GetPage<T>() where T : BasePage
    {
        return (T)Activator.CreateInstance(typeof(T), _scenarioContext);
    }
}

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

    /// <summary>
    /// Gets an instance of the specified page type from the PageFactory.
    /// </summary>
    /// <typeparam name="T">The type of page to retrieve, must inherit from BasePage</typeparam>
    /// <returns>An instance of the requested page</returns>
    protected T Page<T>() where T : BasePage => Factory.GetPage<T>();
}
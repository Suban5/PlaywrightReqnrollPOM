using System;
using System.Collections.Generic;
using Reqnroll;

namespace PlaywrightReqnrollFramework.Pages;

public class PageFactory(ScenarioContext scenarioContext)
{
    private readonly ScenarioContext _scenarioContext = scenarioContext;
    private readonly Dictionary<Type, BasePage> _cache = [];

    public T GetPage<T>() where T : BasePage
    {
        if (!_cache.TryGetValue(typeof(T), out var page))
        {
            page = (T)Activator.CreateInstance(typeof(T), _scenarioContext);
            _cache[typeof(T)] = page;
        }
        return (T)page;
    }
}

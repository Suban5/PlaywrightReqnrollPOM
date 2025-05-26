using NUnit.Framework;
using Reqnroll;

namespace PlaywrightReqnrollFramework.StepDefinitions;

[Binding]
public sealed class CalculatorStepDefinitions(ScenarioContext scenarioContext) : BaseSteps(scenarioContext)
{
    // For additional details on Reqnroll step definitions see https://go.reqnroll.net/doc-stepdef
    private int firstNumber;
    private int secondNumber;
    private int result;

    [Given("the first number is {int}")]
    public void GivenTheFirstNumberIs(int number)
    {
        firstNumber = number;
    }

    [Given("the second number is {int}")]
    public void GivenTheSecondNumberIs(int number)
    {
        secondNumber = number;
    }

    [When("the two numbers are added")]
    public void WhenTheTwoNumbersAreAdded()
    {
        result = firstNumber + secondNumber;
    }

    [Then("the result should be {int}")]
    public void ThenTheResultShouldBe(int expectedresult)
    {
        Assert.That(result, Is.EqualTo(expectedresult));
    }
}

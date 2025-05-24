using NUnit.Framework;
using Reqnroll;

namespace PlaywrightReqnrollFramework.StepDefinitions;

[Binding]
public sealed class CalculatorStepDefinitions
{
    // For additional details on Reqnroll step definitions see https://go.reqnroll.net/doc-stepdef
    private int firstNumber;
    private int secondNumber;
    private int result;
    [Given("the first number is {int}")]
    public void GivenTheFirstNumberIs(int number)
    {
        //TODO: implement arrange (precondition) logic
        // For storing and retrieving scenario-specific data see https://go.reqnroll.net/doc-sharingdata
        // To use the multiline text or the table argument of the scenario,
        // additional string/DataTable parameters can be defined on the step definition
        // method. 

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

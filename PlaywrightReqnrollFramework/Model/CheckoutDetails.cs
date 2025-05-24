using System;

namespace PlaywrightReqnrollFramework.Model;

public class CheckoutDetails(string firstName, string lastName, string postalCode)
{
    public string FirstName { get; set; } = firstName;
    public string LastName { get; set; } = lastName;
    public string PostalCode { get; set; } = postalCode;
}

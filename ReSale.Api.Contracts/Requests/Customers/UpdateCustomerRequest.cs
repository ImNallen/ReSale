namespace ReSale.Api.Contracts.Requests.Customers;

public record UpdateCustomerRequest(
    string FirstName,
    string LastName,
    string ShippingStreet,
    string ShippingCity,
    string ShippingZipCode,
    string ShippingCountry,
    string? ShippingState,
    string? BillingStreet,
    string? BillingCity,
    string? BillingZipCode,
    string? BillingCountry,
    string? BillingState);

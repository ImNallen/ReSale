namespace ReSale.Api.Contracts.Requests.Customers;

public record CreateCustomerRequest(
    string Email,
    string FirstName,
    string LastName,
    string ShippingStreet,
    string ShippingCity,
    string ShippingZipCode,
    string ShippingCountry,
    string? ShippingState,
    string PhoneNumber,
    string? BillingStreet,
    string? BillingCity,
    string? BillingZipCode,
    string? BillingCountry,
    string? BillingState);

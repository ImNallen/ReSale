using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Customers.Results;

namespace ReSale.Application.Customers.Create;

public record CreateCustomerCommand(
    string Email,
    string FirstName,
    string LastName,
    string ShippingStreet,
    string ShippingCity,
    string ShippingState,
    string ShippingZipCode,
    string ShippingCountry,
    string PhoneNumber,
    string? BillingStreet,
    string? BillingCity,
    string? BillingZipCode,
    string? BillingCountry,
    string? BillingState) : ICommand<CustomerResult>;
using System.Windows.Input;
using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Customers.Results;

namespace ReSale.Application.Customers.Update;

public record UpdateCustomerCommand(
    Guid Id,
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
    string? BillingState) : ICommand<CustomerResult>;

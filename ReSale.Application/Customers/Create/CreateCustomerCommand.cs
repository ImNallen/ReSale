using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Customers.Results;

namespace ReSale.Application.Customers.Create;

public record CreateCustomerCommand(
    string Email,
    string FirstName,
    string LastName,
    string Street,
    string City,
    string State,
    string ZipCode,
    string Country,
    string PhoneNumber) : ICommand<CustomerResult>;
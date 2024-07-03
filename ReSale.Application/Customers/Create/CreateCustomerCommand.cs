using ReSale.Application.Abstractions.Messaging;

namespace ReSale.Application.Customers.Create;

public record CreateCustomerCommand(
    string Email,
    string FirstName,
    string LastName,
    string Street,
    string City,
    string State,
    string ZipCode,
    string Country) : ICommand<Guid>;
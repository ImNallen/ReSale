using System.Windows.Input;
using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Customers.Results;

namespace ReSale.Application.Customers.Update;

public record UpdateCustomerCommand(
    Guid Id,
    string FirstName,
    string LastName,
    string Street,
    string City,
    string ZipCode,
    string Country,
    string State) : ICommand<CustomerResult>;
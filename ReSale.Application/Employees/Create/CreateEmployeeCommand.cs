using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Employees.Results;

namespace ReSale.Application.Employees.Create;

public record CreateEmployeeCommand(
    string Email,
    string FirstName,
    string LastName,
    DateOnly HireDate,
    string Street,
    string City,
    string ZipCode,
    string Country,
    string? State,
    decimal Amount,
    string Currency) : ICommand<EmployeeResult>;

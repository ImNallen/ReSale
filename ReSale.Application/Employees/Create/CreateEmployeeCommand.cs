using ReSale.Application.Abstractions.Messaging;

namespace ReSale.Application.Employees.Create;

public record CreateEmployeeCommand(
    string Email,
    string FirstName,
    string LastName) : ICommand<Guid>;
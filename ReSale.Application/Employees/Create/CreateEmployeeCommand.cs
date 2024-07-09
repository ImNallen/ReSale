using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Employees.Results;

namespace ReSale.Application.Employees.Create;

public record CreateEmployeeCommand(
    string Email,
    string FirstName,
    string LastName) : ICommand<EmployeeResult>;
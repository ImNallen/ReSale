using ReSale.Application.Abstractions.Messaging;

namespace ReSale.Application.Employees.Delete;

public record DeleteEmployeeCommand(Guid Id) : ICommand<bool>;

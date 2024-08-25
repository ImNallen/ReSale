using ReSale.Application.Abstractions.Messaging;

namespace ReSale.Application.Customers.Delete;

public record DeleteCustomerCommand(Guid Id) : ICommand<bool>;

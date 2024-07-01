using ReSale.Application.Abstractions.Messaging;
using ReSale.Domain.Users;

namespace ReSale.Application.Users.Create;

public record CreateUserCommand(
    string Email,
    string Password,
    string FirstName,
    string LastName) : ICommand<Guid>;
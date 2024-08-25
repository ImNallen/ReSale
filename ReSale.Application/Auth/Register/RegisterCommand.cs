using ReSale.Application.Abstractions.Messaging;

namespace ReSale.Application.Auth.Register;

public sealed record RegisterCommand(
    string Email,
    string Password,
    string FirstName,
    string LastName) : ICommand<Guid>;

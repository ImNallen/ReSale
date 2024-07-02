using ReSale.Application.Abstractions.Messaging;

namespace ReSale.Application.Users.Login;

public sealed record LoginCommand(
    string Email,
    string Password) : ICommand<AccessTokenResponse>;
using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Users.Shared;

namespace ReSale.Application.Users.Login;

public sealed record LoginCommand(
    string Email,
    string Password) : ICommand<AccessTokenResponse>;
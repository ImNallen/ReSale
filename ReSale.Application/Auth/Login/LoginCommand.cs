using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Auth.Results;

namespace ReSale.Application.Auth.Login;

public sealed record LoginCommand(
    string Email,
    string Password) : ICommand<AccessTokenResult>;

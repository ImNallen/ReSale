using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Authentication.Results;

namespace ReSale.Application.Authentication.Login;

public sealed record LoginCommand(
    string Email,
    string Password) : ICommand<AccessTokenResult>;
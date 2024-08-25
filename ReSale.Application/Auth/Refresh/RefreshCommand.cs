using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Auth.Results;

namespace ReSale.Application.Auth.Refresh;

public record RefreshCommand(string RefreshToken) : ICommand<AccessTokenResult>;

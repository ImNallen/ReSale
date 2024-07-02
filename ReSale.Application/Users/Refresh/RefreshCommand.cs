using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Users.Shared;

namespace ReSale.Application.Users.Refresh;

public record RefreshCommand(string RefreshToken) : ICommand<AccessTokenResponse>;
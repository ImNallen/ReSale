using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Identity.Shared;

namespace ReSale.Application.Identity.Refresh;

public record RefreshCommand(string RefreshToken) : ICommand<AccessTokenResponse>;
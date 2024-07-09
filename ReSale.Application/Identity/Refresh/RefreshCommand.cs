using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Identity.Results;

namespace ReSale.Application.Identity.Refresh;

public record RefreshCommand(string RefreshToken) : ICommand<AccessTokenResult>;
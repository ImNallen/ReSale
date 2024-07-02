using ReSale.Domain.Common;

namespace ReSale.Application.Abstractions.Authentication;

public interface IRefreshService
{
    Task<Result<Token>> RefreshAccessTokenAsync(
        string refreshToken,
        CancellationToken cancellationToken = default);
}
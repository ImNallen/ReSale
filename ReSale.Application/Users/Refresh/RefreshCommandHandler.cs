using ReSale.Application.Abstractions.Authentication;
using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Users.Shared;
using ReSale.Domain.Common;
using ReSale.Domain.Users;

namespace ReSale.Application.Users.Refresh;

public class RefreshCommandHandler(
    IRefreshService refreshService) 
    : ICommandHandler<RefreshCommand, AccessTokenResponse>
{
    public async Task<Result<AccessTokenResponse>> Handle(RefreshCommand request, CancellationToken cancellationToken)
    {
        var result = await refreshService.RefreshAccessTokenAsync(
            request.RefreshToken,
            cancellationToken);
        
        if (result.IsFailure)
        {
            return Result.Failure<AccessTokenResponse>(UserErrors.InvalidCredentials);
        }
        
        return new AccessTokenResponse(
            result.Value.AccessToken, 
            result.Value.RefreshToken,
            result.Value.ExpiresIn,
            result.Value.RefreshExpiresIn,
            result.Value.TokenType,
            result.Value.IdToken,
            result.Value.NotBeforePolicy,
            result.Value.SessionState,
            result.Value.Scope);
    }
}
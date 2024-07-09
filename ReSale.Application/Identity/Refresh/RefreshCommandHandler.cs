using ReSale.Application.Abstractions.Authentication;
using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Identity.Results;
using ReSale.Domain.Common;
using ReSale.Domain.Users;

namespace ReSale.Application.Identity.Refresh;

public class RefreshCommandHandler(
    IRefreshService refreshService) 
    : ICommandHandler<RefreshCommand, AccessTokenResult>
{
    public async Task<Result<AccessTokenResult>> Handle(RefreshCommand request, CancellationToken cancellationToken)
    {
        var result = await refreshService.RefreshAccessTokenAsync(
            request.RefreshToken,
            cancellationToken);
        
        if (result.IsFailure)
        {
            return Result.Failure<AccessTokenResult>(UserErrors.InvalidCredentials);
        }
        
        return new AccessTokenResult(
            result.Value.AccessToken, 
            result.Value.RefreshToken,
            result.Value.ExpiresIn,
            result.Value.RefreshExpiresIn);
    }
}
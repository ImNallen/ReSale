using ReSale.Application.Abstractions.Authentication;
using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Identity.Shared;
using ReSale.Domain.Common;
using ReSale.Domain.Users;

namespace ReSale.Application.Identity.Login;

public class LoginCommandHandler(IJwtService jwtService) 
    : ICommandHandler<LoginCommand, AccessTokenResponse>
{
    public async Task<Result<AccessTokenResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var result = await jwtService.GetAccessTokenAsync(
            request.Email,
            request.Password,
            cancellationToken);

        if (result.IsFailure)
        {
            return Result.Failure<AccessTokenResponse>(UserErrors.InvalidCredentials);
        }
        
        return new AccessTokenResponse(
            result.Value.AccessToken, 
            result.Value.RefreshToken,
            result.Value.ExpiresIn,
            result.Value.RefreshExpiresIn);
    }
}
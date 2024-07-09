using ReSale.Application.Abstractions.Authentication;
using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Identity.Results;
using ReSale.Domain.Common;
using ReSale.Domain.Users;

namespace ReSale.Application.Identity.Login;

public class LoginCommandHandler(IJwtService jwtService) 
    : ICommandHandler<LoginCommand, AccessTokenResult>
{
    public async Task<Result<AccessTokenResult>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var result = await jwtService.GetAccessTokenAsync(
            request.Email,
            request.Password,
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
using Microsoft.EntityFrameworkCore;
using ReSale.Application.Abstractions.Authentication;
using ReSale.Application.Abstractions.Encryption;
using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Abstractions.Persistence;
using ReSale.Application.Authentication.Results;
using ReSale.Domain.Common;
using ReSale.Domain.Users;

namespace ReSale.Application.Authentication.Login;

public class LoginCommandHandler(
    IReSaleDbContext context,
    IJwtTokenGenerator tokenGenerator,
    IPasswordHasher hasher) 
    : ICommandHandler<LoginCommand, AccessTokenResult>
{
    public async Task<Result<AccessTokenResult>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await context.Users
            .Where(x => (string)x.Email == request.Email)
            .SingleOrDefaultAsync(cancellationToken);
        
        if (user is null)   
            return Result.Failure<AccessTokenResult>(UserErrors.NotFound);
        
        if(!hasher.Verify(request.Password, user.Password.Value))
            return Result.Failure<AccessTokenResult>(UserErrors.InvalidCredentials);
        
        var accessToken = tokenGenerator.GenerateToken(user);
        
        return new AccessTokenResult(accessToken);
    }
}
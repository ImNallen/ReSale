using Microsoft.EntityFrameworkCore;
using ReSale.Application.Abstractions.Authentication;
using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Abstractions.Persistence;
using ReSale.Application.Auth.Results;
using ReSale.Domain.Common;
using ReSale.Domain.Users;

namespace ReSale.Application.Auth.Refresh;

public class RefreshCommandHandler(
    IReSaleDbContext context,
    IJwtTokenGenerator tokenGenerator) : ICommandHandler<RefreshCommand, AccessTokenResult>
{
    public async Task<Result<AccessTokenResult>> Handle(
        RefreshCommand request,
        CancellationToken cancellationToken)
    {
        User? user = await context.Users
            .Where(x => x.RefreshToken == request.RefreshToken)
            .SingleOrDefaultAsync(cancellationToken);

        if (user is null)
        {
            return Result.Failure<AccessTokenResult>(UserErrors.NotFound);
        }

        AccessTokenResult tokens = tokenGenerator.GenerateToken(user);

        user.SetRefreshToken(tokens.RefreshToken);

        await context.SaveChangesAsync(cancellationToken);

        return tokens;
    }
}

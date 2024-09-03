using Microsoft.EntityFrameworkCore;
using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Abstractions.Persistence;
using ReSale.Domain.Common;
using ReSale.Domain.Shared;
using ReSale.Domain.Users;

namespace ReSale.Application.Auth.Verify;

public class VerifyCommandHandler(
    IReSaleDbContext context) : ICommandHandler<VerifyCommand>
{
    public async Task<Result> Handle(
        VerifyCommand request,
        CancellationToken cancellationToken)
    {
        User? user = await context.Users
            .Where(x => x.EmailVerificationToken == request.Token)
            .FirstOrDefaultAsync(cancellationToken);

        if (user is null)
        {
            return Result.Failure(DomainErrors.NotFound(nameof(User)));
        }

        if (user.IsEmailVerified)
        {
            return Result.Failure(DomainErrors.Verified(nameof(Email)));
        }

        user.VerifyEmail();

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

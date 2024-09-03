using Microsoft.EntityFrameworkCore;
using ReSale.Application.Abstractions.Encryption;
using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Abstractions.Persistence;
using ReSale.Domain.Common;
using ReSale.Domain.Users;

namespace ReSale.Application.Auth.Reset;

public class ResetCommandHandler(
    IReSaleDbContext context,
    IPasswordHasher passwordHasher)
    : ICommandHandler<ResetCommand>
{
    public async Task<Result> Handle(ResetCommand request, CancellationToken cancellationToken)
    {
        User? user = await context.Users.FirstOrDefaultAsync(
            x => x.PasswordResetToken == request.Token, cancellationToken);

        if (user is null)
        {
            return Result.Failure(DomainErrors.NotFound(nameof(User)));
        }

        string passwordHash = passwordHasher.Hash(request.Password);

        Result<Password> passwordResult = Password.Create(passwordHash);

        if (passwordResult.IsFailure)
        {
            return Result.Failure(passwordResult.Error);
        }

        user.ResetPassword(passwordResult.Value);

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

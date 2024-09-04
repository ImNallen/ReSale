using Microsoft.EntityFrameworkCore;
using ReSale.Application.Abstractions.Encryption;
using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Abstractions.Persistence;
using ReSale.Application.Abstractions.Services;
using ReSale.Domain.Common;
using ReSale.Domain.Users;

namespace ReSale.Application.Auth.Reset;

public class ResetCommandHandler(
    IReSaleDbContext context,
    IPasswordHasher passwordHasher,
    IEmailService emailService)
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

        await emailService.SendAsync(
            user.Email.Value,
            "Password has been reset",
            string.Join(
                Environment.NewLine,
                "Your password has been reset successfully.",
                "<br/><br/>",
                "If you did not request this change, please contact us immediately.",
                "<br/><br/>",
                "Best regards,",
                "<br/><br/>",
                "The ReSale Team"));

        return Result.Success();
    }
}

using Microsoft.EntityFrameworkCore;
using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Abstractions.Persistence;
using ReSale.Application.Abstractions.Services;
using ReSale.Domain.Common;
using ReSale.Domain.Shared;
using ReSale.Domain.Users;

namespace ReSale.Application.Auth.Forgot;

public class ForgotPasswordCommandHandler(
    IReSaleDbContext context,
    IEmailService emailService) : ICommandHandler<ForgotPasswordCommand>
{
    public async Task<Result> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        User? user = await context.Users
            .FirstOrDefaultAsync(x => (string)x.Email == request.Email, cancellationToken);

        if (user is null)
        {
            return Result.Failure(DomainErrors.NotFound(nameof(User)));
        }

        user.SetPasswordResetToken(Guid.NewGuid(), DateTime.UtcNow.AddDays(1));

        await context.SaveChangesAsync(cancellationToken);

        await emailService.SendAsync(
            user.Email.Value,
            "Reset Your Password",
            string.Join(
                Environment.NewLine,
                $"Dear {user.FirstName.Value},",
                "<br/><br/>",
                "You have requested to reset your password. Please click the link below to reset your password:",
                "<br/><br/>",
                $"<a href=\"https://localhost:5001/api/v1/auth/reset/{user.PasswordResetToken}\">Reset Password</a>",
                "<br/><br/>",
                "This link will expire in 24 hours.",
                "<br/><br/>",
                "If you did not request a password reset, please ignore this email.",
                "<br/><br/>",
                "Best regards,",
                "<br/><br/>",
                "The ReSale Team"));

        return Result.Success();
    }
}

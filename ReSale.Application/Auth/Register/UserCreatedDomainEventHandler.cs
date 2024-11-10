using MediatR;
using ReSale.Application.Abstractions.Persistence;
using ReSale.Application.Abstractions.Services;
using ReSale.Domain.Activities;
using ReSale.Domain.Shared;
using ReSale.Domain.Users.Events;

namespace ReSale.Application.Auth.Register;

internal sealed class UserCreatedDomainEventHandler(
    IEmailService emailService,
    IReSaleDbContext context)
    : INotificationHandler<UserCreatedDomainEvent>
{
    public async Task Handle(
        UserCreatedDomainEvent notification,
        CancellationToken cancellationToken)
        {
            await emailService.SendAsync(
            notification.User.Email.Value,
            "Welcome to ReSale",
            string.Join(
                Environment.NewLine,
                $"We welcome you {notification.User.FirstName.Value} to ReSale!",
                "<br/><br/>",
                "Your account has been created successfully, please click the link below to verify your email address:",
                "<br/><br/>",
                $"<a href=\"https://localhost:5003/verify/{notification.User.EmailVerificationToken}\">Click here</a>",
                "<br/><br/>",
                "Best regards,",
                "<br/><br/>",
                "The ReSale Team"));

            var description = new Description($"User '{notification.User.Email.Value}' created");

            var activity = Activity.Create(description, ActivityType.UserCreated);

            await context.Activities.AddAsync(activity, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);
    }
}

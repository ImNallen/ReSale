using Microsoft.EntityFrameworkCore;
using ReSale.Application.Abstractions.Authentication;
using ReSale.Application.Abstractions.Encryption;
using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Abstractions.Persistence;
using ReSale.Domain.Common;
using ReSale.Domain.Shared;
using ReSale.Domain.Users;

namespace ReSale.Application.Auth.Register;

internal sealed class RegisterCommandHandler(
    IReSaleDbContext context,
    IPasswordHasher hasher)
    : ICommandHandler<RegisterCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
        RegisterCommand request,
        CancellationToken cancellationToken)
    {
        Result<Email> emailResult = Email.Create(request.Email);
        Result<FirstName> firstNameResult = FirstName.Create(request.FirstName);
        Result<LastName> lastNameResult = LastName.Create(request.LastName);
        string hashedPassword = hasher.Hash(request.Password);
        Result<Password> passwordResult = Password.Create(hashedPassword);

        if (emailResult.IsFailure)
        {
            return Result.Failure<Guid>(emailResult.Error);
        }

        if (firstNameResult.IsFailure)
        {
            return Result.Failure<Guid>(firstNameResult.Error);
        }

        if (lastNameResult.IsFailure)
        {
            return Result.Failure<Guid>(lastNameResult.Error);
        }

        if (passwordResult.IsFailure)
        {
            return Result.Failure<Guid>(passwordResult.Error);
        }

        bool emailExists = await context.Users
            .AnyAsync(u => u.Email == emailResult.Value, cancellationToken);

        if (emailExists)
        {
            return Result.Failure<Guid>(DomainErrors.NotUnique(nameof(Email)));
        }

        var user = User.Create(
            emailResult.Value,
            passwordResult.Value,
            firstNameResult.Value,
            lastNameResult.Value,
            Guid.NewGuid());

        await context.Users.AddAsync(user, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}

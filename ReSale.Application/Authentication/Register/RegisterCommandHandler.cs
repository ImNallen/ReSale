using Microsoft.EntityFrameworkCore;
using ReSale.Application.Abstractions.Encryption;
using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Abstractions.Persistence;
using ReSale.Domain.Common;
using ReSale.Domain.Shared;
using ReSale.Domain.Users;

namespace ReSale.Application.Authentication.Register;

internal sealed class RegisterCommandHandler(
    IReSaleDbContext context,
    IPasswordHasher hasher)
    : ICommandHandler<RegisterCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
        RegisterCommand request, 
        CancellationToken cancellationToken)
    {
        var emailResult = Email.Create(request.Email);
        var firstNameResult = FirstName.Create(request.FirstName);
        var lastNameResult = LastName.Create(request.LastName);
        
        var hashedPassword = hasher.Hash(request.Password);
        var passwordResult = Password.Create(hashedPassword);
        
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
        
        var emailExists = await context.Users
            .AnyAsync(u => u.Email == emailResult.Value, cancellationToken);
        
        if (emailExists)
        {
            return Result.Failure<Guid>(EmailErrors.NotUnique);
        }
        
        var user = User.Create(
            emailResult.Value,
            passwordResult.Value,
            firstNameResult.Value,
            lastNameResult.Value);

        await context.Users.AddAsync(user, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
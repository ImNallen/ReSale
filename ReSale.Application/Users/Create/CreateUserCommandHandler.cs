using Microsoft.Extensions.Logging;
using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Abstractions.Persistence;
using ReSale.Application.Abstractions.Persistence.Repositories;
using ReSale.Domain.Common;
using ReSale.Domain.Shared;
using ReSale.Domain.Users;

namespace ReSale.Application.Users.Create;

public class CreateUserCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    ILogger<CreateUserCommandHandler> logger) 
    : ICommandHandler<CreateUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating email");
        
        var emailResult = Email.Create(request.Email);
        if (emailResult.IsFailure)
        {
            return Result.Failure<Guid>(emailResult.Error);
        }
        
        logger.LogInformation("Checking if email is unique");
        
        if (!await userRepository.IsEmailUniqueAsync(emailResult.Value))
        {
            return Result.Failure<Guid>(EmailErrors.NotUnique);
        }
        
        logger.LogInformation("Creating password");

        var passwordResult = Password.Create(request.Password);
        if (passwordResult.IsFailure)
        {
            return Result.Failure<Guid>(passwordResult.Error);
        }
        
        logger.LogInformation("Creating user");
        
        var firstName = new FirstName(request.FirstName);
        var lastName = new LastName(request.LastName);
        
        var user = User.Create(emailResult.Value, passwordResult.Value, firstName, lastName);
        
        logger.LogInformation("Saving user");
        
        await userRepository.CreateAsync(user, cancellationToken);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return user.Id;
    }
}
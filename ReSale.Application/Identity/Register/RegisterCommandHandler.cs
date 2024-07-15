using ReSale.Application.Abstractions.Authentication;
using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Abstractions.Persistence;
using ReSale.Domain.Common;
using ReSale.Domain.Shared;
using ReSale.Domain.Users;

namespace ReSale.Application.Identity.Register;

internal sealed class RegisterCommandHandler(
    IAuthenticationService authenticationService,
    IUnitOfWork unitOfWork)
    : ICommandHandler<RegisterCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
        RegisterCommand request, 
        CancellationToken cancellationToken)
    {
        var emailResult = Email.Create(request.Email);
        var firstNameResult = FirstName.Create(request.FirstName);
        var lastNameResult = LastName.Create(request.LastName);
        
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
        
        var isEmailUnique = await unitOfWork.Users.IsEmailUniqueAsync(emailResult.Value);
        
        if (!isEmailUnique)
        {
            return Result.Failure<Guid>(EmailErrors.NotUnique);
        }
        
        var user = User.Create(
            emailResult.Value,
            firstNameResult.Value,
            lastNameResult.Value);
        
        var identityId = await authenticationService.RegisterAsync(
            user,
            request.Password,
            cancellationToken);
        
        user.SetIdentityId(identityId);

        await unitOfWork.Users.AddAsync(user, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
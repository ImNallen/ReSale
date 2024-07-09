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
        var email = Email.Create(request.Email);
        
        if (email.IsFailure)
        {
            return Result.Failure<Guid>(email.Error);
        }
        
        var isEmailUnique = await unitOfWork.Users.IsEmailUniqueAsync(email.Value);
        
        if (!isEmailUnique)
        {
            return Result.Failure<Guid>(EmailErrors.NotUnique);
        }
        
        var user = User.Create(
            email.Value,
            new FirstName(request.FirstName),
            new LastName(request.LastName));
        
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
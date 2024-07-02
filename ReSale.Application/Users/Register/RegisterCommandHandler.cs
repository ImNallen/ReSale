using ReSale.Application.Abstractions.Authentication;
using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Abstractions.Persistence;
using ReSale.Application.Abstractions.Persistence.Repositories;
using ReSale.Domain.Common;
using ReSale.Domain.Shared;
using ReSale.Domain.Users;

namespace ReSale.Application.Users.Register;

internal sealed class RegisterCommandHandler(
    IAuthenticationService authenticationService,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<RegisterCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
        RegisterCommand request, 
        CancellationToken cancellationToken)
    {
        var email = Email.Create(request.Email);

        // TODO: return error if email is not valid
        
        var existingUser = await userRepository.IsEmailUniqueAsync(email.Value);
        
        // TODO: Return error if email is not unique
        
        var user = User.Create(
            email.Value,
            new FirstName(request.FirstName),
            new LastName(request.LastName));
        
        var identityId = await authenticationService.RegisterAsync(
            user,
            request.Password,
            cancellationToken);
        
        user.SetIdentityId(identityId);

        await userRepository.AddAsync(user, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
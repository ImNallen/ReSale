using ReSale.Domain.Common;
using ReSale.Domain.Shared;
using ReSale.Domain.Users.Events;

namespace ReSale.Domain.Users;

public sealed class User : Entity
{
    public Email Email { get; private set; }
    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public string IdentityId { get; private set; } = string.Empty;
    
    private User(
        Guid id, 
        Email email, 
        FirstName firstName, 
        LastName lastName)
        : base(id)
    {
        Email = email;
        FirstName = firstName;
        LastName = lastName;
    }

    public static User Create(
        Email email, 
        FirstName firstName, 
        LastName lastName)
    {
        var user = new User(
            Guid.NewGuid(), 
            email, 
            firstName, 
            lastName);
        
        user.Raise(new UserCreatedDomainEvent(user.Id));

        return user;
    }
    
    public void SetIdentityId(string identityId)
    {
        IdentityId = identityId;
    }
}
using ReSale.Domain.Common;
using ReSale.Domain.Shared;

namespace ReSale.Domain.Users;

public sealed class User : Entity
{
    public Email Email { get; private set; }
    public Password Password { get; private set; }
    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    
    private User(
        Guid id, 
        Email email, 
        Password password, 
        FirstName firstName, 
        LastName lastName)
        : base(id)
    {
        Email = email;
        Password = password;
        FirstName = firstName;
        LastName = lastName;
    }

    public static User Create(
        Email email, 
        Password password, 
        FirstName firstName, 
        LastName lastName)
    {
        var user = new User(
            Guid.NewGuid(), 
            email, 
            password, 
            firstName, 
            lastName);

        return user;
    }
}
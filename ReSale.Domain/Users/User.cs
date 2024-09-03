using ReSale.Domain.Common;
using ReSale.Domain.Shared;
using ReSale.Domain.Users.Events;

namespace ReSale.Domain.Users;

public sealed class User : Entity
{
    private User(
        Guid id,
        Email email,
        Password password,
        FirstName firstName,
        LastName lastName,
        bool isEmailVerified,
        Guid emailVerificationToken)
        : base(id)
    {
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        Password = password;
        IsEmailVerified = isEmailVerified;
        EmailVerificationToken = emailVerificationToken;
    }

    public Email Email { get; private set; }

    public Password Password { get; private set; }

    public FirstName FirstName { get; private set; }

    public LastName LastName { get; private set; }

    public bool IsEmailVerified { get; private set; }

    public Guid EmailVerificationToken { get; private set; }

    public Guid? PasswordResetToken { get; private set; }

    public DateTime? PasswordResetTokenExpires { get; private set; }

    public string? RefreshToken { get; private set; }

    public static User Create(
        Email email,
        Password password,
        FirstName firstName,
        LastName lastName,
        Guid emailVerificationToken,
        bool isEmailVerified = false)
    {
        var user = new User(
            Guid.NewGuid(),
            email,
            password,
            firstName,
            lastName,
            isEmailVerified,
            emailVerificationToken);

        user.RaiseDomainEvent(new UserCreatedDomainEvent(user));

        return user;
    }

    public User SetRefreshToken(string refreshToken)
    {
        RefreshToken = refreshToken;

        return this;
    }

    public User SetPasswordResetToken(Guid passwordResetToken, DateTime expires)
    {
        PasswordResetToken = passwordResetToken;
        PasswordResetTokenExpires = expires;

        return this;
    }

    public User VerifyEmail()
    {
        IsEmailVerified = true;

        return this;
    }

    public User ResetPassword(Password password)
    {
        Password = password;

        return this;
    }
}

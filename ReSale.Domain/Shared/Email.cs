using ReSale.Domain.Common;

namespace ReSale.Domain.Shared;

public sealed record Email
{
    private Email(string value) => Value = value;

    public string Value { get; }
    
    public static explicit operator string(Email email) => email.Value;

    public static Result<Email> Create(string? email)
    {
        if (string.IsNullOrEmpty(email))
        {
            return Result.Failure<Email>(EmailErrors.Empty);
        }

        if (email.Split('@').Length != 2)
        {
            return Result.Failure<Email>(EmailErrors.InvalidFormat);
        }

        return new Email(email);
    }
}

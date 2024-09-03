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
            return Result.Failure<Email>(DomainErrors.Empty(nameof(Email)));
        }

        email = email.ToLower(System.Globalization.CultureInfo.CurrentCulture);

        if (email.Split('@').Length != 2)
        {
            return Result.Failure<Email>(DomainErrors.InvalidFormat(nameof(Email)));
        }

        return new Email(email);
    }
}

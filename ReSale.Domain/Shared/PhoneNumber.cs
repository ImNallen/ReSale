using System.Text.RegularExpressions;
using ReSale.Domain.Common;

namespace ReSale.Domain.Shared;

public sealed record PhoneNumber
{
    private PhoneNumber(string value) => Value = value;

    public string Value { get; }

    public static explicit operator string(PhoneNumber phoneNumber) => phoneNumber.Value;

    public static Result<PhoneNumber> Create(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return Result.Failure<PhoneNumber>(PhoneNumberErrors.Empty);
        }

        if (!IsValidPhoneNumber(value))
        {
            return Result.Failure<PhoneNumber>(PhoneNumberErrors.NotValid);
        }

        return new PhoneNumber(value);
    }

    private static bool IsValidPhoneNumber(string phoneNumber)
    {
        var regex = new Regex(@"^(\+\d{1,2}\s?)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$");
        return regex.IsMatch(phoneNumber);
    }
}

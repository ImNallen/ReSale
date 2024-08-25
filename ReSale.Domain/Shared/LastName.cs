using System.Globalization;
using ReSale.Domain.Common;

namespace ReSale.Domain.Shared;

public sealed record LastName
{
    private LastName(string value) => Value = value;

    public string Value { get; }

    public static explicit operator string(LastName lastName) => lastName.Value;

    public static Result<LastName> Create(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return Result.Failure<LastName>(LastNameErrors.Empty);
        }

        value = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower(CultureInfo.CurrentCulture));

        return new LastName(value);
    }
}

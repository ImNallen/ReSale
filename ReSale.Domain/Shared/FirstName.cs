using System.Globalization;
using ReSale.Domain.Common;

namespace ReSale.Domain.Shared;

public sealed record FirstName
{
    private FirstName(string value) => Value = value;

    public string Value { get; }

    public static explicit operator string(FirstName firstName) => firstName.Value;

    public static Result<FirstName> Create(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return Result.Failure<FirstName>(FirstNameErrors.Empty);
        }

        value = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower(CultureInfo.CurrentCulture));

        return new FirstName(value);
    }
}

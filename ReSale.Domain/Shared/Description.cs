namespace ReSale.Domain.Shared;

public record Description(string Value)
{
    public static explicit operator string(Description description) => description.Value;
}

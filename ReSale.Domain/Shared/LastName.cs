namespace ReSale.Domain.Shared;

public sealed record LastName
{
    public LastName(string value) => Value = value;

    public string Value { get; }
    
    public static explicit operator string(LastName lastName) => lastName.Value;
}
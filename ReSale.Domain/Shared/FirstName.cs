namespace ReSale.Domain.Shared;

public sealed record FirstName
{
    public FirstName(string value) => Value = value;

    public string Value { get; }
    
    public static explicit operator string(FirstName firstName) => firstName.Value;
}
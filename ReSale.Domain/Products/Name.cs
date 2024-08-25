namespace ReSale.Domain.Products;

public record Name(string Value)
{
    public static explicit operator string(Name name) => name.Value;
}

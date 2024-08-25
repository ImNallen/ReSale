namespace ReSale.Domain.Shared;

public sealed record Currency
{
    internal static readonly Currency None = new(string.Empty);
    internal static readonly Currency Usd = new("USD");
    internal static readonly Currency Eur = new("EUR");
    internal static readonly Currency Gbp = new("GBP");
    internal static readonly Currency Sek = new("SEK");
    internal static readonly Currency Nok = new("NOK");

    private static readonly IReadOnlyCollection<Currency> All = new[]
    {
        Usd,
        Eur,
        Gbp,
        Sek,
        Nok
    };

    private Currency(string code) => Code = code;

    public string Code { get; init; }

    public static Currency FromCode(string code) => All.FirstOrDefault(c => c.Code.Equals(code, StringComparison.OrdinalIgnoreCase)) ??
            throw new CurrencyCodeException("The currency code is invalid");
}

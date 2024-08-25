namespace ReSale.Domain.Shared;

public class CurrencyCodeException : Exception
{
    public CurrencyCodeException()
    {
    }

    public CurrencyCodeException(string message)
        : base(message)
    {
    }

    public CurrencyCodeException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}

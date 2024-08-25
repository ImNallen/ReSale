namespace ReSale.Domain.Common;

public interface IDateTimeProvider
{
    public DateTime UtcNow { get; }

    public DateOnly Today { get; }
}

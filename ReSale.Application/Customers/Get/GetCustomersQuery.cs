using ReSale.Application.Abstractions.Caching;
using ReSale.Application.Customers.Results;

namespace ReSale.Application.Customers.Get;

public record GetCustomersQuery() : ICachedQuery<List<CustomerResult>>
{
    public string CacheKey => $"customers";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(2);
}
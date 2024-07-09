using ReSale.Application.Abstractions.Caching;
using ReSale.Application.Customers.Shared;

namespace ReSale.Application.Customers.Get;

public record GetCustomersQuery() : ICachedQuery<List<CustomerResponse>>
{
    public string CacheKey => $"customers";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(2);
}
using ReSale.Application.Abstractions.Caching;
using ReSale.Application.Customers.Results;

namespace ReSale.Application.Customers.GetByEmail;

public sealed record GetCustomerByEmailQuery(string Email) : ICachedQuery<CustomerResult>
{
    public string CacheKey => $"customer-by-id-{Email}";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(2);
}
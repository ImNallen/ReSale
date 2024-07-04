using ReSale.Application.Abstractions.Caching;
using ReSale.Application.Customers.Shared;

namespace ReSale.Application.Customers.GetByEmail;

public sealed record GetCustomerByEmailQuery(string Email) : ICachedQuery<CustomerResponse>
{
    public string CacheKey => $"customer-by-id-{Email}";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(2);
}
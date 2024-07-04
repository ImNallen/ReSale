using ReSale.Application.Abstractions.Caching;
using ReSale.Application.Customers.Shared;

namespace ReSale.Application.Customers.GetById;

public sealed record GetCustomerByIdQuery(Guid Id) : ICachedQuery<CustomerResponse>
{
    public string CacheKey => $"customer-by-id-{Id}";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(2);
}
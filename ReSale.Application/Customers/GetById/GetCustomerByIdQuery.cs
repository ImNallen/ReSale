using ReSale.Application.Abstractions.Caching;
using ReSale.Application.Customers.Results;

namespace ReSale.Application.Customers.GetById;

public sealed record GetCustomerByIdQuery(Guid Id) : ICachedQuery<CustomerResult>
{
    public string CacheKey => $"customer-by-id-{Id}";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(0.2);
}

using ReSale.Application.Abstractions.Caching;
using ReSale.Application.Products.Results;

namespace ReSale.Application.Products.GetById;

public record GetProductByIdQuery(Guid Id)
    : ICachedQuery<ProductResult>
{
    public string CacheKey => $"product-by-id-{Id}";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(0.1);
}

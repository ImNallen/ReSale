using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Products.Results;
using ReSale.Domain.Common;

namespace ReSale.Application.Products.Get;

public record GetProductsQuery(
    string? SearchTerm,
    string? SortColumn,
    string? SortOrder,
    int Page,
    int PageSize) : IQuery<PagedList<ProductResult>>;

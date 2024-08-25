using System.Globalization;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Abstractions.Persistence;
using ReSale.Application.Products.Results;
using ReSale.Domain.Common;
using ReSale.Domain.Products;

namespace ReSale.Application.Products.Get;

public class GetProductsQueryHandler(
    IReSaleDbContext context)
    : IQueryHandler<GetProductsQuery, PagedList<ProductResult>>
{
    public async Task<Result<PagedList<ProductResult>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Product> productsQuery = context.Products.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            productsQuery = productsQuery
                .Where(p =>
                    ((string)p.Name).Contains(request.SearchTerm) ||
                    ((string)p.Description).Contains(request.SearchTerm));
        }

        Expression<Func<Product, object>> keySelector = request.SortColumn?.ToLower(CultureInfo.CurrentCulture) switch
        {
            "name" => p => p.Name,
            "description" => p => p.Description,
            _ => p => p.Id
        };

        productsQuery = request.SortColumn?.ToLower(CultureInfo.CurrentCulture) == "desc"
            ? productsQuery.OrderByDescending(keySelector)
            : productsQuery.OrderBy(keySelector);

        int totalCount = await productsQuery.CountAsync(cancellationToken);

        List<ProductResult> products = await productsQuery
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(p => new ProductResult(
                p.Id,
                p.Name.Value,
                p.Description.Value,
                p.Price.Amount,
                p.Price.Currency.Code))
            .ToListAsync(cancellationToken);

        return PagedList<ProductResult>.Create(products, request.Page, request.PageSize, totalCount);
    }
}

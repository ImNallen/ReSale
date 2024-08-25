using MapsterMapper;
using MediatR;
using ReSale.Api.Contracts.Responses.Products;
using ReSale.Api.Infrastructure;
using ReSale.Application.Products.Get;
using ReSale.Application.Products.Results;
using ReSale.Domain.Common;

namespace ReSale.Api.Endpoints.Products;

public class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapGet("/products", async (
            string? searchTerm,
            string? sortColumn,
            string? sortOrder,
            int page,
            int pageSize,
            ISender sender,
            IMapper mapper,
            CancellationToken cancellationToken) =>
        {
            var query = new GetProductsQuery(
                searchTerm,
                sortColumn,
                sortOrder,
                page,
                pageSize);

            Result<PagedList<ProductResult>> result = await sender.Send(query, cancellationToken);

            if (result.IsFailure)
            {
                return CustomResults.Problem(result);
            }

            return Results.Ok(PagedList<ProductResponse>.Create(
                result.Value.Items.Select(mapper.Map<ProductResponse>).ToList(),
                result.Value.Page,
                result.Value.PageSize,
                result.Value.TotalCount));
        })
        .WithTags(Tags.Products)
        .WithDescription("Get a paginated list of products with the possibility of searching and sorting.")
        .WithName("Get Products")
        .WithSummary("Get Products")
        .Produces(StatusCodes.Status200OK, typeof(PagedList<ProductResponse>))
        .RequireAuthorization();
}

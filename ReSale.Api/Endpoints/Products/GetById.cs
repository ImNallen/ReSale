using MapsterMapper;
using MediatR;
using ReSale.Api.Contracts.Responses.Products;
using ReSale.Api.Infrastructure;
using ReSale.Application.Products.GetById;
using ReSale.Application.Products.Results;
using ReSale.Domain.Common;

namespace ReSale.Api.Endpoints.Products;

public class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapGet("/products/{id:guid}", async (
            Guid id,
            ISender sender,
            IMapper mapper,
            CancellationToken cancellationToken) =>
        {
            var query = new GetProductByIdQuery(id);

            Result<ProductResult> result = await sender.Send(query, cancellationToken);

            if (result.IsFailure)
            {
                return CustomResults.Problem(result);
            }

            return Results.Ok(mapper.Map<ProductResponse>(result.Value));
        }).WithTags(Tags.Products)
          .WithDescription("Retrieves a product by ID.")
          .WithName("Get Product By Id")
          .WithSummary("Get Product By Id")
          .Produces(StatusCodes.Status200OK, typeof(ProductResponse))
          .Produces(StatusCodes.Status404NotFound)
          .RequireAuthorization();
}

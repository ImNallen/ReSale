using MapsterMapper;
using MediatR;
using ReSale.Api.Contracts.Requests.Products;
using ReSale.Api.Contracts.Responses.Products;
using ReSale.Api.Infrastructure;
using ReSale.Application.Products.Create;
using ReSale.Application.Products.Results;
using ReSale.Domain.Common;

namespace ReSale.Api.Endpoints.Products;

public class Create : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapPost("/products", async (
                CreateProductRequest request,
                ISender sender,
                IMapper mapper,
                CancellationToken cancellationToken) =>
            {
                var command = new CreateProductCommand(
                    request.Name,
                    request.Description,
                    request.Price,
                    request.Currency);

                Result<ProductResult> result = await sender.Send(command, cancellationToken);

                if (result.IsFailure)
                {
                    return CustomResults.Problem(result);
                }

                return Results.Ok(mapper.Map<ProductResponse>(result.Value));
            })
            .WithTags(Tags.Products)
            .WithDescription("Creates a new product.")
            .WithName("Create Product")
            .WithSummary("Create Product")
            .Produces(StatusCodes.Status200OK, typeof(ProductResponse))
            .Produces(StatusCodes.Status400BadRequest)
            .RequireAuthorization();
}

using MapsterMapper;
using MediatR;
using ReSale.Api.Contracts.Responses.Customers;
using ReSale.Api.Infrastructure;
using ReSale.Application.Customers.GetById;
using ReSale.Application.Customers.Results;
using ReSale.Domain.Common;

namespace ReSale.Api.Endpoints.Customers;

public class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapGet("/customers/{id:guid}", async (
            Guid id,
            ISender sender,
            IMapper mapper,
            CancellationToken cancellationToken) =>
        {
            var query = new GetCustomerByIdQuery(id);

            Result<CustomerResult> result = await sender.Send(query, cancellationToken);

            if (result.IsFailure)
            {
                return CustomResults.Problem(result);
            }

            return Results.Ok(mapper.Map<CustomerResponse>(result.Value));
        })
        .WithTags(Tags.Customers)
        .WithDescription("Retrieves a customer by ID.")
        .WithName("Get Customer By Id")
        .WithSummary("Get Customer By Id")
        .Produces(StatusCodes.Status200OK, typeof(CustomerResponse))
        .Produces(StatusCodes.Status404NotFound)
        .RequireAuthorization();
}

using MediatR;
using ReSale.Api.Extensions;
using ReSale.Api.Infrastructure;
using ReSale.Application.Customers.GetById;
using ReSale.Application.Customers.Shared;

namespace ReSale.Api.Endpoints.Customers;

public class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("customers/{id:guid}", async (
            Guid id,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var query = new GetCustomerByIdQuery(id);
            
            var result = await sender.Send(query, cancellationToken);
            
            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Customers)
        .WithDescription("Retrieves a customer by ID.")
        .WithName("Get Customer By Id")
        .Produces(StatusCodes.Status200OK, typeof(CustomerResponse))
        .RequireAuthorization();
    }
}
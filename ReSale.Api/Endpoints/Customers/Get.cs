using MediatR;
using ReSale.Api.Extensions;
using ReSale.Api.Infrastructure;
using ReSale.Application.Customers.Get;
using ReSale.Application.Customers.Shared;

namespace ReSale.Api.Endpoints.Customers;

public class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/customers", async (
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var query = new GetCustomersQuery();
            
            var result = await sender.Send(query, cancellationToken);
            
            return result.Match(Results.Ok, CustomResults.Problem);
        }).WithTags(Tags.Customers)
        .WithDescription("Retrieves a list of all customers. Note: Might be a long list.")
        .WithName("Get Customers")
        .Produces(StatusCodes.Status200OK, typeof(List<CustomerResponse>))
        .RequireAuthorization();
    }
}
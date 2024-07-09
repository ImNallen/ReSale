using MediatR;
using ReSale.Api.Contracts.Responses;
using ReSale.Api.Extensions;
using ReSale.Api.Infrastructure;
using ReSale.Application.Customers.Get;

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
            
            if (result.IsFailure)
                return CustomResults.Problem(result);

            // TODO: Add Mapper
            return Results.Ok(result.Value.Select(
                c => new CustomerResponse(
                c.Id,
                c.Email,
                c.FirstName,
                c.LastName,
                c.Street,
                c.City,
                c.ZipCode,
                c.Country,
                c.State)).ToList());
        }).WithTags(Tags.Customers)
        .WithDescription("Retrieves a list of all customers.")
        .WithName("Get Customers")
        .Produces(StatusCodes.Status200OK, typeof(List<CustomerResponse>))
        .RequireAuthorization();
    }
}
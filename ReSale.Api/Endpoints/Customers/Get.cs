using MapsterMapper;
using MediatR;
using ReSale.Api.Contracts.Responses;
using ReSale.Api.Contracts.Responses.Customers;
using ReSale.Api.Infrastructure;
using ReSale.Application.Customers.Get;
using Swashbuckle.AspNetCore.Annotations;

namespace ReSale.Api.Endpoints.Customers;

public class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/customers", async (
            ISender sender,
            IMapper mapper,
            CancellationToken cancellationToken) =>
        {
            var query = new GetCustomersQuery();
            
            var result = await sender.Send(query, cancellationToken);
            
            if (result.IsFailure)
                return CustomResults.Problem(result);
            
            return Results.Ok(result.Value.Select(mapper.Map<CustomerResponse>).ToList());
        })
        .WithTags(Tags.Customers)
        .WithDescription("Retrieves a list of all customers.")
        .WithName("Get Customers")
        .WithSummary("Get Customers")
        .Produces(StatusCodes.Status200OK, typeof(List<CustomerResponse>))
        .RequireAuthorization();
    }
}
using MediatR;
using ReSale.Api.Extensions;
using ReSale.Api.Infrastructure;
using ReSale.Application.Customers.GetByEmail;

namespace ReSale.Api.Endpoints.Customers;

public class GetByEmail : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("customers/{email}", async (
                string email,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var query = new GetCustomerByEmailQuery(email);
            
                var result = await sender.Send(query, cancellationToken);
            
                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .WithTags(Tags.Customers)
            .RequireAuthorization();
    }
}
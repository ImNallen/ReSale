using MediatR;
using ReSale.Api.Contracts.Requests.Customers;
using ReSale.Api.Extensions;
using ReSale.Api.Infrastructure;
using ReSale.Application.Customers.Create;

namespace ReSale.Api.Endpoints.Customers;

public class Create : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("customers", async (
                CreateCustomerRequest request,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var command = new CreateCustomerCommand(
                    request.Email,
                    request.FirstName,
                    request.LastName,
                    request.Street,
                    request.City,
                    request.State,
                    request.ZipCode,
                    request.Country);
        
                var result = await sender.Send(command, cancellationToken);
        
                return result.Match(Results.Ok, CustomResults.Problem);
            }).WithTags(Tags.Customers)
            .RequireAuthorization();
    }
}
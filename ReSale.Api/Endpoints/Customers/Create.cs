using MediatR;
using ReSale.Api.Contracts.Requests.Customers;
using ReSale.Api.Contracts.Responses;
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

                if (result.IsFailure)
                    return CustomResults.Problem(result);
        
                // TODO: Add Mapper
                return Results.Ok(new CustomerResponse(
                    result.Value.Id,
                    result.Value.Email,
                    result.Value.FirstName,
                    result.Value.LastName,
                    result.Value.Street,
                    result.Value.City,
                    result.Value.ZipCode,
                    result.Value.Country,
                    result.Value.State));
            }).WithTags(Tags.Customers)
            .WithDescription("Creates a new customer.")
            .WithName("Create Customer")
            .Produces(StatusCodes.Status200OK, typeof(CustomerResponse))
            .Produces(StatusCodes.Status400BadRequest)
            .RequireAuthorization();
    }
}
using MediatR;
using ReSale.Api.Contracts.Responses;
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
            })
            .WithTags(Tags.Customers)
            .WithDescription("Retrieves a customer by email address.")
            .WithName("Get Customer By Email")
            .Produces(StatusCodes.Status200OK, typeof(CustomerResponse))
            .RequireAuthorization();
    }
}
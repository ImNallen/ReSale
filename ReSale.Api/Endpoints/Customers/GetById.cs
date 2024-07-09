using MediatR;
using ReSale.Api.Contracts.Responses;
using ReSale.Api.Extensions;
using ReSale.Api.Infrastructure;
using ReSale.Application.Customers.GetById;

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
        .WithDescription("Retrieves a customer by ID.")
        .WithName("Get Customer By Id")
        .Produces(StatusCodes.Status200OK, typeof(CustomerResponse))
        .RequireAuthorization();
    }
}
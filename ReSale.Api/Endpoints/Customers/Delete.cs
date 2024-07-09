using MediatR;
using ReSale.Api.Infrastructure;
using ReSale.Application.Customers.Delete;

namespace ReSale.Api.Endpoints.Customers;

public class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/customers/{id:guid}", async (
            Guid id,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new DeleteCustomerCommand(id);
            
            var result = await sender.Send(command, cancellationToken);
            
            return result.IsFailure 
                ? CustomResults.Problem(result) 
                : Results.NoContent();
        })
        .WithTags(Tags.Customers)
        .WithDescription("Deletes a customer by id")
        .WithName("Delete Customer")
        .WithSummary("Delete Customer")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .RequireAuthorization();
    }
}
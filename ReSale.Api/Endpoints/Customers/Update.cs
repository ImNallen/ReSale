using MapsterMapper;
using MediatR;
using ReSale.Api.Contracts.Requests.Customers;
using ReSale.Api.Contracts.Responses.Customers;
using ReSale.Api.Infrastructure;
using ReSale.Application.Customers.Results;
using ReSale.Application.Customers.Update;
using ReSale.Domain.Common;

namespace ReSale.Api.Endpoints.Customers;

public class Update : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapPut("/customers/{id:guid}", async (
        Guid id,
        UpdateCustomerRequest request,
        ISender sender,
        IMapper mapper,
        CancellationToken cancellationToken) =>
        {
            var command = new UpdateCustomerCommand(
                id,
                request.FirstName,
                request.LastName,
                request.ShippingStreet,
                request.ShippingCity,
                request.ShippingZipCode,
                request.ShippingCountry,
                request.ShippingState,
                request.BillingStreet,
                request.BillingCity,
                request.BillingZipCode,
                request.BillingCountry,
                request.BillingState);

            Result<CustomerResult> result = await sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return CustomResults.Problem(result);
            }

            return Results.Ok(mapper.Map<CustomerResponse>(result.Value));
        })
        .WithTags(Tags.Customers)
        .WithDescription("Updates a customer.")
        .WithName("Update Customer")
        .WithSummary("Update Customer")
        .Produces(StatusCodes.Status200OK, typeof(CustomerResponse))
        .Produces(StatusCodes.Status404NotFound)
        .RequireAuthorization();
}

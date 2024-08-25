using MapsterMapper;
using MediatR;
using ReSale.Api.Contracts.Responses;
using ReSale.Api.Contracts.Responses.Customers;
using ReSale.Api.Infrastructure;
using ReSale.Application.Customers.GetByEmail;
using ReSale.Application.Customers.Results;
using ReSale.Domain.Common;

namespace ReSale.Api.Endpoints.Customers;

public class GetByEmail : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapGet("/customers/{email}", async (
                string email,
                ISender sender,
                IMapper mapper,
                CancellationToken cancellationToken) =>
            {
                var query = new GetCustomerByEmailQuery(email);

                Result<CustomerResult> result = await sender.Send(query, cancellationToken);

                if (result.IsFailure)
                {
                    return CustomResults.Problem(result);
                }

                return Results.Ok(mapper.Map<CustomerResponse>(result.Value));
            })
            .WithTags(Tags.Customers)
            .WithDescription("Retrieves a customer by email address.")
            .WithName("Get Customer By Email")
            .WithSummary("Get Customer By Email")
            .Produces(StatusCodes.Status200OK, typeof(CustomerResponse))
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization();
}

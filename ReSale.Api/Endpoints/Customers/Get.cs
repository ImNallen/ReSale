using System.Collections.ObjectModel;
using MapsterMapper;
using MediatR;
using ReSale.Api.Contracts.Responses.Customers;
using ReSale.Api.Infrastructure;
using ReSale.Application.Customers.Get;
using ReSale.Application.Customers.Results;
using ReSale.Domain.Common;

namespace ReSale.Api.Endpoints.Customers;

public class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapGet("/customers", async (
            string? searchTerm,
            string? sortColumn,
            string? sortOrder,
            int page,
            int pageSize,
            ISender sender,
            IMapper mapper,
            CancellationToken cancellationToken) =>
        {
            var query = new GetCustomersQuery(
                searchTerm,
                sortColumn,
                sortOrder,
                page,
                pageSize);

            Result<PagedList<CustomerResult>> result = await sender.Send(query, cancellationToken);

            if (result.IsFailure)
            {
                return CustomResults.Problem(result);
            }

            return Results
                .Ok(PagedList<CustomerResponse>
                .Create(
                    result.Value.Items.Select(mapper.Map<CustomerResponse>).ToList(),
                    result.Value.Page,
                    result.Value.PageSize,
                    result.Value.TotalCount));
        })
        .WithTags(Tags.Customers)
        .WithDescription("Get a paginated list of customers with the possibility of searching and sorting.")
        .WithName("Get Customers")
        .WithSummary("Get Customers")
        .Produces(StatusCodes.Status200OK, typeof(PagedList<CustomerResponse>))
        .RequireAuthorization();
}

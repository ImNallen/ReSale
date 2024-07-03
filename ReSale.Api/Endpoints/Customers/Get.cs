using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReSale.Api.Extensions;
using ReSale.Api.Infrastructure;
using ReSale.Application.Customers.Get;

namespace ReSale.Api.Endpoints.Customers;

public class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("customers", async (
            string? searchTerm,
            string? sortColumn,
            string? sortOrder,
            int page,
            int pageSize,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var query = new GetCustomersQuery(
                searchTerm, 
                sortColumn, 
                sortOrder,
                page,
                pageSize);
            
            var result = await sender.Send(query, cancellationToken);
            
            return result.Match(Results.Ok, CustomResults.Problem);
        }).WithTags(Tags.Customers)
        .RequireAuthorization();
    }
}
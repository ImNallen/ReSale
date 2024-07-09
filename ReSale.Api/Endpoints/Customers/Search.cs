using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReSale.Api.Contracts.Responses;
using ReSale.Api.Extensions;
using ReSale.Api.Infrastructure;
using ReSale.Application.Customers.Search;
using ReSale.Domain.Common;

namespace ReSale.Api.Endpoints.Customers;

public class Search : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("customers/search", async (
            string? searchTerm,
            string? sortColumn,
            string? sortOrder,
            int page,
            int pageSize,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var query = new SearchCustomersQuery(
                searchTerm, 
                sortColumn, 
                sortOrder,
                page,
                pageSize);
            
            var result = await sender.Send(query, cancellationToken);
            
            if (result.IsFailure)
                return CustomResults.Problem(result);
            
            // TODO: Add Mapper
            return Results.Ok(PagedList<CustomerResponse>.Create(result.Value.Items.Select(c => new CustomerResponse(
                        c.Id,
                        c.Email,
                        c.FirstName,
                        c.LastName,
                        c.Street,
                        c.City,
                        c.ZipCode,
                        c.Country,
                        c.State)).ToList(),
                result.Value.Page,
                result.Value.PageSize,
                result.Value.TotalCount));
        }).WithTags(Tags.Customers)
        .WithDescription("Get a paginated list of customers with the possibility of searching and sorting.")
        .WithName("Search Customers")
        .Produces(StatusCodes.Status200OK, typeof(PagedList<CustomerResponse>))
        .RequireAuthorization();
    }
}
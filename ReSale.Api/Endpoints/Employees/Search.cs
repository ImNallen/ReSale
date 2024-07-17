using MapsterMapper;
using MediatR;
using ReSale.Api.Contracts.Responses.Employees;
using ReSale.Api.Infrastructure;
using ReSale.Application.Employees.Search;
using ReSale.Domain.Common;

namespace ReSale.Api.Endpoints.Employees;

public class Search : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("employees/search", async (
                string? searchTerm,
                string? sortColumn,
                string? sortOrder,
                int page,
                int pageSize,
                ISender sender,
                IMapper mapper,
                CancellationToken cancellationToken) =>
            {
                var query = new SearchEmployeesQuery(
                    searchTerm, 
                    sortColumn, 
                    sortOrder,
                    page,
                    pageSize);
            
                var result = await sender.Send(query, cancellationToken);
            
                if (result.IsFailure)
                    return CustomResults.Problem(result);
            
                // TODO: Add Mapper
                return Results.Ok(PagedList<EmployeeResponse>.Create(result.Value.Items.Select(mapper.Map<EmployeeResponse>).ToList(),
                    result.Value.Page,
                    result.Value.PageSize,
                    result.Value.TotalCount));
            })
            .WithTags(Tags.Employees)
            .WithDescription("Get a paginated list of employees with the possibility of searching and sorting.")
            .WithName("Search Employees")
            .WithSummary("Search Employees")
            .Produces(StatusCodes.Status200OK, typeof(PagedList<EmployeeResponse>))
            .RequireAuthorization();
    }
}
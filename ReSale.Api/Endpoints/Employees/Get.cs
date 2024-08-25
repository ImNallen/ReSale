using MapsterMapper;
using MediatR;
using ReSale.Api.Contracts.Responses.Employees;
using ReSale.Api.Infrastructure;
using ReSale.Application.Employees.Get;
using ReSale.Application.Employees.Results;
using ReSale.Domain.Common;

namespace ReSale.Api.Endpoints.Employees;

public class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapGet("/employees", async (
                string? searchTerm,
                string? sortColumn,
                string? sortOrder,
                int page,
                int pageSize,
                ISender sender,
                IMapper mapper,
                CancellationToken cancellationToken) =>
            {
                var query = new GetEmployeesQuery(
                    searchTerm,
                    sortColumn,
                    sortOrder,
                    page,
                    pageSize);

                Result<PagedList<EmployeeResult>> result = await sender.Send(query, cancellationToken);

                if (result.IsFailure)
                {
                    return CustomResults.Problem(result);
                }

                return Results.Ok(PagedList<EmployeeResponse>.Create(
                    result.Value.Items.Select(mapper.Map<EmployeeResponse>).ToList(),
                    result.Value.Page,
                    result.Value.PageSize,
                    result.Value.TotalCount));
            })
            .WithTags(Tags.Employees)
            .WithDescription("Get a paginated list of employees with the possibility of searching and sorting.")
            .WithName("Get Employees")
            .WithSummary("Get Employees")
            .Produces(StatusCodes.Status200OK, typeof(PagedList<EmployeeResponse>))
            .RequireAuthorization();
}

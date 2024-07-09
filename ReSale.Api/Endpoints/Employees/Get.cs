using MediatR;
using ReSale.Api.Contracts.Responses;
using ReSale.Api.Contracts.Responses.Employees;
using ReSale.Api.Extensions;
using ReSale.Api.Infrastructure;
using ReSale.Application.Employees.Get;

namespace ReSale.Api.Endpoints.Employees;

public class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/employees", async (
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var query = new GetEmployeesQuery();
            
            var result = await sender.Send(query, cancellationToken);
            
            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Employees)
        .WithDescription("Get a list of all employees.")
        .WithName("Get Employees")
        .WithSummary("Get Employees")
        .Produces(StatusCodes.Status200OK, typeof(List<EmployeeResponse>))
        .RequireAuthorization();
    }
}
using MediatR;
using ReSale.Api.Extensions;
using ReSale.Api.Infrastructure;
using ReSale.Application.Employees.Get;
using ReSale.Application.Employees.Shared;

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
        }).WithTags(Tags.Employees)
        .WithDescription("Get a list of all employees.")
        .WithName("Get Employees")
        .Produces(StatusCodes.Status200OK, typeof(List<EmployeeResponse>))
        .RequireAuthorization();
    }
}
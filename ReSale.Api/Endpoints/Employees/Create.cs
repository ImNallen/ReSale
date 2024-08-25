using MediatR;
using ReSale.Api.Contracts.Requests.Employees;
using ReSale.Api.Extensions;
using ReSale.Api.Infrastructure;
using ReSale.Application.Employees.Create;
using ReSale.Application.Employees.Results;
using ReSale.Domain.Common;

namespace ReSale.Api.Endpoints.Employees;

public class Create : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapPost("/employees", async (
            CreateEmployeeRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new CreateEmployeeCommand(
                request.Email,
                request.FirstName,
                request.LastName,
                request.HireDate,
                request.Street,
                request.City,
                request.ZipCode,
                request.Country,
                request.State,
                request.Amount,
                request.Currency);

            Result<EmployeeResult> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Employees)
        .WithDescription("Creates a new employee.")
        .WithName("Create Employee")
        .WithSummary("Create Employee")
        .Produces(StatusCodes.Status200OK, typeof(Guid))
        .Produces(StatusCodes.Status400BadRequest)
        .RequireAuthorization();
}

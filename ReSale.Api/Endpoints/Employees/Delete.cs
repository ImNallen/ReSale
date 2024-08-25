using MediatR;
using ReSale.Api.Infrastructure;
using ReSale.Application.Employees.Delete;
using ReSale.Domain.Common;

namespace ReSale.Api.Endpoints.Employees;

public class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapDelete("/employees/{id:guid}", async (
                Guid id,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var command = new DeleteEmployeeCommand(id);

                Result<bool> result = await sender.Send(command, cancellationToken);

                return result.IsFailure
                    ? CustomResults.Problem(result)
                    : Results.NoContent();
            })
            .WithTags(Tags.Employees)
            .WithDescription("Deletes a employee by id")
            .WithName("Delete Employee")
            .WithSummary("Delete Employeee")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization();
}

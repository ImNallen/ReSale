using MapsterMapper;
using MediatR;
using ReSale.Api.Contracts.Responses.Employees;
using ReSale.Api.Infrastructure;
using ReSale.Application.Employees.GetById;
using ReSale.Application.Employees.Results;
using ReSale.Domain.Common;

namespace ReSale.Api.Endpoints.Employees;

public class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapGet("/employees/{id}", async (
                Guid id,
                ISender sender,
                IMapper mapper) =>
            {
                var query = new GetEmployeeByIdQuery(id);

                Result<EmployeeResult> result = await sender.Send(query);

                if (result.IsFailure)
                {
                    return CustomResults.Problem(result);
                }

                return Results.Ok(mapper.Map<EmployeeResponse>(result.Value));
            })
            .WithTags(Tags.Employees)
            .WithDescription("Get Employee by Id")
            .WithName("Get Employee By Id")
            .WithSummary("Get Employee By Id")
            .Produces(StatusCodes.Status200OK, typeof(EmployeeResponse))
            .RequireAuthorization();
}

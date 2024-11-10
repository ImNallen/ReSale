using MapsterMapper;
using MediatR;
using ReSale.Api.Contracts.Responses.Activities;
using ReSale.Api.Infrastructure;
using ReSale.Application.Activities.Get;
using ReSale.Application.Activities.Results;
using ReSale.Domain.Common;

namespace ReSale.Api.Endpoints.Activities;

public class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapGet("/activities", async (
            string? sortColumn,
            string? sortOrder,
            int page,
            int pageSize,
            ISender sender,
            IMapper mapper,
            CancellationToken cancellationToken) =>
        {
            var query = new GetActivitiesQuery(
                sortColumn,
                sortOrder,
                page,
                pageSize);

            Result<PagedList<ActivityResult>> result = await sender.Send(query, cancellationToken);

            return result.IsFailure
                ? CustomResults.Problem(result)
                : Results.Ok(PagedList<ActivityResponse>.Create(
                    result.Value.Items.Select(mapper.Map<ActivityResponse>).ToList(),
                    result.Value.Page,
                    result.Value.PageSize,
                    result.Value.TotalCount));
        })
        .WithTags(Tags.Activities)
        .WithDescription("Gets a list of activities.")
        .WithName("Get Activities")
        .WithSummary("Get Activities")
        .Produces(StatusCodes.Status200OK, typeof(PagedList<ActivityResponse>))
        .Produces(StatusCodes.Status400BadRequest)
        .RequireAuthorization();
}

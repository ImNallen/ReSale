using System;
using MapsterMapper;
using MediatR;
using ReSale.Api.Contracts.Responses.Messages;
using ReSale.Api.Infrastructure;
using ReSale.Application.Messages.Get;
using ReSale.Application.Messages.Results;
using ReSale.Domain.Common;

namespace ReSale.Api.Endpoints.Messages;

public class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapGet("/messages", async (
            string? sortColumn,
            string? sortOrder,
            int page,
            int pageSize,
            ISender sender,
            IMapper mapper,
            CancellationToken cancellationToken) =>
        {
            var query = new GetMessagesQuery(
                sortColumn,
                sortOrder,
                page,
                pageSize);

            Result<PagedList<MessageResult>> result = await sender.Send(query, cancellationToken);

            return result.IsFailure
                ? CustomResults.Problem(result)
                : Results.Ok(PagedList<MessageResponse>.Create(
                    result.Value.Items.Select(mapper.Map<MessageResponse>).ToList(),
                    result.Value.Page,
                    result.Value.PageSize,
                    result.Value.TotalCount));
        })
        .WithTags(Tags.Messages)
        .WithDescription("Gets a list of messages.")
        .WithName("Get Messages")
        .WithSummary("Get Messages")
        .Produces(StatusCodes.Status200OK, typeof(PagedList<MessageResponse>))
        .Produces(StatusCodes.Status400BadRequest)
        .RequireAuthorization();
}

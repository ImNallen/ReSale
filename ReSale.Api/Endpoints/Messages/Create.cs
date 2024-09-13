using MediatR;
using ReSale.Api.Contracts.Requests.Messages;
using ReSale.Api.Extensions;
using ReSale.Api.Infrastructure;
using ReSale.Application.Messages.Create;
using ReSale.Domain.Common;

namespace ReSale.Api.Endpoints.Messages;

public class Create : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapPost("/messages", async (
            CreateMessageRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new CreateMessageCommand(request.Title, request.Content);

            Result<Guid> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Messages)
        .WithDescription("Creates a new message.")
        .WithName("Create Message")
        .WithSummary("Create Message")
        .Produces(StatusCodes.Status200OK, typeof(Guid))
        .Produces(StatusCodes.Status400BadRequest)
        .RequireAuthorization();
}

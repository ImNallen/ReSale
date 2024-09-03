using MediatR;
using ReSale.Api.Contracts.Requests.Auth;
using ReSale.Api.Infrastructure;
using ReSale.Application.Auth.Reset;
using ReSale.Domain.Common;

namespace ReSale.Api.Endpoints.Auth;

public class Reset : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapPost("/auth/reset/{token:guid}", async (
            Guid token,
            ResetRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new ResetCommand(token, request.Password);

            Result result = await sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return CustomResults.Problem(result);
            }

            return Results.Ok();
        })
        .WithTags(Tags.Authentication)
        .WithDescription("Reset password.")
        .WithName("Reset")
        .WithSummary("Reset")
        .Produces(StatusCodes.Status200OK)
        .AllowAnonymous();
}

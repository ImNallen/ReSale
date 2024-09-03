using MediatR;
using ReSale.Api.Contracts.Requests.Auth;
using ReSale.Api.Infrastructure;
using ReSale.Application.Auth.Verify;
using ReSale.Domain.Common;

namespace ReSale.Api.Endpoints.Auth;

public class Verify : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapGet("/auth/verify/{token:guid}", async (
                Guid token,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var command = new VerifyCommand(token);

                Result result = await sender.Send(command, cancellationToken);

                if (result.IsFailure)
                {
                    return CustomResults.Problem(result);
                }

                return Results.Ok();
            })
            .WithTags(Tags.Authentication)
            .WithDescription("Verify email address.")
            .WithName("Verify")
            .WithSummary("Verify")
            .Produces(StatusCodes.Status200OK)
            .AllowAnonymous();
}

using MediatR;
using ReSale.Api.Contracts.Requests.Auth;
using ReSale.Api.Contracts.Responses.Auth;
using ReSale.Api.Extensions;
using ReSale.Api.Infrastructure;
using ReSale.Application.Auth.Refresh;
using ReSale.Application.Auth.Results;
using ReSale.Domain.Common;

namespace ReSale.Api.Endpoints.Auth;

public class Refresh : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapPost("auth/refresh", async (
            RefreshTokenRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new RefreshCommand(
                request.RefreshToken);

            Result<AccessTokenResult> result = await sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return CustomResults.Problem(result);
            }

            return Results.Ok(new AccessTokenResponse(
                result.Value.AccessToken,
                result.Value.ExpiresIn,
                result.Value.RefreshToken));
        })
        .WithTags(Tags.Authentication)
        .WithDescription("Retrieves a new access token using a refresh token.")
        .WithName("Refresh")
        .WithSummary("Refresh")
        .Produces(StatusCodes.Status200OK, typeof(AccessTokenResponse))
        .AllowAnonymous();
}

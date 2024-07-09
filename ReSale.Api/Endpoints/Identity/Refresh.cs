using MediatR;
using ReSale.Api.Contracts.Requests.Users;
using ReSale.Api.Contracts.Responses;
using ReSale.Api.Contracts.Responses.Identity;
using ReSale.Api.Extensions;
using ReSale.Api.Infrastructure;
using ReSale.Application.Identity.Refresh;

namespace ReSale.Api.Endpoints.Identity;

public class Refresh : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("identity/refresh", async (
                RefreshTokenRequest request,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var command = new RefreshCommand(
                    request.RefreshToken);
        
                var result = await sender.Send(command, cancellationToken);
        
                return result.Match(Results.Ok, CustomResults.Problem);
            }).WithTags(Tags.Identity)
            .WithDescription("Generates a new access token using a refresh token.")
            .WithName("Refresh")
            .WithSummary("Refresh")
            .Produces(StatusCodes.Status200OK, typeof(AccessTokenResponse))
            .AllowAnonymous();
    }
}
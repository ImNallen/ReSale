using MediatR;
using ReSale.Api.Contracts.Requests.Auth;
using ReSale.Api.Contracts.Responses.Auth;
using ReSale.Api.Infrastructure;
using ReSale.Application.Auth.Login;
using ReSale.Application.Auth.Results;
using ReSale.Domain.Common;

namespace ReSale.Api.Endpoints.Auth;

public class Login : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapPost("auth/login", async (
            LoginUserRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new LoginCommand(
                request.Email,
                request.Password);

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
        .WithDescription("Retrieves an access token for a user.")
        .WithName("Login")
        .WithSummary("Login")
        .Produces(StatusCodes.Status200OK, typeof(AccessTokenResponse))
        .AllowAnonymous();
}

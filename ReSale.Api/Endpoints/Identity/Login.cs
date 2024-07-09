using MediatR;
using ReSale.Api.Contracts.Requests.Users;
using ReSale.Api.Extensions;
using ReSale.Api.Infrastructure;
using ReSale.Application.Identity.Login;
using ReSale.Application.Identity.Shared;

namespace ReSale.Api.Endpoints.Identity;

public class Login : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("identity/login", async (
                LoginUserRequest request,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var command = new LoginCommand(
                    request.Email,
                    request.Password);
        
                var result = await sender.Send(command, cancellationToken);
        
                return result.Match(Results.Ok, CustomResults.Problem);
            }).WithTags(Tags.Identity)
            .WithDescription("Retrieves an access token for a user.")
            .WithName("Login")
            .Produces(StatusCodes.Status200OK, typeof(AccessTokenResponse))
            .AllowAnonymous();
    }
}
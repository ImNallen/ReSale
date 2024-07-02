using MediatR;
using ReSale.Api.Contracts.Requests.Users;
using ReSale.Api.Extensions;
using ReSale.Api.Infrastructure;
using ReSale.Application.Users.Login;

namespace ReSale.Api.Endpoints.Users;

public class Login : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/login", async (
                LoginUserRequest request,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var command = new LoginCommand(
                    request.Email,
                    request.Password);
        
                var result = await sender.Send(command, cancellationToken);
        
                return result.Match(Results.Ok, CustomResults.Problem);
            }).WithTags(Tags.Users)
            .AllowAnonymous();
    }
}
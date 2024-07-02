using MediatR;
using ReSale.Api.Contracts.Requests.Users;
using ReSale.Api.Extensions;
using ReSale.Api.Infrastructure;
using ReSale.Application.Users.Refresh;

namespace ReSale.Api.Endpoints.Users;

public class Refresh : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/refresh", async (
                RefreshTokenRequest request,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var command = new RefreshCommand(
                    request.RefreshToken);
        
                var result = await sender.Send(command, cancellationToken);
        
                return result.Match(Results.Ok, CustomResults.Problem);
            }).WithTags(Tags.Users)
            .AllowAnonymous();
    }
}
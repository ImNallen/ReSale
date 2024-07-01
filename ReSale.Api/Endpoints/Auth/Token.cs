using MediatR;
using ReSale.Api.Contracts.Requests.Auth;
using ReSale.Api.Extensions;
using ReSale.Api.Infrastructure;
using ReSale.Application.Auth.Token;

namespace ReSale.Api.Endpoints.Auth;

public class Token : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("token", async (
            TokenRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new TokenCommand(
                request.Email,
                request.Password);
            
            var result = await sender.Send(command, cancellationToken);
            
            return result.Match(Results.Ok, CustomResults.Problem);
        }).WithTags(Tags.Auth);
    }
}
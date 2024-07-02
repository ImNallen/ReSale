using MediatR;
using ReSale.Api.Contracts.Requests.Users;
using ReSale.Api.Extensions;
using ReSale.Api.Infrastructure;
using ReSale.Application.Users.Register;

namespace ReSale.Api.Endpoints.Users;

public class Register : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/register", async (
            RegisterUserRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new RegisterCommand(
                request.Email,
                request.Password,
                request.FirstName,
                request.LastName);
        
            var result = await sender.Send(command, cancellationToken);
        
            return result.Match(Results.Ok, CustomResults.Problem);
        }).WithTags(Tags.Users)
            .AllowAnonymous();
    }
}
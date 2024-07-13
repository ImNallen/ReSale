using MediatR;
using ReSale.Api.Contracts.Requests.Users;
using ReSale.Api.Extensions;
using ReSale.Api.Infrastructure;
using ReSale.Application.Identity.Register;

namespace ReSale.Api.Endpoints.Identity;

public class Register : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("identity/register", async (
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
        }).WithTags(Tags.Identity)
        .WithDescription("Registers a new user.")
        .WithName("Register")
        .WithSummary("Register")
        .Produces(StatusCodes.Status200OK, typeof(Guid))
        .AllowAnonymous();
    }
}
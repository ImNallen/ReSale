using MediatR;
using ReSale.Api.Contracts.Requests.Auth;
using ReSale.Api.Extensions;
using ReSale.Api.Infrastructure;
using ReSale.Application.Auth.Register;
using ReSale.Domain.Common;

namespace ReSale.Api.Endpoints.Auth;

public class Register : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapPost("auth/register", async (
            RegisterUserRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new RegisterCommand(
                request.Email,
                request.Password,
                request.FirstName,
                request.LastName);

            Result<Guid> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Authentication)
        .WithDescription("Registers a new user.")
        .WithName("Register")
        .WithSummary("Register")
        .Produces(StatusCodes.Status200OK, typeof(Guid))
        .AllowAnonymous();
}

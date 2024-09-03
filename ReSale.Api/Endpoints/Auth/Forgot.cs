using MediatR;
using ReSale.Api.Contracts.Requests.Auth;
using ReSale.Api.Infrastructure;
using ReSale.Application.Auth.Forgot;
using ReSale.Domain.Common;

namespace ReSale.Api.Endpoints.Auth;

public class Forgot : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapPost("auth/forgot", async (
            ForgotPasswordRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new ForgotPasswordCommand(request.Email);

            Result result = await sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return CustomResults.Problem(result);
            }

            return Results.Ok();
        })
        .WithTags(Tags.Authentication)
        .WithDescription("Sends a password reset link to the user's email.")
        .WithName("Forgot Password Request")
        .WithSummary("Request Password Reset")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status200OK)
        .AllowAnonymous();
}

using Refit;
using ReSale.Api.Contracts.Requests.Auth;
using ReSale.Api.Contracts.Responses.Auth;

namespace ReSale.Web.Clients;

public interface IAuthenticationClient
{
    [Post("/api/v1/auth/login")]
    Task<AccessTokenResponse> Login(LoginUserRequest request);

    [Get("/api/v1/auth/verify/{token}")]
    Task Verify(Guid token);

    [Post("/api/v1/auth/reset/{token}")]
    Task Reset(Guid token, ResetRequest request);

    [Post("/api/v1/auth/register")]
    Task<Guid> Register(RegisterUserRequest request);
}

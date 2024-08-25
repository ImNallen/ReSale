using Refit;
using ReSale.Api.Contracts.Requests.Auth;
using ReSale.Api.Contracts.Responses.Auth;

namespace ReSale.Web.Clients;

public interface IAuthenticationClient
{
    [Post("/api/v1/auth/login")]
    Task<AccessTokenResponse> Login(LoginUserRequest request);
}

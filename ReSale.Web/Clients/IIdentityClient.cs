using Refit;
using ReSale.Api.Contracts.Requests.Users;
using ReSale.Api.Contracts.Responses.Identity;

namespace ReSale.Web.Clients;

public interface IIdentityClient
{
    [Post("/api/v1/identity/login")]
    Task<AccessTokenResponse> Login(LoginUserRequest request);
}
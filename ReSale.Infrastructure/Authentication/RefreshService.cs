using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using ReSale.Application.Abstractions.Authentication;
using ReSale.Domain.Common;
using ReSale.Infrastructure.Authentication.Models;

namespace ReSale.Infrastructure.Authentication;

public class RefreshService(
    HttpClient httpClient,
    IOptions<KeycloakOptions> keycloakOptions) 
    : IRefreshService
{
    private static readonly Error AuthenticationFailed = new(
        "Keycloak.AuthenticationFailed",
        "Failed to acquire access token do to authentication failure",
        ErrorType.Problem);
    
    public async Task<Result<Token>> RefreshAccessTokenAsync(
        string refreshToken, 
        CancellationToken cancellationToken = default)
    {
        try
        {
            var authRequestParameters = new KeyValuePair<string, string>[]
            {
                new("client_id", keycloakOptions.Value.AuthClientId),
                new("client_secret", keycloakOptions.Value.AuthClientSecret),
                new("refresh_token", refreshToken),
                new("grant_type", "refresh_token"),
            };
            
            var authorizationRequestContent = new FormUrlEncodedContent(authRequestParameters);
            
            var response = await httpClient.PostAsync("", authorizationRequestContent, cancellationToken);
            
            response.EnsureSuccessStatusCode();
            
            var authorizationToken = await response.Content.ReadFromJsonAsync<AuthorizationToken>();
            
            if (authorizationToken is null)
            {
                return Result.Failure<Token>(AuthenticationFailed);
            }

            return new Token
            {
                AccessToken = authorizationToken.AccessToken,
                RefreshToken = authorizationToken.RefreshToken
            };
        }
        catch (HttpRequestException)
        {
            return Result.Failure<Token>(AuthenticationFailed);
        }
    }
}
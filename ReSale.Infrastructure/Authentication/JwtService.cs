using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using ReSale.Application.Abstractions.Authentication;
using ReSale.Domain.Common;
using ReSale.Infrastructure.Authentication.Models;

namespace ReSale.Infrastructure.Authentication;

internal sealed class JwtService : IJwtService
{
    private static readonly Error AuthenticationFailed = new(
        "Keycloak.AuthenticationFailed",
        "Failed to acquire access token do to authentication failure",
        ErrorType.Problem);

    private readonly HttpClient _httpClient;
    private readonly KeycloakOptions _keycloakOptions;

    public JwtService(HttpClient httpClient, IOptions<KeycloakOptions> keycloakOptions)
    {
        _httpClient = httpClient;
        _keycloakOptions = keycloakOptions.Value;
    }

    public async Task<Result<Token>> GetAccessTokenAsync(
        string email,
        string password,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var authRequestParameters = new KeyValuePair<string, string>[]
            {
                new("client_id", _keycloakOptions.AuthClientId),
                new("client_secret", _keycloakOptions.AuthClientSecret),
                new("scope", "openid email"),
                new("grant_type", "password"),
                new("username", email),
                new("password", password)
            };

            var authorizationRequestContent = new FormUrlEncodedContent(authRequestParameters);

            var response = await _httpClient.PostAsync("", authorizationRequestContent, cancellationToken);

            response.EnsureSuccessStatusCode();

            var authorizationToken = await response.Content.ReadFromJsonAsync<AuthorizationToken>();

            if (authorizationToken is null)
            {
                return Result.Failure<Token>(AuthenticationFailed);
            }

            return new Token
            {
                AccessToken = authorizationToken.AccessToken,
                RefreshToken = authorizationToken.RefreshToken,
                ExpiresIn = authorizationToken.ExpiresIn,
                RefreshExpiresIn = authorizationToken.RefreshExpiresIn,
                TokenType = authorizationToken.TokenType,
                Scope = authorizationToken.Scope,
                IdToken = authorizationToken.IdToken,
                SessionState = authorizationToken.SessionState,
                NotBeforePolicy = authorizationToken.NotBeforePolicy
            };
        }
        catch (HttpRequestException)
        {
            return Result.Failure<Token>(AuthenticationFailed);
        }
    }
}
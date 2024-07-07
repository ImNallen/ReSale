using System.Text.Json.Serialization;

namespace ReSale.Infrastructure.Authentication.Models;

public sealed class AuthorizationToken
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; init; } = string.Empty;
    
    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; init; } = string.Empty;
    
    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; init; }
    
    [JsonPropertyName("refresh_expires_in")]
    public int RefreshExpiresIn { get; init; }
}
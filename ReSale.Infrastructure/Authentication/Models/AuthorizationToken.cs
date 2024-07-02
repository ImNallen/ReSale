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
    
    [JsonPropertyName("token_type")]
    public string TokenType { get; init; } = string.Empty;
    
    [JsonPropertyName("id_token")]
    public string IdToken { get; init; } = string.Empty;
    
    [JsonPropertyName("not-before-policy")]
    public int NotBeforePolicy { get; init; }
    
    [JsonPropertyName("session_state")]
    public string SessionState { get; init; } = string.Empty;
    
    [JsonPropertyName("scope")]
    public string Scope { get; init; } = string.Empty;
}
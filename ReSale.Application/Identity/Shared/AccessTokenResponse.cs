namespace ReSale.Application.Identity.Shared;

public sealed record AccessTokenResponse(
    string AccessToken, 
    string RefreshToken,
    int ExpiresIn,
    int RefreshExpiresIn);
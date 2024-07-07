namespace ReSale.Application.Users.Shared;

public sealed record AccessTokenResponse(
    string AccessToken, 
    string RefreshToken,
    int ExpiresIn,
    int RefreshExpiresIn);
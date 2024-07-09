namespace ReSale.Api.Contracts.Responses.Identity;

public sealed record AccessTokenResponse(
    string AccessToken, 
    string RefreshToken,
    int ExpiresIn,
    int RefreshExpiresIn);
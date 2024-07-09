namespace ReSale.Api.Contracts.Responses;

public sealed record AccessTokenResponse(
    string AccessToken, 
    string RefreshToken,
    int ExpiresIn,
    int RefreshExpiresIn);
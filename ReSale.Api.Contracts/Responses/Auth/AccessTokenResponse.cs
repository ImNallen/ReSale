namespace ReSale.Api.Contracts.Responses.Auth;

public sealed record AccessTokenResponse(
    string AccessToken,
    int ExpiresIn,
    string RefreshToken);

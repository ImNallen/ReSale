namespace ReSale.Application.Auth.Results;

public record AccessTokenResult(
    string AccessToken,
    int ExpiresIn,
    string RefreshToken);

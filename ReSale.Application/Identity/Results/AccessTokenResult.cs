namespace ReSale.Application.Identity.Results;

public record AccessTokenResult(
    string AccessToken, 
    string RefreshToken,
    int ExpiresIn,
    int RefreshExpiresIn);
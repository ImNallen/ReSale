namespace ReSale.Application.Authentication.Results;

public record AccessTokenResult(
    string AccessToken, 
    int ExpiresIn = 60);
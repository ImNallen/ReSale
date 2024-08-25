namespace ReSale.Api.Contracts.Requests.Auth;

public sealed record LoginUserRequest(
    string Email,
    string Password);

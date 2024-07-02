namespace ReSale.Api.Contracts.Requests.Users;

public sealed record LoginUserRequest(
    string Email,
    string Password);
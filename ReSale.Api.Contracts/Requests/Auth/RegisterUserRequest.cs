namespace ReSale.Api.Contracts.Requests.Auth;

public sealed record RegisterUserRequest(
    string Email,
    string Password,
    string FirstName,
    string LastName);

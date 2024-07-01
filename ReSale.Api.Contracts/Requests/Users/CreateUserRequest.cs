namespace ReSale.Api.Contracts.Requests.Users;

public sealed record CreateUserRequest(
    string Email,
    string Password,
    string FirstName,
    string LastName);
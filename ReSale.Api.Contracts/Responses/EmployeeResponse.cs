namespace ReSale.Api.Contracts.Responses;

public record EmployeeResponse(
    Guid Id,
    string Email,
    string FirstName,
    string LastName);
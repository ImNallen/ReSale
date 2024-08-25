namespace ReSale.Api.Contracts.Responses.Employees;

public record EmployeeResponse(
    Guid Id,
    string Email,
    string FirstName,
    string LastName,
    DateOnly HireDate,
    string Street,
    string City,
    string ZipCode,
    string Country,
    string State,
    decimal Amount,
    string Currency);

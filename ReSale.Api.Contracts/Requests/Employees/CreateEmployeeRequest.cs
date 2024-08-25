namespace ReSale.Api.Contracts.Requests.Employees;

public record CreateEmployeeRequest(
    string Email,
    string FirstName,
    string LastName,
    DateOnly HireDate,
    string Street,
    string City,
    string ZipCode,
    string Country,
    string? State,
    decimal Amount,
    string Currency);

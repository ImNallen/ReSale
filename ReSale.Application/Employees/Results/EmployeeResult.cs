namespace ReSale.Application.Employees.Results;

public record EmployeeResult(
    Guid Id,
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

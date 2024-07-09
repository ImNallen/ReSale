namespace ReSale.Application.Employees.Shared;

public record EmployeeResponse(
    Guid Id,
    string Email,
    string FirstName,
    string LastName);
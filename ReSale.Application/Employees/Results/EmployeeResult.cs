namespace ReSale.Application.Employees.Results;

public record EmployeeResult(
    Guid Id,
    string Email,
    string FirstName,
    string LastName);
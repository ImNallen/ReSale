namespace ReSale.Api.Contracts.Requests.Employees;

public record CreateEmployeeRequest(
    string Email,
    string FirstName,
    string LastName);
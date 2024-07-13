namespace ReSale.Api.Contracts.Requests.Employees;

public record CreateEmployeeRequest
{
    public required string Email { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}
using Swashbuckle.AspNetCore.Annotations;

namespace ReSale.Api.Contracts.Requests.Employees;

public record CreateEmployeeRequest
{
    [SwaggerSchema(Description = "The email of the employee")]
    public required string Email { get; set; }
        
    [SwaggerSchema(Description = "The first name of the employee")]
    public required string FirstName { get; set; }
        
    [SwaggerSchema(Description = "The last name of the employee")]
    public required string LastName { get; set; }
}
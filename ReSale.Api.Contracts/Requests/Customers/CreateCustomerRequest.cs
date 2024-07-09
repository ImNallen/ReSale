using Swashbuckle.AspNetCore.Annotations;

namespace ReSale.Api.Contracts.Requests.Customers;

public record CreateCustomerRequest
{
    [SwaggerSchema(Description = "The email address of the customer.")]
    public required string Email { get; set; } 
    
    [SwaggerSchema(Description = "The first name of the customer.")]
    public required string FirstName { get; set; } 
    
    [SwaggerSchema(Description = "The last name of the customer.")]
    public required string LastName { get; set; } 
    
    [SwaggerSchema(Description = "The street address of the customer.")]
    public required string Street { get; set; } 
    
    [SwaggerSchema(Description = "The city of the customer.")]
    public required string City { get; set; } 
    
    [SwaggerSchema(Description = "The state of the customer. (Optional)")]
    public string State { get; set; } = string.Empty;
    
    [SwaggerSchema(Description = "The zip code of the customer.")]
    public required string ZipCode { get; set; } 
    
    [SwaggerSchema(Description = "The country of the customer.")]
    public required string Country { get; set; } 
}
namespace ReSale.Api.Contracts.Requests.Customers;

public record CreateCustomerRequest
{
    public required string Email { get; set; } 
    public required string FirstName { get; set; } 
    public required string LastName { get; set; } 
    public required string Street { get; set; } 
    public required string City { get; set; } 
    public string State { get; set; } = string.Empty;
    public required string ZipCode { get; set; } 
    public required string Country { get; set; }
    public required string PhoneNumber { get; set; }
}
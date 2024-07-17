namespace ReSale.Api.Contracts.Requests.Customers;

public record CreateCustomerRequest
{
    public required string Email { get; set; } 
    public required string FirstName { get; set; } 
    public required string LastName { get; set; } 
    public required string ShippingStreet { get; set; } 
    public required string ShippingCity { get; set; } 
    public string ShippingState { get; set; } = string.Empty;
    public required string ShippingZipCode { get; set; } 
    public required string ShippingCountry { get; set; }
    public required string PhoneNumber { get; set; }
    public string? BillingStreet { get; set; }
    public string? BillingCity { get; set; }
    public string? BillingState { get; set; }
    public string? BillingZipCode { get; set; }
    public string? BillingCountry { get; set; }
}
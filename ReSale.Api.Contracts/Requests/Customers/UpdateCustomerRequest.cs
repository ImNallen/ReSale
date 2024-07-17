namespace ReSale.Api.Contracts.Requests.Customers;

public record UpdateCustomerRequest
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string ShippingStreet { get; set; }
    public required string ShippingCity { get; set; }
    public string ShippingState { get; set; } = string.Empty;
    public required string ShippingZipCode { get; set; }
    public required string ShippingCountry { get; set; }
    public string? BillingStreet { get; set; } = string.Empty;
    public string? BillingCity { get; set; } = string.Empty;
    public string? BillingState { get; set; } = string.Empty;
    public string? BillingZipCode { get; set; } = string.Empty;
    public string? BillingCountry { get; set; } = string.Empty;
}
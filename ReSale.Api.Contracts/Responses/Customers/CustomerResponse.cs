﻿namespace ReSale.Api.Contracts.Responses.Customers;

public record CustomerResponse
{
    public required Guid Id { get; set; }
    public required string Email { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string ShippingStreet { get; set; }
    public required string ShippingCity { get; set; }
    public required string ShippingZipCode { get; set; }
    public required string ShippingCountry { get; set; }
    public string ShippingState { get; set; } = string.Empty;
    public required string PhoneNumber { get; set; }
    public string? BillingStreet { get; set; }
    public string? BillingCity { get; set; }
    public string? BillingZipCode { get; set; }
    public string? BillingCountry { get; set; }
    public string? BillingState { get; set; }
}
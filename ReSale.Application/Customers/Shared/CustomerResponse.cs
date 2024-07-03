namespace ReSale.Application.Customers.Shared;

public record CustomerResponse(
    Guid Id,
    string Email,
    string FirstName,
    string LastName,
    string Street,
    string City,
    string ZipCode,
    string Country,
    string State);
namespace ReSale.Application.Customers.Results;

public record CustomerResult(
    Guid Id,
    string Email,
    string FirstName,
    string LastName,
    string Street,
    string City,
    string ZipCode,
    string Country,
    string State,
    string PhoneNumber);
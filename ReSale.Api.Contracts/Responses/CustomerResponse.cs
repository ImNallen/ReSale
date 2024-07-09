namespace ReSale.Api.Contracts.Responses;

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
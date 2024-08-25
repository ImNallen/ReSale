namespace ReSale.Domain.Shared;

public record Address(
    string Street,
    string City,
    string ZipCode,
    string Country,
    string? State);

namespace ReSale.Api.Contracts.Responses.Products;

public record ProductResponse(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    string Currency);

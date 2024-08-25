namespace ReSale.Application.Products.Results;

public record ProductResult(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    string Currency);

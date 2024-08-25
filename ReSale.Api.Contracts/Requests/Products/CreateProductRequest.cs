namespace ReSale.Api.Contracts.Requests.Products;

public record CreateProductRequest(
    string Name,
    string Description,
    decimal Price,
    string Currency);

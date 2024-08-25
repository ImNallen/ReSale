using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Products.Results;

namespace ReSale.Application.Products.Create;

public record CreateProductCommand(
    string Name,
    string Description,
    decimal Price,
    string Currency) : ICommand<ProductResult>;

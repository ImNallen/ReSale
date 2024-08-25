using ReSale.Domain.Common;

namespace ReSale.Domain.Products;

public static class ProductErrors
{
    public static readonly Error NotFound = Error.NotFound("Product.NotFound", "Product not found.");
}

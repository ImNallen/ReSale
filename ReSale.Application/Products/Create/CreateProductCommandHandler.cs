using MapsterMapper;
using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Abstractions.Persistence;
using ReSale.Application.Products.Results;
using ReSale.Domain.Common;
using ReSale.Domain.Products;
using ReSale.Domain.Shared;

namespace ReSale.Application.Products.Create;

public class CreateProductCommandHandler(
    IReSaleDbContext context,
    IMapper mapper)
    : ICommandHandler<CreateProductCommand, ProductResult>
{
    public async Task<Result<ProductResult>> Handle(
        CreateProductCommand request,
        CancellationToken cancellationToken)
    {
        var product = Product.Create(
            new Name(request.Name),
            new Description(request.Description),
            new Money(
                request.Price,
                Currency.FromCode(request.Currency)));

        await context.Products.AddAsync(product, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return mapper.Map<ProductResult>(product);
    }
}

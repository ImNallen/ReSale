using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using ReSale.Application.Abstractions.Messaging;
using ReSale.Application.Abstractions.Persistence;
using ReSale.Application.Products.Results;
using ReSale.Domain.Common;
using ReSale.Domain.Products;

namespace ReSale.Application.Products.GetById;

public class GetProductByIdQueryHandler(
    IReSaleDbContext context,
    IMapper mapper) : IQueryHandler<GetProductByIdQuery, ProductResult>
{
    public async Task<Result<ProductResult>> Handle(
        GetProductByIdQuery request,
        CancellationToken cancellationToken)
    {
        ProductResult? product = await context.Products
            .Where(x => x.Id == request.Id)
            .Select(p => mapper.Map<ProductResult>(p))
            .FirstOrDefaultAsync(cancellationToken);

        if (product is null)
        {
            return Result.Failure<ProductResult>(ProductErrors.NotFound);
        }

        return product;
    }
}
